using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObj : MonoBehaviour
{
    public ItemScriptableObj kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    //public ItemScriptableObj GetItemScriptableObj() {
        //return kitchenObjectSO;
    //}

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

    public ItemScriptableObj GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObj plateKitchenObject) {
        if (this is PlateKitchenObj)
        {
            plateKitchenObject = this as PlateKitchenObj;
            return true;
        }
        else {
            plateKitchenObject = null;
            return false;
        }
    }



    public static KitchenObj SpawnKitchenObject(ItemScriptableObj kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        KitchenObj kitchenObj = kitchenObjectTransform.GetComponent<KitchenObj>();
        
        kitchenObj.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObj;
    }
}
