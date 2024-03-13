using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public GameObject onPlateCompletedParticleEffect;
    public GameObject onSuccesfulCutParticleEffect;

    public CuttingMinigame cuttingMinigame;

    public Transform particleEffectPosition;

    public void Start()
    {
        cuttingMinigame.OnSuccesfulCut += CuttingMiniGame_SuccesfulCut;

        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void CuttingMiniGame_SuccesfulCut(object sender, EventArgs e)
    {
        Instantiate(onSuccesfulCutParticleEffect, particleEffectPosition.transform.position, Quaternion.identity);
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        Instantiate(onPlateCompletedParticleEffect, particleEffectPosition.transform.position, Quaternion.identity);
    }
}
