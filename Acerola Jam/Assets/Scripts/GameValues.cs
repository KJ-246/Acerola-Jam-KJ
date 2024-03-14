using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMOD.Studio;

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
    private bool giveExtraTime;
    public int extraTime = 0;
    private int payout = 0;
    public TextMeshProUGUI moneyEarnedText;
    public TextMeshProUGUI goalMoneyText;
    public GameObject Shop;
    public GameObject mainUI;
    private bool dayOverFadeOut;

    [Header("Upgrades")]
    private bool boughtMoreStorage = false;
    public GameObject moreStorageObjects;



    [Header("Day Cycle")]
    //public float dayTimer;
    //public float dayTimerMax = 120f;
    public int DayNum = 1;
    public bool stopSpawning = false;

    public Animator viewFade;
    public Animator dayOverFadeOutAnimator;
    public bool stopFade;
    private GameObject gameObjectToSwitchTo;
    public GameObject stoveArea;
    private float betweenFadeTimermax = 1f;
    private float betweenFadeTimer;
    private bool startFadeTimer;
    public Transform cameraStovePosition;
    public Transform defaultCameraPosition;

    public GameObject cam;

    private State state;
    private bool introOver;


    [Header("Game Timers")]

    private float waitingToStartTimer = 1f;
    public float waitingToStartTimerMax = 1f;

    private float countdownToStartTimer = 10f;
    public float countdownToStartTimerMax = 10f;

    public float gamePlayingTimer;
    public float gamePlayingTimerMax = 120f;

    private float gameOverTimer = 10f;
    public float gameOverTimerMax = 5f;

    //AUDIO STUFF
    private EventInstance music;
    private float parameterValue;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        music = AudioManager.instance.CreateInstance(FmodEvents.instance.music);

        giveExtraTime = true;
        boughtMoreStorage = false;
        moreStorageObjects.SetActive(false);
        //SurplusMoneyText.enabled = false;
        Shop.SetActive(false);
        mainUI.SetActive(true);

        //DayNum = 1;

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
        if (gamePlayingTimer <= 10 && state == State.GamePlaying)
        {
            parameterValue += Time.deltaTime;
        }
        else {
            parameterValue = 0;
        }

        UpdateSound();

        switch (state) {
            case State.WaitingToStart:
                //mainUI.SetActive(true);
                EnableInteractions();
                stopSpawning = false;
                waitingToStartTimer -= Time.deltaTime;
                goalMoneyText.text = ("Goal: $ " + goalMoney);
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
                DisableInteractions();
                SwitchViewsTest(Areas[0]);
                gameOverTimer -= Time.deltaTime;
                stopSpawning = true;
                if (currentMoney >= goalMoney || DayNum == 1) {
                    state = State.Intro;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    giveExtraTime = true;
                    gamePlayingTimerMax = 120;
                    GiveExtraTime();
                    gamePlayingTimerMax += extraTime;
                    extraTime = 0;
                }
                if (gameOverTimer < 0f)
                {
                    

                    if (currentMoney < goalMoney)
                    {
                        
                        state = State.GameOver;
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
                    currentMoney = 0;
                    moneyEarnedText.text = "Current Money: $" + currentMoney;

                    //REMOVED FOR TESTING UNREMOVE THIS LATER
                    state = State.WaitingToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    introOver = false;
                    
                }
                break;
            case State.BuyingPhase:
                break;
            case State.GameOver:
                Debug.Log("You lost");
                break;
        }

        switch (DayNum) {
            case 1:
                goalMoney = 0;
                goalMoneyText.enabled = false;
                break;
            case 2:
                goalMoneyText.enabled = true;
                goalMoney = 25;
                break;
            case 3:
                goalMoney = 40;
                break;
            case 4:
                goalMoney = 50;
                break;
            case 5:
                goalMoney = 60;
                break;
        }

        if (startFadeTimer) {
            betweenFadeTimer -= Time.deltaTime;
            stopFade = true;
            if (betweenFadeTimer <= 0) {
                startFadeTimer = false;
                stopFade = false;
            }
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

    public void SwitchAfterFade() { 
        foreach (GameObject areas in Areas) {

            if (gameObjectToSwitchTo == stoveArea)
            {
                cam.transform.position = cameraStovePosition.transform.position;
            }
            else {
                cam.transform.position = defaultCameraPosition.transform.position;
            }
            if (areas == gameObjectToSwitchTo) {
                gameObjectToSwitchTo.SetActive(true);
                continue;
            }
            if (areas != stoveArea) { 
                areas.SetActive(false);
            }
            
        }
    }

    public void SwitchViewsTest(GameObject areaToSwitchTo)
    {
        if (areaToSwitchTo != gameObjectToSwitchTo && !stopFade) { 
            Fade();
            startFadeTimer = true;
            betweenFadeTimer = betweenFadeTimermax;
            gameObjectToSwitchTo = areaToSwitchTo;
        }
        
    }

    public void GiveExtraTime() {
        if (giveExtraTime && DayNum != 1) {
            extraTime = currentMoney -= goalMoney;
            Debug.Log(extraTime);
            giveExtraTime = false;
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

    public void EnableInteractions()
    {
        foreach (Collider2D collider in AreaColliders)
        {
            collider.enabled = true;
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

    public void Fade() {
        viewFade.SetTrigger("isTransitioning");
        AudioManager.instance.PlayOneShot(FmodEvents.instance.swoosh);
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

    public void CloseShop() {
        ResetTimers();
        state = State.WaitingToStart;
        goalMoneyText.text = ("Goal: $ " + goalMoney);
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateSound() {
        if (state == State.GamePlaying)
        {
            music.setParameterByName("DayAlmostOver", parameterValue);

            PLAYBACK_STATE playbackState;
            music.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                music.start();
            }
        }
        else {
            music.stop(STOP_MODE.ALLOWFADEOUT);
        }

    }
}
