using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenue : MonoBehaviour
{
    public void LoadSceneMainScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadSceneMainMenue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
