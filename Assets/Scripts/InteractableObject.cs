using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour , IMoveableObject
{
    public Transform player;
    public Transform playerHand;

    public float ThrowForce = 10;

    public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    float LerpSpeed = 5;

    public bool Held = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Held)
        {
            gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, playerHand.position, Time.deltaTime * LerpSpeed));
            if (Input.GetMouseButton(1))
            {
                var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

                transform.eulerAngles += new Vector3(mouseMovement.y * mouseSensitivityFactor, mouseMovement.x * mouseSensitivityFactor, 0f);
            }
        }
    }

    public void OnClicked()
    {
        if (Vector3.Distance(player.position, transform.position) < 8)
        {
            Held = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void OnThrown()
    {
        Held = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().AddForce(-GetDirectionTo(player.position) * ThrowForce,ForceMode.Impulse);
    }

    public void OnMouseHover()
    {
        if (Vector3.Distance(player.position, transform.position) < 8) Debug.Log("Care to pick this up?"); //Change cursor to indicate player can use this
    }

    public bool IsHeld()
    {
        return Held;
    }

    public Vector3 GetDirectionTo(Vector3 input)
    {
        return (input - transform.position).normalized;
    }
}
