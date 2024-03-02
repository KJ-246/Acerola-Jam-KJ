using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameValues : MonoBehaviour
{
    [Header("Areas")]
    public GameObject servingArea;
    public GameObject storageArea;
    public GameObject counterArea;


    public Animator viewFade;
    public bool stopFade;

    [Header("UI")]
    public Image customCursor;

    private void Start()
    {
        

        stopFade = false;
        storageArea.SetActive(false);
        servingArea.SetActive(true);
    }

    private void Update()
    {
        
    }
    //Everything is really bad please change it later

    public void SwitchViewsMain() {
        servingArea.SetActive(true);
        storageArea.SetActive(false);
        counterArea.SetActive(false);
    }
    public void SwitchViewsStorage()
    {
        servingArea.SetActive(false);
        storageArea.SetActive(true);
        counterArea.SetActive(false);
    }
    public void SwitchViewsCounter()
    {
        servingArea.SetActive(false);
        storageArea.SetActive(false);
        counterArea.SetActive(true);
    }

    public void StopFade() {
        viewFade.SetBool("isTransitioning", false);
    }

}
