using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject Barricade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered");
            for(int i = 0; i < other.GetComponent<CharacterControllerAddition>().keyRing.Count; i++)
            {
                if (other.GetComponent<CharacterControllerAddition>().keyRing[i] == GetComponentInParent<InteractableDoor>().doorIndex) UnlockDoor();
            }
        }   
    }

    private void OnTriggerExit(Collider other) { if (other.gameObject.tag == "Barricade") { UnlockDoor(); } }

    public void UnlockDoor()
    {
        GetComponentInParent<InteractableDoor>().UnlockDoor();
        Destroy(this.gameObject);
    }
    
    public void RemoveBarricade()
    {
        if (Barricade) Destroy(Barricade);
    }

}
