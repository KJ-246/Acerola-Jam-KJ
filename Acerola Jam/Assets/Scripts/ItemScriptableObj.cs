using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObjects")]
public class ItemScriptableObj : ScriptableObject
{
    public string itemName;
    public Transform prefab;
    public Sprite itemSprite;
    public BoxCollider2D boxCollider;
}
