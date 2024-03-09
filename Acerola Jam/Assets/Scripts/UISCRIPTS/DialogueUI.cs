using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{

    
    public float waitTimer = 3f;
    public bool wait;
    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        Hide();
        GameValues.Instance.OnStateChanged += GameValues_OnStateChanged;
    }

    private void Update()
    {
        if (wait) {
            waitTimer -= Time.deltaTime;
        }

        if (waitTimer <= 0) {
            Show();
        }
    }

    private void GameValues_OnStateChanged(object sender, EventArgs e)
    {
        if (GameValues.Instance.IsIntro()) {
            wait = true;
        }
    }

    private void Show()
    {
        dialogueBubble.SetActive(true);
    }

    private void Hide()
    {
        dialogueBubble.SetActive(false);
    }
}
