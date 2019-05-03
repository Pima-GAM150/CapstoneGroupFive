using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Change scene to credits scene/victory scene.
            SceneManager.LoadScene(4);
            //Debug.Log("You Win!");
        }
    }
}
