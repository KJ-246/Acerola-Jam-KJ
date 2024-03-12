using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour, IKitchenObjectParent
{

    private KitchenObj kitchenObject;

    private KitchenObj objectBeingHeld;

    public Transform kitchenObjectHoldPoint;

    public CustomCursor customCursor;

    private void Awake()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {
        //if (customCursor.HasKitchenObj()) {
            //objectBeingHeld = customCursor.GetKitchenObj();

            //if (!customCursor.GetKitchenObj().TryGetPlate(out PlateKitchenObj plateKitchenObject)) { 
                //objectBeingHeld.spriteRenderer.sortingOrder = 30;
            //}
        //}

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
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
