﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControllerAddition : MonoBehaviour
{
    public GameObject match;
    private GameObject instantiatedMatch;

    public UnityEngine.AI.NavMeshAgent agent;
    public Transform agentTarget;

    public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    public Transform sceneCamera;

    public Transform fingers;

    private RaycastHit currentHit;

    public IMoveableObject clickedObjHeld;

    public Transform heldObject;

    public GameObject myCharacter;

    private float wait = -1;

    public float matchWaitTimer = 0;

    public bool changedTransform;

    //Key Region.  Double click to expand. Tap Ctrl + M twice to shrink.
    #region Key Region

    bool hasKeys;
    int keyNum;

    #endregion

    // Update is called once per frame
    void Update()
    {
        matchWaitTimer -= Time.deltaTime;
        if (matchWaitTimer < -600) matchWaitTimer = 0f;

        if (wait >= 0) wait -= Time.deltaTime;
        if (instantiatedMatch == null)
        {
            if (Input.GetKeyDown(KeyCode.Q)) if (matchWaitTimer <= 0)LightMatch();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q)) DropMatch();
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out currentHit, Mathf.Infinity, LayerMask.GetMask("Default")))
        {
            IMoveableObject clickedObj = currentHit.collider.GetComponent<IMoveableObject>();
            if (clickedObj != null && wait < 0)
            {
                if (Input.GetMouseButton(0) && !clickedObj.IsHeld())
                {
                    clickedObj.OnClicked();
                    if (clickedObj.IsHeld())clickedObjHeld = clickedObj;
                    wait = 2;
                }
                if (!clickedObj.IsHeld()) clickedObj.OnMouseHover();
            } 
        }
        if (Input.GetMouseButtonDown(0) && clickedObjHeld != null && wait < 0)
        {
            clickedObjHeld.OnThrown();
            clickedObjHeld = null;
            wait = 2;
        }
        if (!Input.GetMouseButton(1))
        {
            var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            Vector3 angleToChangeCamera;

            if ((mouseMovement.y * mouseSensitivityFactor) > 5) angleToChangeCamera = new Vector3(5, 0f, 0f);
            else if ((mouseMovement.y * mouseSensitivityFactor) < -5) angleToChangeCamera = new Vector3(-5, 0f, 0f);
            else angleToChangeCamera = new Vector3(mouseMovement.y * mouseSensitivityFactor,0f,0f);

            if (sceneCamera.eulerAngles.x > 355 && mouseMovement.y > 0) sceneCamera.eulerAngles -= angleToChangeCamera;
            else if (sceneCamera.eulerAngles.x >= 0 && sceneCamera.eulerAngles.x <= 30 && mouseMovement.y > 0) sceneCamera.eulerAngles -= angleToChangeCamera;
            else if (sceneCamera.eulerAngles.x >= 350 && sceneCamera.eulerAngles.x >= 0 && mouseMovement.y < 0) sceneCamera.eulerAngles -= angleToChangeCamera;
            else if (sceneCamera.eulerAngles.x < 25 && mouseMovement.y < 0) sceneCamera.eulerAngles -= angleToChangeCamera;

            transform.eulerAngles += new Vector3(0f,mouseMovement.x * mouseSensitivityFactor,0f);
            if (!changedTransform) if (mouseMovement != new Vector2(0f, 0f)) changedTransform = true;
            else changedTransform = false;
        }
    }

    public void DropMatch()
    {
        instantiatedMatch.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        instantiatedMatch.transform.parent = null;
        instantiatedMatch = null;
    }

    public void LightMatch()
    {
        instantiatedMatch = Instantiate<GameObject>(match, fingers.transform);
        instantiatedMatch.transform.localScale -= new Vector3(.099f,.099f,.099f);
        instantiatedMatch.transform.parent = fingers.transform;
    }

    public void SetMatchWaitTimer(float amount)
    {
        matchWaitTimer = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            Destroy(other.gameObject);
            hasKeys = true;
        }
        if (other.gameObject.tag == "Victory")
        {
            //Change scene to credits scene/victory scene.
            Debug.Log("You Win!");
        }
    }
}
