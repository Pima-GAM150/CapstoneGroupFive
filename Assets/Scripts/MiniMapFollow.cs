using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform Player;

    public RectTransform miniMapBorder;

    public RectTransform miniMask;

    public RectTransform miniMap;

    private bool expandMap = true;

    private bool big = false;

    private float[] cameraFOV;

    private Vector2 anchorReturn = new Vector2(-110,-110);

    private Vector2[] anchorTarget;

    private Vector2[] sizeDeltaTarget;

    private Vector2[] maskSizeDeltaTarget;

    // Start is called before the first frame update
    void Start()
    {
        sizeDeltaTarget = new Vector2[] { new Vector2(160, 160), new Vector2(500, 500) };
        maskSizeDeltaTarget = new Vector2[] { new Vector2(150, 150), new Vector2(490, 490) };
        anchorTarget = new Vector2[] { new Vector2(), new Vector2() };
        cameraFOV = new float[] { 20f, 35f };
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.position.x, transform.position.y, Player.position.z);    
        if (Input.GetKeyDown(KeyCode.M) && expandMap)
        {
            if (!big)
            {
                expandMap = false;
                miniMapBorder.anchorMax = new Vector2(0.5f, 0.5f);
                miniMapBorder.anchorMin = new Vector2(0.5f, 0.5f);
                miniMapBorder.anchoredPosition = miniMapBorder.position;
            }
            else
            {
                expandMap = false;
                miniMapBorder.anchorMax = Vector2.one;
                miniMapBorder.anchorMin = Vector2.one;
                miniMapBorder.anchoredPosition = miniMapBorder.position / -2;
            }
        }
        if (!expandMap)
        {
            if (big)
            {
                miniMapBorder.sizeDelta = Vector2.Lerp(miniMapBorder.sizeDelta, sizeDeltaTarget[0], Time.deltaTime);
                miniMask.sizeDelta = new Vector2(miniMapBorder.sizeDelta.x - 10, miniMapBorder.sizeDelta.y - 10);
                miniMap.sizeDelta = miniMask.sizeDelta;
                miniMapBorder.anchoredPosition = Vector2.Lerp(miniMapBorder.anchoredPosition,anchorReturn , Time.deltaTime);
                GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, cameraFOV[0],Time.deltaTime);
                if (miniMapBorder.anchoredPosition.x > -120)
                {
                    miniMapBorder.anchoredPosition = anchorReturn;
                    miniMask.sizeDelta = new Vector2(miniMapBorder.sizeDelta.x - 10, miniMapBorder.sizeDelta.y - 10);
                    miniMask.sizeDelta = maskSizeDeltaTarget[0];
                    GetComponent<Camera>().orthographicSize = cameraFOV[0];
                    big = false;
                    expandMap = true;
                }
            }
            else
            {
                miniMapBorder.sizeDelta = Vector2.Lerp(miniMapBorder.sizeDelta,sizeDeltaTarget[1],Time.deltaTime);
                miniMask.sizeDelta = new Vector2(miniMapBorder.sizeDelta.x - 10, miniMapBorder.sizeDelta.y - 10);
                miniMap.sizeDelta = miniMask.sizeDelta;
                miniMapBorder.anchoredPosition = Vector2.Lerp(miniMapBorder.anchoredPosition, Vector2.zero,Time.deltaTime);
                GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, cameraFOV[1], Time.deltaTime);
                if (miniMapBorder.anchoredPosition.x < 10){
                    miniMapBorder.anchoredPosition = Vector2.zero;
                    miniMapBorder.sizeDelta = sizeDeltaTarget[1];
                    miniMask.sizeDelta = new Vector2(miniMapBorder.sizeDelta.x - 10, miniMapBorder.sizeDelta.y - 10);
                    GetComponent<Camera>().orthographicSize = cameraFOV[1];
                    big = true;
                    expandMap = true;
                }
            }
        }
    }
}
