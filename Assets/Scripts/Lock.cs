using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<CharacterControllerAddition>().hasKeys == true)
            {
                GetComponentInParent<InteractableDoor>().UnlockDoor();
                //Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            //other.GetComponentInParent<CharacterControllerAddition>().HasKeys();
        }   
    }
}
