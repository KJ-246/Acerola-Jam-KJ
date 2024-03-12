using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public void startFade() {
        GameValues.Instance.SwitchAfterFade();
    }
    
}
