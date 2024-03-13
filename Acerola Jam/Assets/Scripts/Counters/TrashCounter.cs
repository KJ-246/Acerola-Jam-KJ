using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : MonoBehaviour, IKitchenObjectParent
{
    //public ItemScriptableObj kitchenObjectSO;
    public Transform counterPoint;
    private KitchenObj kitchenObject;
    public Animator trashCanAnimator;

    public void Interact(CustomCursor customCursor)
    {
        if (customCursor.HasKitchenObj())
        {
            //is holding object Object
            customCursor.GetKitchenObj().DestroySelf();
            trashCanAnimator.SetTrigger("Interaction");
            AudioManager.instance.PlayOneShot(FmodEvents.instance.thudSfx);
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
