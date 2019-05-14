using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour , IMoveableObject
{
    public Transform player;
    private Quaternion closedPosition;
    private bool close = false;
    private bool openInOpenOut;
    private bool locked;
    private UnityEngine.AI.NavMeshObstacle navMeshOb;
    private Rigidbody rb;

    public int doorIndex;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        closedPosition = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z,transform.rotation.w);
        navMeshOb = GetComponentInChildren<UnityEngine.AI.NavMeshObstacle>();
        rb = GetComponent<Rigidbody>();
        LockDoor();
    }

    // Update is called once per frame
    void Update()
    {
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
                transform.rotation = new Quaternion(transform.rotation.x, Mathf.Lerp(transform.rotation.y, closedPosition.y, Time.deltaTime * 5),transform.rotation.z,transform.rotation.w);
                //transform.Rotate(new Vector3(0f, Mathf.Lerp(transform.rotation.y, closedPosition.y, 5f), 0f));
                rb.velocity = Vector3.zero;
                if (transform.rotation.y <= closedPosition.y+.01)
                {
                    transform.rotation = closedPosition;
                    close = false;
                    navMeshOb.enabled = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
                
            }

            if (!openInOpenOut)
            {
                transform.rotation = new Quaternion(transform.rotation.x, Mathf.Lerp(closedPosition.y, transform.rotation.y, Time.deltaTime * 5), transform.rotation.z, transform.rotation.w);
                //transform.Rotate(new Vector3(0f, -Mathf.Lerp(transform.rotation.y, closedPosition.y, 5f), 0f));
                rb.velocity = Vector3.zero;
                if (transform.rotation.y >= closedPosition.y-.01)
                {
                    transform.rotation = closedPosition;
                    close = false;
                    navMeshOb.enabled = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster") { if (Unlocked()) { coroutine = Delayed(3f); StartCoroutine(coroutine); } }
    }

    public void OnClicked()
    {
        if (!locked)
        {
            navMeshOb.enabled = false;
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void LockDoor()
    {
        navMeshOb.enabled = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        locked = true;
    }

    public void UnlockDoor()
    {
        navMeshOb.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        locked = false;
    }

    public void OnThrown()
    {
        
    }

    public void OnMouseHover()
    {
        if (Vector3.Distance(player.position, transform.position) < 8) { }//Debug.Log("Open?"); //Change cursor to indicate player can use this
    }

    public bool IsHeld()
    {
        return false;
    }

    public bool Unlocked()
    {
        return !locked;
    }

    private IEnumerator Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        navMeshOb.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
    }
}
