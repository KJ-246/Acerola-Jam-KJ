using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayClockUI : MonoBehaviour
{
    public Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = GameValues.Instance.GetGamePlayingTimerNormalized();
    }
}
