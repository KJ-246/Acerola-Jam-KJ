using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class MainMenue : MonoBehaviour
{
    private EventInstance mainMenueMusic;


    private void Start()
    {
        mainMenueMusic = AudioManager.instance.CreateInstance(FmodEvents.instance.mainMenueMusic);

        mainMenueMusic.start();
    }

    public void PlayPopSound() {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.popSfx);
    }

    public void LoadSceneMainScene() {

        PLAYBACK_STATE playbackState;
        mainMenueMusic.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.PLAYING))
        {
            mainMenueMusic.stop(STOP_MODE.ALLOWFADEOUT);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        
    }

    public void LoadSceneMainMenue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
