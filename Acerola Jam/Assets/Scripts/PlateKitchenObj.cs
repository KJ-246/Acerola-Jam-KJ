using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObj : KitchenObj
{

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public ItemScriptableObj kitchenObjectSO;
    }

    public List<ItemScriptableObj> validKitchenObjectSOList;

    private List<ItemScriptableObj> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<ItemScriptableObj>();
        //validKitchenObjectSOList = new List<ItemScriptableObj>();
    }

    public bool TryAddIngredient(ItemScriptableObj kitchenObjectSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            //Not a valid ingredient
            return false;
        }
        //if (kitchenObjectSOList.Contains(kitchenObjectSO))
        //{
            //return false;
        //}
        else { 
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { 
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
        
    }

    public List<ItemScriptableObj> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }
}
