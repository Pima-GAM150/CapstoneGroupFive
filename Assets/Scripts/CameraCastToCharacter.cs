using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCastToCharacter : MonoBehaviour
{
    public GameObject MyCamera;
    public GameObject Player;
    MeshRenderer DisabledRenderer;
    private List<MeshRenderer> myRenderList = new List<MeshRenderer>();
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Physics.Linecast(transform.position, Player.transform.position, out RaycastHit hit);

        if (hit.transform != null)
        {
            //Debug.Log("Raycast successful.");
            //Debug.Log(hit.transform.name);
        }

        if (hit.transform.tag != "Player")
        {
            Debug.Log(hit.transform.name);
            //if (hit.transform.GetComponent<MeshRenderer>() != DisabledRenderer)
            if(myRenderList != hit.transform.gameObject.GetComponent<MeshRendererList>().thisObjectMeshes)
            {
                foreach (MeshRenderer enabledRender in myRenderList)
                {
                    enabledRender.enabled = true;
                }
                //DisabledRenderer.enabled = true;
                //DisabledRenderer = hit.transform.GetComponent<MeshRenderer>();
                myRenderList = hit.transform.gameObject.GetComponent<MeshRendererList>().thisObjectMeshes;
                //Debug.Log("Switching Disabled Objects");
            }
            //hit.transform.GetComponent<MeshRenderer>().enabled = false;
            myRenderList = hit.transform.gameObject.GetComponent<MeshRendererList>().thisObjectMeshes;
            foreach (MeshRenderer enabledRender in myRenderList)
            {
                enabledRender.enabled = false;
            }
            //Debug.Log("Disabling Objects");
        }
        else
        {
            foreach (MeshRenderer enabledRender in myRenderList)
            {
                enabledRender.enabled = true;
            }
        }
    }

    
}
