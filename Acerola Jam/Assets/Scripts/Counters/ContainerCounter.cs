using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : MonoBehaviour, IKitchenObjectParent
{

    public event EventHandler OnPlayerGrabbedObject;
         
    public ItemScriptableObj kitchenObjectSO;
    public Transform counterPoint;
    private KitchenObj kitchenObject;

    public void Interact(CustomCursor customCursor)
    {
        if (!customCursor.HasKitchenObj())
        {
            //isnt holding an object
            KitchenObj.SpawnKitchenObject(kitchenObjectSO, customCursor);
            AudioManager.instance.PlayOneShot(FmodEvents.instance.popSfx);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
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
