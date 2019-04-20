using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatProximity : MonoBehaviour
{
    public int heartRate = 20;
    public float distance;
    public float audioDelay = 0f;
    public GameObject Player;
    public GameObject Monster;

    public AudioSource heartSource;
    AudioClip heartClip;

    // Start is called before the first frame update
    void Start()
    {
        heartClip = heartSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Round(Vector3.Distance(Player.transform.position, Monster.transform.position));
        if (distance < 120)
        {
            audioDelay += 1;
            if (distance >= 12 && audioDelay > distance - 5 && audioDelay < distance + 5 && !heartSource.isPlaying)
            {
                Debug.Log("Playing heartbeat");
                heartSource.Play();
                audioDelay = 0;
            }
            else if (audioDelay > distance + 5)
            {
                audioDelay = 0;
            }
        }
    }
}
