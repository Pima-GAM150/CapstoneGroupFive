using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTransform : MonoBehaviour
{

    private string verticalInputAxis = "Vertical";
    private string horizontalInputAxis = "Horizontal";
    private string jumpInputAxis = "Jump";

    public float rotationRate = 360;
    public float moveSpeed = 2;
    public float sprintMoveSpeed;
    public float jumpForce = 100;
    public bool isJumping;

    private Rigidbody playerRB;

    #region Animator Reference

    

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalAxis = Input.GetAxis(verticalInputAxis);
        float horizontalAxis = Input.GetAxis(horizontalInputAxis);
        float jumpAxis = Input.GetAxis(jumpInputAxis);

        ApplyInput(verticalAxis, horizontalAxis, jumpAxis);
    }

    private void ApplyInput(float verticalInput, float horizontalInput, float jumpInput)
    {
        MoveVertical(verticalInput);
        MoveHorizontal(horizontalInput);
        if (playerRB.velocity.y == 0)
        {
            isJumping = false;
        }
        /*if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            Jump(jumpInput);
        }*/
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<CharacterControllerAddition>().isWalking = true;
        }
        else
        {
            GetComponent<CharacterControllerAddition>().isWalking = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Need a backwards walk animation...
            //GetComponent<CharacterControllerAddition>().isBackpeddle = true;
        }
        else
        {
            //GetComponent<CharacterControllerAddition>().isBackpeddle = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //GetComponent<CharacterControllerAddition>().isRight = true;
        }
        else
        {
            //GetComponent<CharacterControllerAddition>().isRight = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //GetComponent<CharacterControllerAddition>().isLeft = true;
        }
        else
        {
            //GetComponent<CharacterControllerAddition>().isLeft = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<CharacterControllerAddition>().isRunning = true;
            sprintMoveSpeed = 3;
        }
        else
        {
            GetComponent<CharacterControllerAddition>().isRunning = false;
            sprintMoveSpeed = 1;
        }
    }

    private void MoveVertical(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed * sprintMoveSpeed);
    }

    private void MoveHorizontal(float input)
    {
        transform.Translate(Vector3.right * input * moveSpeed * sprintMoveSpeed);
    }

    private void Jump(float input)
    {
        playerRB.AddForce(0, input * jumpForce, 0);
    }
}
