using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFreezeAI : MonoBehaviour
{
    public GameObject player;

    public UnityEngine.AI.NavMeshAgent agent;

    public Collider LibraryBounds;

    // Update is called once per frame
    void Update()
    {
        if (LibraryBounds.bounds.Contains(player.transform.position))
        {
            try
            {
                if (Vector3.Distance(FindObjectOfType<Light>().transform.position, transform.position) < FindObjectOfType<Light>().range)
                {
                    agent.destination = transform.position;
                }
            }
            catch { agent.destination = player.transform.position; agent.speed = 3.5f; }
        }
    }
}
