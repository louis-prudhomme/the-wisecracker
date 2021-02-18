using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunFlashController : MonoBehaviour
{
    private float duration;
    private float existence = 0f;

    public float radius = 4f;
    public float offSetFromBarrel = 3f;

    private ParticleSystem sparks;

    private Light flash;
    private float baseLightIntensity;

    void Start()
    {
        sparks = GetComponentInChildren<ParticleSystem>();
        duration = sparks.main.duration / sparks.main.simulationSpeed;

        flash = GetComponentInChildren<Light>();
        baseLightIntensity = flash.intensity;
    }

    void Update()
    {
        existence += Time.deltaTime;
        flash.intensity = baseLightIntensity 
            * (1 - (duration / existence));

        if (existence > duration)
        {
            FindColliding();
            Destroy(gameObject);
        }
    }

    private void FindColliding()
    {
        Vector3 overlapSpherePosition = Utils.Copy(transform.position) 
            + transform.TransformDirection(Vector3.forward) * offSetFromBarrel;

        foreach (var c in Physics.OverlapSphere(overlapSpherePosition, radius))
            if (c.gameObject.tag == "Rioter")
                c.GetComponent<RioterController>()
                    .Scare(ScareType.SHOTGUN);
    }
}
