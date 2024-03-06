using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_Gameobject {

        public ItemScriptableObj kitchenObjectSO;
        public GameObject gameObject;

    }



    public PlateKitchenObj plateKitchenObject;
    public List<KitchenObjectSO_Gameobject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_Gameobject kitchenObjectSOGameobject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameobject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObj.OnIngredientAddedEventArgs e) {
        foreach (KitchenObjectSO_Gameobject kitchenObjectSOGameobject in kitchenObjectSOGameObjectList) {
            if (kitchenObjectSOGameobject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectSOGameobject.gameObject.SetActive(true);
            }
        }
    }
}
