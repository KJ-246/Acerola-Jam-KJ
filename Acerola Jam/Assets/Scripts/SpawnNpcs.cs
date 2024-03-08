using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNpcs : MonoBehaviour
{
    public List<GameObject> npcs;
    public GameObject hoodedFigure;
    private bool spawnHoodedGuy;
    private GameObject spawnedNpcClone;
    public Animator npcAnimator;

    private bool spawnStarterNpc = true;
    //public GameValues gameValues;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        GameValues.Instance.OnStateChanged += GameValues_OnStateChanged;

        
    }

    private void GameValues_OnStateChanged(object sender, EventArgs e)
    {
        if (GameValues.Instance.IsDayOver()) {
            npcAnimator.SetTrigger("completedOrder");
            spawnStarterNpc = true;
        }

        if (GameValues.Instance.IsWaitingToStart()) {
            if (spawnedNpcClone != null) { 
                npcAnimator.SetTrigger("completedOrder");
                return;
            }
            spawnNpc();
        }
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        npcAnimator.SetTrigger("completedOrder");
        //DestroySpawnedNpc();
        //spawnNpc();
    }

    public void spawnNpc() {
        if (!GameValues.Instance.isStopSpawning())
        {
            GameObject spawnedNpc = npcs[UnityEngine.Random.Range(0, npcs.Count)];

            spawnedNpcClone = Instantiate(spawnedNpc, this.transform);
        }
        else {
            spawnHoodedFigure();
            spawnedNpcClone = Instantiate(hoodedFigure, this.transform);
        }
    }

    public void spawnHoodedFigure() {
        if (spawnHoodedGuy) { 
            npcAnimator.SetTrigger("completedOrder");
            spawnHoodedGuy = false;
        }
    }

    public void DestroySpawnedNpc() {
        Destroy(spawnedNpcClone);
    }
}
