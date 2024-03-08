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
        GameOver,
    }

    [Header("Areas")]
    public List<GameObject> Areas;
    public List<Collider2D> AreaColliders;

    public int currentMoney = 0;
    public int goalMoney = 35;
    private int payout = 0;
    public TextMeshProUGUI moneyEarnedText;

    [Header("Day Cycle")]
    //public float dayTimer;
    //public float dayTimerMax = 120f;
    public int DayNum = 1;
    public bool stopSpawning = false;

    public Animator viewFade;
    public bool stopFade;

    private State state;


    [Header("Game Timers")]

    private float waitingToStartTimer = 1f;
    public float waitingToStartTimerMax = 1f;

    private float countdownToStartTimer = 3f;
    public float countdownToStartTimerMax = 3f;

    private float gamePlayingTimer;
    public float gamePlayingTimerMax = 20f;

    private float gameOverTimer = 5f;
    public float gameOverTimerMax = 5f;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
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
                //DayNum++;
                if (gameOverTimer < 0f)
                {
                    

                    if (currentMoney < goalMoney)
                    {
                        state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else {
                        Debug.Log("You Lived");
                        DayNum++;
                        ResetTimers();
                        state = State.WaitingToStart;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
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

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public bool isStopSpawning() {
        return stopSpawning;
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

}
