using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNpcs : MonoBehaviour
{
    public List<GameObject> npcs;
    private GameObject spawnedNpcClone;
    public Animator npcAnimator;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        spawnNpc();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        npcAnimator.SetTrigger("completedOrder");
        //DestroySpawnedNpc();
        //spawnNpc();
    }

    public void spawnNpc() {
        GameObject spawnedNpc = npcs[UnityEngine.Random.Range(0, npcs.Count)];

        spawnedNpcClone = Instantiate(spawnedNpc, this.transform);
    }

    public void DestroySpawnedNpc() {
        Destroy(spawnedNpcClone);
    }
}
