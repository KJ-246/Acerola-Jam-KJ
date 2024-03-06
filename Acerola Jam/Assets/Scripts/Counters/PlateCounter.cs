using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : MonoBehaviour, IKitchenObjectParent
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    public ItemScriptableObj plateKitchenObjectSO;
    public Transform counterPoint;
    private KitchenObj kitchenObject;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;


    private float spawnPlateTimerMax = 4f;
    private float spawnPlateTimer;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (spawnPlateTimer > spawnPlateTimerMax) {
            //KitchenObj.SpawnKitchenObject(plateKitchenObjectSO, this);
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void Interact(CustomCursor customCursor)
    {
        if (!customCursor.HasKitchenObj())
        {
            //player is empty handed
            if (platesSpawnedAmount > 0) {
                //atleast 1 plate here
                platesSpawnedAmount--;

                KitchenObj.SpawnKitchenObject(plateKitchenObjectSO, customCursor);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }


    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return counterPoint;
    }

    public void SetKitchenObj(KitchenObj kitchenObj)
    {
        this.kitchenObject = kitchenObj;
    }

    public KitchenObj GetKitchenObj()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObj()
    {
        return kitchenObject != null;
    }
}
