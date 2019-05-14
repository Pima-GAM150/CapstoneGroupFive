using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject Barricade;

    public Color[] lockColors;

    public SpriteRenderer miniMapDoor;

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

    public void RendColor()
    {
        Material lockMaterial = new Material(GetComponentInChildren<MeshRenderer>().material);
        MeshRenderer[] locks = GetComponentsInChildren<MeshRenderer>();
        switch (GetComponentInParent<InteractableDoor>().doorIndex)
        {
            case 2: lockMaterial.SetColor("_BaseColor", lockColors[0]); foreach (MeshRenderer i in locks) { i.material = lockMaterial; }; miniMapDoor.color = lockColors[0]; break;
            case 5: lockMaterial.SetColor("_BaseColor", lockColors[1]); foreach (MeshRenderer i in locks) { i.material = lockMaterial; }; miniMapDoor.color = lockColors[1]; break;
            case 7: lockMaterial.SetColor("_BaseColor", lockColors[2]); foreach (MeshRenderer i in locks) { i.material = lockMaterial; }; miniMapDoor.color = lockColors[2]; break;
            case 9: lockMaterial.SetColor("_BaseColor", lockColors[3]); foreach (MeshRenderer i in locks) { i.material = lockMaterial; }; miniMapDoor.color = lockColors[3]; break;
            case 13: lockMaterial.SetColor("_BaseColor", lockColors[4]); foreach (MeshRenderer i in locks) { i.material = lockMaterial; }; miniMapDoor.color = lockColors[4]; break;
        }
    }
}
