using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public Light Match;

    public float LifeSpan;

    float flickerWait = 0f;

    public float intensity;

    public float matchJumpiness = 2;

    public float nearPlane;

    private void Awake()
    {
        LifeSpan = getLifeSpan();
    }

    void Update()
    {

        if (LifeSpan > 2)
        {
            if (flickerWait <= 0)
            {
                try
                {
                    if (GetComponentInParent<CharacterControllerAddition>().changedTransform) randomFlickerMoveing();
                    else randomFlickerIdle();
                }
                catch { randomFlickerIdle(); }
                flickerWait = Random.Range(.1f, .2f);
            } 
        }
        else Match.intensity = 25 * LifeSpan;
        FlickerLerp();
        flickerWait -= Time.deltaTime;
        LifeSpan -= Time.deltaTime;
        if (LifeSpan < 0) Destroy(this.gameObject);
    }

    public void FlickerLerp()
    {
        Match.intensity = Mathf.Lerp(Match.intensity, intensity, Time.deltaTime * matchJumpiness);
        Match.shadowNearPlane = Mathf.Lerp(Match.shadowNearPlane,nearPlane,Time.deltaTime * matchJumpiness);
    }

    public void randomFlickerIdle()
    {
        intensity = Random.Range(60f, 70f);
        nearPlane = 2 - (intensity/100);
        if (intensity > 75f) flickerWait += .8f;
    }
    public void randomFlickerMoveing()
    {
        intensity = Random.Range(50f, 60f);
        nearPlane = 2 - (intensity / 100);
        if (intensity < 55f) flickerWait += .8f;
    }
    private float getLifeSpan()
    {
        return Random.Range(5f, 7f);
    }
}
