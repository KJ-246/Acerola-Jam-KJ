using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance;

    private void Awake()
    {
        instance = this;

        eventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        //InitializeMusic(FmodEvents.instance.music);
    }

    public void PlayOneShot(EventReference sound) {
        RuntimeManager.PlayOneShot(sound);
    }

    public EventInstance CreateInstance(EventReference eventReference) {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void InitializeMusic(EventReference musicEventReference) {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    private void CleanUp() {
        foreach (EventInstance eventInstance in eventInstances) {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
