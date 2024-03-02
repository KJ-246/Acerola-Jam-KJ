using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObj : MonoBehaviour
{
    public ItemScriptableObj kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public ItemScriptableObj GetItemScriptableObj() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObj()) {
            Debug.LogError("IKitchenObjectParent already has a kitchen obj!!!!!!!!");
        }
        kitchenObjectParent.SetKitchenObj(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }
}
