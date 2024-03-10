using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{

    
    public float waitTimer = 6f;
    public bool wait;

    private float timeBetweenDialogueBubbles;
    public float maxTimeBetweenDialogueBubbles;

    private int dialogue;

    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        ResetDialogue();
        dialogue = 1;

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
            timeBetweenDialogueBubbles -= Time.deltaTime;
            switchDialogue();
            wait = false;
        }

        if (timeBetweenDialogueBubbles <= 0 && dialogue != 4) {
            dialogue++;
            ResetDialogue();
        }

    }

    void switchDialogue() {
        switch (dialogue) {
            case 1:
                dialogueText.text = "Hello. You do not know me but I know of you. Unfortunately, you are caught up in something " +
                    "far greater than you know. If you want to live follow my instructions...";
                break;
            case 2:
                dialogueText.text = "Feed my children. They are growing. They need lots of food. Give them whatever they ask for...";
                break;
            case 3:
                dialogueText.text = "I will be testing you to ensure that are are truly worthy of surving the dawn of the " +
                    "new world. At the end of every day I will be here. If you make enough money to satify me you will pass. " +
                    "If you dont, you will die.";
                break;
            case 4:
                dialogueText.text = "I belive you are capable. Prove to me you are. Oh and one last thing i need some money right now" +
                    " so ill be taking this. See you tommorow. Boodbye.";
                if (timeBetweenDialogueBubbles <= 0) { 
                    GameValues.Instance.currentMoney = 0;
                    GameValues.Instance.IntroIsOver();
                    waitTimer = 6;
                    Hide();
                    dialogue = 5;
                }
                break;
            case 5:
                break;
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

    void ResetDialogue() {
        timeBetweenDialogueBubbles = maxTimeBetweenDialogueBubbles;
        switchDialogue();
    }
}
