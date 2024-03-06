using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour, IKitchenObjectParent
{

    public Transform counterPoint;
    private KitchenObj kitchenObject;

    public void Interact(CustomCursor customCursor) {
        if (customCursor.HasKitchenObj()) {
            if (customCursor.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObj)) {
                //Only accepts plates

                DeliveryManager.Instance.DeliverRecipe(plateKitchenObj);

                customCursor.GetKitchenObj().DestroySelf();
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
