using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    private bool startSecondTimer = false;
    private float secondTimer = 1f;

    private void Start()
    {
        GameValues.Instance.OnStateChanged += GameValues_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        if (startSecondTimer == false) {
            secondTimer = 1f;
        }

        if (startSecondTimer == true) {
            secondTimer -= Time.deltaTime;

            if (secondTimer <= 0) {
                AudioManager.instance.PlayOneShot(FmodEvents.instance.countdownTimerBoops);
                secondTimer = 1f;
            }
        }

        countdownText.text = Mathf.Ceil(GameValues.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void GameValues_OnStateChanged(object sender, EventArgs e)
    {
        if (GameValues.Instance.IsCountdownToStartActive())
        {
            startSecondTimer = true;
            AudioManager.instance.PlayOneShot(FmodEvents.instance.countdownTimerBoops);
            Show();
        }
        else {
            startSecondTimer = false;
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
