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
    public GameObject stoveArea;


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

    //public void SwitchViews(GameObject areaToSwitchTo, GameObject area2, GameObject area3, GameObject area4, GameObject area5) {
        //areaToSwitchTo.SetActive(true);
        //area2.SetActive(false);
        //area3.SetActive(false);
        //area4.SetActive(false);
        //area5.SetActive(false);
    //}

    public void SwitchViewsMain(GameObject areaToSwitchTo) {
        servingArea.SetActive(false);
        storageArea.SetActive(false);
        counterArea.SetActive(false);
        stoveArea.SetActive(false);
        areaToSwitchTo.SetActive(true);
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
