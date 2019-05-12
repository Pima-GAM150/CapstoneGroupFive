using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCastTest : MonoBehaviour
{
    public GameObject MyCamera;
    public GameObject Player;
    MeshRenderer DisabledRenderer;
    private List<MeshRenderer> myRenderList = new List<MeshRenderer>();
    private List<List<MeshRenderer>> myRenderListList = new List<List<MeshRenderer>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Linecast(transform.position, Player.transform.position, out RaycastHit hit);
        if (hit.transform)
        {
            if (hit.transform.tag != "Player")
            {
                MeshRenderer currentHit = hit.collider.GetComponent<MeshRenderer>();
                if (currentHit && !myRenderList.Contains(currentHit))
                {
                    currentHit.enabled = false;
                    myRenderList.Add(currentHit);
                }
                if (!currentHit && hit.transform.tag == "DisableMesh")
                {
                    List<MeshRenderer> currentHits = hit.collider.GetComponent<MeshRendererList>().thisObjectMeshes;
                    if (currentHits != null && !myRenderListList.Contains(currentHits))
                    {
                        foreach (MeshRenderer disableRender in currentHits)
                        {
                            disableRender.enabled = false;
                        }
                        myRenderListList.Add(currentHits);
                    }
                }
            }
            else
            {
                if (myRenderList.Count > 0)
                {
                    foreach (MeshRenderer enabledRender in myRenderList)
                    {
                        enabledRender.enabled = true;
                    }
                    myRenderList.Clear();
                }   
                if (myRenderListList.Count > 0)
                {
                    foreach (List<MeshRenderer> enabledRenders in myRenderListList)
                    {
                        foreach (MeshRenderer enabledRender in enabledRenders)
                        {
                            enabledRender.enabled = true;
                        }
                    }
                    myRenderListList.Clear();
                }
            }
        } 
    }
}
