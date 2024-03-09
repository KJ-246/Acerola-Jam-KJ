using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNpcs : MonoBehaviour
{
    [Header("List of Npc Aberrations over time")]
    public List<GameObject> npcsDay1;
    public List<GameObject> npcsDay2;
    public List<GameObject> npcsDay3;
    public List<GameObject> npcsDay4;
    private List<GameObject> currentNpcs;

 
    public GameObject hoodedFigure;
    private bool spawnHoodedGuy;
    private GameObject spawnedNpcClone;
    public Animator npcAnimator;


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        GameValues.Instance.OnStateChanged += GameValues_OnStateChanged;

        currentNpcs = npcsDay1;
    }

    //Use event instead
    private void Update()
    {
        EvaluateDayNum();
    }

    public void EvaluateDayNum() {
        switch (GameValues.Instance.DayNum)
        {
            case 1:
                currentNpcs = npcsDay1;
                break;
            case 2:
                currentNpcs = npcsDay2;
                break;
            case 3:
                currentNpcs = npcsDay3;
                break;
            case 4:
                currentNpcs = npcsDay4;
                break;
        }
    }

    private void GameValues_OnStateChanged(object sender, EventArgs e)
    {
        if (GameValues.Instance.IsDayOver()) {
            npcAnimator.SetTrigger("completedOrder");
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
            GameObject spawnedNpc = currentNpcs[UnityEngine.Random.Range(0, currentNpcs.Count)];

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
