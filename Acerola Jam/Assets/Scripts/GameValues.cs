using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameValues : MonoBehaviour
{
    public event EventHandler OnStateChanged;

    public static GameValues Instance { get; private set; }

    private enum State { 
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        DayOver,
        BuyingPhase,
        Intro,
        GameOver,
    }

    [Header("Areas")]
    public List<GameObject> Areas;
    public List<GameObject> pages;
    public List<Collider2D> AreaColliders;


    [Header("UI stuff")]

    public int currentMoney = 0;
    public int goalMoney = 35;
    private bool giveSurplusMoney;
    public int surplusMoney = 0;
    private int payout = 0;
    public TextMeshProUGUI moneyEarnedText;
    public TextMeshProUGUI goalMoneyText;
    public TextMeshProUGUI SurplusMoneyText;
    public GameObject Shop;
    public GameObject mainUI;

    [Header("Upgrades")]
    private bool boughtMoreStorage = false;
    public GameObject moreStorageObjects;



    [Header("Day Cycle")]
    //public float dayTimer;
    //public float dayTimerMax = 120f;
    public int DayNum = 1;
    public bool stopSpawning = false;

    public Animator viewFade;
    public bool stopFade;

    private State state;
    private bool introOver;


    [Header("Game Timers")]

    private float waitingToStartTimer = 1f;
    public float waitingToStartTimerMax = 1f;

    private float countdownToStartTimer = 3f;
    public float countdownToStartTimerMax = 3f;

    public float gamePlayingTimer;
    public float gamePlayingTimerMax = 120f;

    private float gameOverTimer = 5f;
    public float gameOverTimerMax = 5f;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        giveSurplusMoney = true;
        boughtMoreStorage = false;
        moreStorageObjects.SetActive(false);
        //SurplusMoneyText.enabled = false;
        Shop.SetActive(false);
        mainUI.SetActive(true);

        DayNum = 1;

        goalMoneyText.text = ("Goal: $ " + goalMoney);

        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;

        OnStateChanged?.Invoke(this, EventArgs.Empty);

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
        switch (state) {
            case State.WaitingToStart:
                //mainUI.SetActive(true);
                stopSpawning = false;
                Shop.SetActive(false);
                SurplusMoneyText.enabled = false;
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f) {
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.DayOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.DayOver:
                //DisableInteractions();
                gameOverTimer -= Time.deltaTime;
                stopSpawning = true;
                if (DayNum == 1) {
                    SurplusMoney();
                    state = State.Intro;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                //DayNum++;
                if (gameOverTimer < 0f)
                {
                    

                    if (currentMoney < goalMoney)
                    {
                        
                        state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else {
                        SurplusMoney();
                        Debug.Log("You Lived");
                        DayNum++;
                        ResetTimers();
                        state = State.WaitingToStart;
                        goalMoneyText.text = ("Goal: $ " + goalMoney);
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
                break;
            case State.Intro:
                if (introOver) { 
                    Debug.Log("You Lived");
                    DayNum++;
                    ResetTimers();
                    goalMoneyText.text = ("Goal: $ " + goalMoney);
                    moneyEarnedText.text = "Current Money: $" + currentMoney;
                    //SurplusMoney();

                    //REMOVED FOR TESTING UNREMOVE THIS LATER

                    state = State.WaitingToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    //state = State.BuyingPhase;
                    //OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.BuyingPhase:
                stopSpawning = true;
                SurplusMoneyText.enabled = true;
                Debug.Log("Buying Phase");
                SurplusMoney();
                Shop.SetActive(true);
                break;
            case State.GameOver:
                Debug.Log("You lost");
                break;
        }
        //Debug.Log(state);
    }

    public bool isGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountDownToStart;
    }

    public bool IsWaitingToStart()
    {
        return state == State.WaitingToStart;
    }

    public bool IsDayOver()
    {
        return state == State.DayOver;
    }

    public bool IsIntro()
    {
        return state == State.Intro;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public bool isStopSpawning() {
        return stopSpawning;
    }

    public bool isBuyingPhase()
    {
        return state == State.BuyingPhase;
    }

    public void IntroIsOver() {
        introOver = true;
    }

    public float GetGamePlayingTimerNormalized() {
        return 1- (gamePlayingTimer / gamePlayingTimerMax);
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

    public void SurplusMoney() {
        if (giveSurplusMoney) {
            surplusMoney = Mathf.Abs(currentMoney - goalMoney);
            currentMoney = 0;
            moneyEarnedText.text = "Current Money: $" + currentMoney;
            SurplusMoneyText.text = "Surplus Money: $" + surplusMoney;
            giveSurplusMoney = false;
        }
    }

    public void SwitchTabs(GameObject pageToSwitchTo)
    {
        foreach (GameObject _pages in pages)
        {
            if (_pages == pageToSwitchTo)
            {
                pageToSwitchTo.SetActive(true);
                continue;
            }
            _pages.SetActive(false);
        }
    }

    public void DisableInteractions() {
        foreach (Collider2D collider in AreaColliders) {
            collider.enabled = false;
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

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public void ResetTimers() {
        waitingToStartTimer = waitingToStartTimerMax;
        countdownToStartTimer = countdownToStartTimerMax;
        gameOverTimer = gameOverTimerMax;
        stopSpawning = false;
    }

    //UPGRADES

    public void StorageUpgrade(int cost) {
        if (!boughtMoreStorage) {
            moreStorageObjects.SetActive(true);
            surplusMoney -= cost;
            SurplusMoneyText.text = "Surplus Money: $" + surplusMoney;
        }
        boughtMoreStorage = true;
    }

    public void CloseShop() {
        ResetTimers();
        state = State.WaitingToStart;
        goalMoneyText.text = ("Goal: $ " + goalMoney);
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

}
