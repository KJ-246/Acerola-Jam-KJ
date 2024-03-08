using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenue : MonoBehaviour
{
    public Canvas pauseMenue;
    public KeyCode pauseButton;

    bool isPaused;

    private void Start()
    {
        pauseMenue.enabled = false;
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseButton)) {
            PauseMenueEnabler();
        }
    }

    public void PauseMenueEnabler() {
        if (!isPaused)
        {
            pauseMenue.enabled = true;
            isPaused = true;
        }
        else {
            
            pauseMenue.enabled = false;
            isPaused = false;
        }
    }

    public void ExitToTitle() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}