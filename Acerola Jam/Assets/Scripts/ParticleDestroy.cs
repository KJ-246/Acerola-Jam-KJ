using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private float particleLifetime = 1f;

    private void Update()
    {
        particleLifetime -= Time.deltaTime;

        if (particleLifetime <= 0) {
            Destroy(gameObject);
        }
    }
}
