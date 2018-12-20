using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public float Delay = 5;
    public float Radius = 5.0F;
    public float Power = 10.0F;

    private void Start()
    {
        StartTime = Time.time;
    }

    public float StartTime;

    private void Update()
    {
        if (Time.time - StartTime > Delay) Detonation();
    }

    private void Detonation()
    {
        ParticleSystem.gameObject.SetActive(true);

        var colliders = Physics.OverlapSphere(transform.position, Radius);
        foreach (var hit in colliders)
        {
            var rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(Power, transform.position, Radius, 3.0F);
        }

//        ParticleSystem.Play();
        Destroy(gameObject, 4);
    }
}