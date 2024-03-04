using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : MonoBehaviour, IKitchenObjectParent, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    

    public CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

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
                if (hasRecipeWithInput(customCursor.GetKitchenObj().GetKitchenObjectSO())) { 
                    //Carrying something that can be cut!!
                    customCursor.GetKitchenObj().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObj().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
                //Player is carrying something
                
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
                //Debug.Log(customCursor.GetKitchenObj().kitchenObjectSO.itemName);
                if (customCursor.GetKitchenObj().GetKitchenObjectSO().itemName == "Knife") {
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
        if (HasKitchenObj() && hasRecipeWithInput(GetKitchenObj().GetKitchenObjectSO())) {

            //There is a kitchenObject here AND it can be cut 
            cuttingProgress++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObj().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) { 
                ItemScriptableObj outputKitchenObjectSO = GetOutputForInput(GetKitchenObj().GetKitchenObjectSO());

                GetKitchenObj().DestroySelf();

                KitchenObj.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
            
        }
    }

    private bool hasRecipeWithInput(ItemScriptableObj inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private ItemScriptableObj GetOutputForInput(ItemScriptableObj inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(ItemScriptableObj inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
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
