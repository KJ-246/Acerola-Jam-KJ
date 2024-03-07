using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    public Image image;

    public void SetKitchenObjectSO(ItemScriptableObj kitchenObjectSO) {
        image.sprite = kitchenObjectSO.itemSprite;
    }
}
