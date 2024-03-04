using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{

    public ItemScriptableObj input;
    public ItemScriptableObj output;
    public float fryingTimerMax;

}
