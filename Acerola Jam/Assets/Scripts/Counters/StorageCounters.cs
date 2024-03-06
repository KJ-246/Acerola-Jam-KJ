using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageCounters : MonoBehaviour, IKitchenObjectParent
{


    //public ItemScriptableObj kitchenObjectSO;
    public Transform counterPoint;
    private KitchenObj kitchenObject;

    public void Interact(CustomCursor customCursor) {
        if (!HasKitchenObj())
        {
            //customCursor.GetKitchenObj().SetKitchenObjectParent(customCursor);
            //There is no kitchen object here
            if (customCursor.HasKitchenObj())
            {
                //Player is carrying something
                customCursor.GetKitchenObj().SetKitchenObjectParent(this);
            }
            else { 
                //Player not carrying anything
            }
        }
        else {
            //There is a kitchen object here
            if (customCursor.HasKitchenObj())
            {
                //player is carrying something
                if (customCursor.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObject))
                {
                    //Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObj().GetKitchenObjectSO()))
                    {
                        GetKitchenObj().DestroySelf();
                    }
                }
                else {
                    //Player is not carrying a plate but something else
                    if (GetKitchenObj().TryGetPlate(out plateKitchenObject)) {
                        //Counter is holding plate
                        if (plateKitchenObject.TryAddIngredient(customCursor.GetKitchenObj().GetKitchenObjectSO())) {
                            customCursor.GetKitchenObj().DestroySelf();
                        }
                    }
                }
            }
            else {
                //player isnt carrying anythign
                GetKitchenObj().SetKitchenObjectParent(customCursor);
            }
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
