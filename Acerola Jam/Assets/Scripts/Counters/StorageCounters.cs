using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCounters : MonoBehaviour, IKitchenObjectParent
{


    public ItemScriptableObj kitchenObjectSO;
    public Transform counterPoint;
    private KitchenObj kitchenObject;

    public void Interact(CustomCursor customCursor) {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterPoint);
            kitchenObjectTransform.GetComponent<KitchenObj>().SetKitchenObjectParent(this);
        }
        else {
            //kitchenObject.SetStorageCounter(customCursorPoint);
            kitchenObject.SetKitchenObjectParent(customCursor);
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
