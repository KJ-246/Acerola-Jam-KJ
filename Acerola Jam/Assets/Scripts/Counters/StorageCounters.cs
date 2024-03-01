using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCounters : MonoBehaviour
{
    public ItemScriptableObj kitchenObjectSO;
    public Transform counterPoint;
    public StorageCounters secondStorageCounter;
    public bool testing;

    private KitchenObj kitchenObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T)) {
            if (kitchenObject != null) {
                kitchenObject.SetStorageCounter(secondStorageCounter);
            }
        }
    }

    public void Interact() {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterPoint);
            kitchenObjectTransform.GetComponent<KitchenObj>().SetStorageCounter(this);
        }
        else {
            //kitchenObject.SetStorageCounter(customCursorPoint);
            //Debug.Log(kitchenObject.GetStorageCounters());
        }
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterPoint;
    }

    public void SetKitchenObj(KitchenObj kitchenObj) {
        this.kitchenObject = kitchenObj;
    }

    public KitchenObj GetKitchenObj() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObj() {
        return kitchenObject != null;
    }
}
