using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{

    public ItemScriptableObj input;
    public ItemScriptableObj output;
    public float burningTimerMax;

}
