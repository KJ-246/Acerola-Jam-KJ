using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{
    public int bread = 10;
    public int lettuce = 10;
    public int meat = 10;
    public int sauce = 10;
    public int tomatoe = 10;

    public int currentDay;

    public GameObject servingArea;
    public GameObject storageArea;

    public Animator viewFade;
    public bool stopFade;

    private void Start()
    {
        stopFade = false;
        storageArea.SetActive(false);
        servingArea.SetActive(true);
    }

    private void Update()
    {
        
    }
    //This is really bad please change it later

    public void SwitchViewsMain() {
        servingArea.SetActive(true);
        storageArea.SetActive(false);
    }
    public void SwitchViewsStorage()
    {
        servingArea.SetActive(false);
        storageArea.SetActive(true);
    }

    public void StopFade() {
        viewFade.SetBool("isTransitioning", false);
    }

}
