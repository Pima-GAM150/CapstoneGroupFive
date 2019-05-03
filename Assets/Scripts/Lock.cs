using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            Destroy(other.gameObject);
            other.GetComponentInParent<InteractableDoor>().UnlockDoor();
            //other.GetComponentInParent<CharacterControllerAddition>().HasKeys();
        }   
    }
}
