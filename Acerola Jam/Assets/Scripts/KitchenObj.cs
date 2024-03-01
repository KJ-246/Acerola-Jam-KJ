using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObj : MonoBehaviour
{
    public ItemScriptableObj kitchenObjectSO;

    private StorageCounters storageCounters;

    public ItemScriptableObj GetItemScriptableObj() {
        return kitchenObjectSO;
    }

    public void SetStorageCounter(StorageCounters storageCounter) {
        if (this.storageCounters != null) {
            this.storageCounters.ClearKitchenObject();
        }

        this.storageCounters = storageCounter;

        if (storageCounter.HasKitchenObj()) {
            Debug.LogError("Counter already has a kitchen obj!!!!!!!!");
        }
        storageCounter.SetKitchenObj(this);

        transform.parent = storageCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public StorageCounters GetStorageCounters() {
        return storageCounters;
    }
}
