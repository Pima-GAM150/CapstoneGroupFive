using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyData : MonoBehaviour
{
	public Color[] keyColor;

    public int keyIndex = 0;

    public void RendColor(){
    	switch(keyIndex){
    		case 2: GetComponentInChildren<MeshRenderer>().material.color = keyColor[0]; break;
    		case 5: GetComponentInChildren<MeshRenderer>().material.color = keyColor[1]; break;
    		case 7: GetComponentInChildren<MeshRenderer>().material.color = keyColor[2]; break;
    		case 9: GetComponentInChildren<MeshRenderer>().material.color = keyColor[3]; break;
    		case 13: GetComponentInChildren<MeshRenderer>().material.color = keyColor[4]; break;
    	}
    }
}
