using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : MonoBehaviour, IKitchenObjectParent
{
    public CuttingRecipeSO[] cuttingRecipeSOArray;

    //public ItemScriptableObj kitchenObjectSO;
    public ItemScriptableObj kitchenObjectSO;
    public Transform counterPoint;
    private KitchenObj kitchenObject;

    public void Interact(CustomCursor customCursor)
    {
        if (!HasKitchenObj())
        {
            //customCursor.GetKitchenObj().SetKitchenObjectParent(customCursor);
            //There is no kitchen object here
            if (customCursor.HasKitchenObj())
            {
                //Player is carrying something
                customCursor.GetKitchenObj().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carrying anything
            }
        }
        else
        {
            //There is a kitchen object here
            if (customCursor.HasKitchenObj())
            {
                //GetKitchenObj();
                Debug.Log(customCursor.GetKitchenObj().kitchenObjectSO.itemName);
                if (customCursor.GetKitchenObj().kitchenObjectSO.itemName == "Knife") {
                    Debug.Log("Has the item");
                    InteractAlternate();
                }
                //player is carrying something
            }
            else
            {
                //player isnt carrying anythign
                GetKitchenObj().SetKitchenObjectParent(customCursor);
            }
        }
    }

    public void InteractAlternate() {
        if (HasKitchenObj()) {

            ItemScriptableObj outputKitchenObjectSO = GetOutputForInput(GetKitchenObj().GetKitchenObjectSO());
            GetKitchenObj().DestroySelf();

            KitchenObj.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private ItemScriptableObj GetOutputForInput(ItemScriptableObj inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
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
