using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public GameObject onPlateCompletedParticleEffect;
    public Transform particleEffectPosition;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        Instantiate(onPlateCompletedParticleEffect, particleEffectPosition.transform.position, Quaternion.identity);
    }
}
