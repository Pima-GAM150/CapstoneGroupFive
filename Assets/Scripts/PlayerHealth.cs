using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class PlayerHealth : MonoBehaviour
{

    public float playerHealth;
    public int healthSwitch;

    public Gradient ParticleHealthGradient;
    public VisualEffect HealthParticles;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth > 50)
        {
            //HealthParticles.SetGradient("HealthGradient", ParticleHealthGradient);
            healthSwitch = 0;
            HealthParticles.SetInt("myHealthSwitch", healthSwitch);
        }

        if (playerHealth < 50)
        {
            //HealthParticles.SetGradient("LowHealthGradient", ParticleHealthGradient);
            healthSwitch = 1;
            HealthParticles.SetInt("myHealthSwitch", healthSwitch);
        }

        if (playerHealth <= 0)
        {
            //Application.Quit();
            //Will actually set up a system here for reset depending on how we decide to go about our scene set up.
            Debug.Log("Game Over!");
        }
    }

    public void TakeDamage(float dam) { playerHealth -= dam; }
    
}
