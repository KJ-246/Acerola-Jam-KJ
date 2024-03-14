using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{
    [field: Header("Genral Sfx")]
    [field: SerializeField] public EventReference popSfx { get; private set; }
    [field: SerializeField] public EventReference thudSfx { get; private set; }
    [field: SerializeField] public EventReference swoosh { get; private set; }
    [field: SerializeField] public EventReference kahChing { get; private set; }
    [field: SerializeField] public EventReference wrong { get; private set; }
    [field: SerializeField] public EventReference hoodedFigureTalking { get; private set; }
    [field: SerializeField] public EventReference sizzling { get; private set; }
    [field: SerializeField] public EventReference countdownTimerBoops { get; private set; }




    [field: Header("Knife Slice Sfx")]
    [field: SerializeField] public EventReference sliceSfx { get; private set; }
    [field: SerializeField] public EventReference splatSfx { get; private set; }


    [field: Header("Music Sfx")]
    [field: SerializeField] public EventReference music { get; private set; }



    public static FmodEvents instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
