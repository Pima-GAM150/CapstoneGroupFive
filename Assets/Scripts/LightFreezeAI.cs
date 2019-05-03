using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFreezeAI : MonoBehaviour
{
    public GameObject player;

    public GameObject monster;

    private UnityEngine.AI.NavMeshAgent agent;

    public Collider LibraryBounds;

    private Light[] lightScan;

    private Rigidbody rb;

    private bool chase;

    private float extinguishWait;

    private float chaseSpeed = 3;

    private Animator myAnimator;

    private void Start()
    {
        getNewWaitTime();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        agent.Warp(transform.position);
        myAnimator = monster.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lightScan = FindObjectsOfType<Light>();

        if (LibraryBounds.bounds.Contains(player.transform.position))
        {
            chase = true;
            foreach (Light light in lightScan) { if (LibraryBounds.bounds.Contains(light.transform.position)) { agent.destination = transform.position; agent.speed = 0f; chase = false; }  }
            if (chase)
            {
                myAnimator.SetBool("IsWalking", true);
                agent.destination = player.transform.position;
                agent.speed = chaseSpeed;
                if (Vector3.Distance(transform.position, player.transform.position) < 2)
                {
                    player.GetComponentInChildren<PlayerHealth>().TakeDamage(5 * Time.deltaTime);                    
                }
            }
            else
            {
                agent.destination = transform.position;
                agent.speed = 0f;
                myAnimator.SetBool("IsWalking", false);
            }
            extinguishWait -= Time.deltaTime;
            if (extinguishWait <= 0)
            {
                MassExtinguish(); getNewWaitTime();
            }
        }    
    }

    private void getNewWaitTime() { extinguishWait = Random.Range(14f, 35f); }

    private void MassExtinguish()
    {
        foreach(Light light in lightScan)
        {
            if (LibraryBounds.bounds.Contains(light.transform.position))
            {
                try { Destroy(light.GetComponentInParent<Flicker>().gameObject); }
                catch { Destroy(light.gameObject); }  
            }
        }
        player.GetComponent<CharacterControllerAddition>().SetMatchWaitTimer(Random.Range(3f,5f));
    }
}
