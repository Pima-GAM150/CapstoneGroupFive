using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
	public Transform Light;
	public Transform Player;

	public GameObject match;
	private GameObject lightSource;

    // Update is called once per frame
    void Update()
    {
    	if (Vector3.Distance(Light.transform.position,Player.transform.position) < 2){
    		if (Input.GetKeyDown(KeyCode.E)){
        		if (Player.GetComponent<CharacterControllerAddition>().HasMatch() && lightSource == null){
        			LightLantern();
        		}
        	}
    	}      
    }

    public void LightLantern(){
    	lightSource = Instantiate<GameObject>(match,Light.transform);
    	lightSource.GetComponent<Flicker>().LifeSpan = 30f;
        FindObjectOfType<SceneOneSaveManager>().Save();
    }
}
