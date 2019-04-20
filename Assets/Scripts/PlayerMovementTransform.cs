using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTransform : MonoBehaviour
{

    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";
    private string jumpInputAxis = "Jump";

    public float rotationRate = 360;
    public float moveSpeed = 2;
    public float jumpForce = 100;
    public bool isJumping;

    private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);
        float jumpAxis = Input.GetAxis(jumpInputAxis);

        ApplyInput(moveAxis, turnAxis, jumpAxis);
    }

    private void ApplyInput(float moveInput, float turnInput, float jumpInput)
    {
        Move(moveInput);
        Turn(turnInput);
        if (playerRB.velocity.y == 0)
        {
            isJumping = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            Jump(jumpInput);
        }
    }

    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

    private void Jump(float input)
    {
        playerRB.AddForce(0, input * jumpForce, 0);
    }
}
