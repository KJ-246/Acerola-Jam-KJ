using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameValues : MonoBehaviour
{
    [Header("Areas")]
    public List<GameObject> Areas;
    private int currentMoney = 0;
    private int payout = 0;
    public TextMeshProUGUI moneyEarnedText;


    public Animator viewFade;
    public bool stopFade;


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;

        stopFade = false;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e) {
        PayMoneyforCompletion();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e) {
        UpdatePayout();
    }

    private void Update()
    {
        
    }
    
    public void SwitchViewsTest(GameObject areaToSwitchTo)
    {
        foreach (GameObject areas in Areas) {
            if (areas == areaToSwitchTo) {
                areaToSwitchTo.SetActive(true);
                continue;
            }
            areas.SetActive(false);
        }
    }

    public void UpdatePayout() {
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()) {
            payout = recipeSO.cost;
            moneyEarnedText.text = "Current Money: $" + currentMoney;
        }
    }

    public void PayMoneyforCompletion() {
        currentMoney += payout;
        moneyEarnedText.text = "Current Money: $" + currentMoney;
    }

    public void StopFade() {
        viewFade.SetBool("isTransitioning", false);
    }

}
