using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour , IMoveableObject
{
    public Transform player;
    private Vector3 doorPosition;
    private Quaternion closedPosition;
    private bool close = false;
    private bool openInOpenOut;
    private bool locked = false;
    private UnityEngine.AI.NavMeshObstacle navMeshOb;
    private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        doorPosition = transform.position;
        closedPosition = new Quaternion(0f, transform.rotation.y, 0f,0f);
        navMeshOb = GetComponentInChildren<UnityEngine.AI.NavMeshObstacle>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = doorPosition;
        if (Vector3.Distance(player.position, transform.position) < 10)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                close = true;
                if (transform.rotation.y > closedPosition.y) openInOpenOut = true;
                else openInOpenOut = false;
            }
        }
        
        if (close)
        {
            if (openInOpenOut)
            {
                transform.Rotate(new Vector3(0f, -Mathf.Lerp(transform.position.y, closedPosition.y, Time.deltaTime), 0f));
                rb.velocity = Vector3.zero;
                if (transform.rotation.y <= 0)
                {
                    transform.rotation = closedPosition;
                    close = false;
                    navMeshOb.enabled = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
                
            }

            if (!openInOpenOut)
            {
                transform.Rotate(new Vector3(0f, Mathf.Lerp(transform.position.y, closedPosition.y, Time.deltaTime), 0f));
                rb.velocity = Vector3.zero;
                if (transform.rotation.y >= 0)
                {
                    transform.rotation = closedPosition;
                    close = false;
                    navMeshOb.enabled = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }   
        }
    }

    public void OnClicked()
    {
        Debug.Log("CLICKED");
        if (!locked)
        {
            navMeshOb.enabled = false;
            rb.constraints = RigidbodyConstraints.None;
        } 
    }

    public void OnThrown()
    {
        
    }

    public void OnMouseHover()
    {
        if (Vector3.Distance(player.position, transform.position) < 8) Debug.Log("Open?"); //Change cursor to indicate player can use this
    }

    public bool IsHeld()
    {
        return false;
    }
}
