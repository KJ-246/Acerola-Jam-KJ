using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI DayNumText;


    private void Start()
    {
        GameValues.Instance.OnStateChanged += GameValues_OnStateChanged;

        Hide();
    }

    private void GameValues_OnStateChanged(object sender, EventArgs e)
    {
        if (GameValues.Instance.IsGameOver())
        {
            DayNumText.text = ("Day: " + GameValues.Instance.DayNum);
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
