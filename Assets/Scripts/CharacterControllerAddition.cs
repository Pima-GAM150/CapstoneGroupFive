using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerAddition : MonoBehaviour
{
    public GameObject match;
    private GameObject instantiatedMatch;

    public UnityEngine.AI.NavMeshAgent agent;
    public Transform agentTarget;

    public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    public Transform sceneCamera;

    public Transform fingers;

    private RaycastHit currentHit;

    public IMoveableObject clickedObjHeld;

    public Transform heldObject;

    public GameObject myCharacter;

    private float wait = -1;

    public float matchWaitTimer = 0;

    public bool changedTransform;

    //Animator Region.  Double click to expand. Tap Ctrl + M twice to shrink.
    #region Animator Region

    Animator myAnimator;

    [SerializeField]
    bool isWalking;
    [SerializeField]
    bool isRunning;
    [SerializeField]
    bool isJumping;

    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Animator set-up
        myAnimator = myCharacter.GetComponent<Animator>();

        isWalking = false;
        isRunning = false;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        matchWaitTimer -= Time.deltaTime;
        if (matchWaitTimer < -600) matchWaitTimer = 0f;

        if (wait >= 0) wait -= Time.deltaTime;
        if (instantiatedMatch == null)
        {
            if (Input.GetKeyDown(KeyCode.Q)) if (matchWaitTimer <= 0)LightMatch();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q)) DropMatch();
        }
        agentTarget.position = transform.position;
        agentTarget.position = RotateDestination(GetDirection(), agentTarget.position);
        agent.destination = agentTarget.position;
        heldObject.position = RotateDestination(new Vector3(-.5f, 1.5f, 2.5f), transform.position);
        heldObject.position = RotateDestination(GetDirection(), heldObject.position);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out currentHit, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            IMoveableObject clickedObj = currentHit.collider.GetComponent<IMoveableObject>();
            if (clickedObj != null && wait < 0)
            {
                if (Input.GetMouseButton(0) && !clickedObj.IsHeld())
                {
                    clickedObj.OnClicked();
                    if (clickedObj.IsHeld())clickedObjHeld = clickedObj;
                    wait = 2;
                }
                if (!clickedObj.IsHeld()) clickedObj.OnMouseHover();
            } 
        }
        if (Input.GetMouseButtonDown(0) && clickedObjHeld != null && wait < 0)
        {
            clickedObjHeld.OnThrown();
            clickedObjHeld = null;
            wait = 2;
        }
        if (!Input.GetMouseButton(1))
        {
            var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            sceneCamera.eulerAngles -= new Vector3( mouseMovement.y * mouseSensitivityFactor,0f,0f);
            transform.eulerAngles += new Vector3(0f,mouseMovement.x * mouseSensitivityFactor,0f);
            if (!changedTransform) if (mouseMovement != new Vector2(0f, 0f)) changedTransform = true;
            else changedTransform = false;
        }

        //updating animator variables to the bools within the character controller
        myAnimator.SetBool("IsWalking", isWalking);
        myAnimator.SetBool("IsRunning", isRunning);
        myAnimator.SetBool("IsJumping", isJumping);
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            //for animator use
            isWalking = true;
            direction += Vector3.forward;
        }
        //for animator use
        else
        {
            isWalking = false;
            isRunning = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //for animator use
            isRunning = true;

            agent.speed = 14f;
        }
        else
        {
            //for animator use
            isRunning = false;

            agent.speed = 3.5f;
        }
        if (direction != new Vector3(0f, 0f, 0f)) changedTransform = true;
        else changedTransform = false;
        return direction;
    }

    public Vector3 RotateDestination(Vector3 direction, Vector3 input)
    {
        Vector3 rotatedTranslation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) * direction ;

        return input += rotatedTranslation;
    }

    public void DropMatch()
    {
        instantiatedMatch.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        instantiatedMatch.transform.parent = null;
        instantiatedMatch = null;
    }

    public void LightMatch()
    {
        instantiatedMatch = Instantiate<GameObject>(match, fingers.transform);
        instantiatedMatch.transform.localScale -= new Vector3(.099f,.099f,.099f);
        instantiatedMatch.transform.parent = fingers.transform;
    }

    public void SetMatchWaitTimer(float amount)
    {
        matchWaitTimer = amount;
    }
}
