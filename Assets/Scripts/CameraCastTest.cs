using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCastTest : MonoBehaviour
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
            }
            else
            {
                foreach (MeshRenderer enabledRender in myRenderList)
                {
                    enabledRender.enabled = true;
                }
                myRenderList.Clear();
            }
        } 
    }
}
