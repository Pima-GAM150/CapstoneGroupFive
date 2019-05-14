using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyData : MonoBehaviour
{
	public Color[] keyColor;

    public int keyIndex = 0;

    public void RendColor(){
        Material keyMaterial = new Material(GetComponentInChildren<Renderer>().material);
    	switch(keyIndex){
    		case 2: keyMaterial.SetColor("_BaseColor",keyColor[0]); GetComponentInChildren<Renderer>().material = keyMaterial; break;
    		case 5: keyMaterial.SetColor("_BaseColor", keyColor[1]); GetComponentInChildren<Renderer>().material = keyMaterial; break;
    		case 7: keyMaterial.SetColor("_BaseColor", keyColor[2]); GetComponentInChildren<Renderer>().material = keyMaterial; break;
    		case 9: keyMaterial.SetColor("_BaseColor", keyColor[3]); GetComponentInChildren<Renderer>().material = keyMaterial; break;
    		case 13: keyMaterial.SetColor("_BaseColor", keyColor[4]); GetComponentInChildren<Renderer>().material = keyMaterial; break;
    	}
    }
}
