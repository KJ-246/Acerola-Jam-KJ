using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    private bool playtalkingSoundAtStart;

    private float waitTimer = 4f;
    public bool wait;

    public Animator hoodedFigureAnimator;

    public int dialogue;

    public GameObject dialogueBubble;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        playtalkingSoundAtStart = true;
        
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
            switchDialogue();
            wait = false;
        }

        if (!wait && playtalkingSoundAtStart && GameValues.Instance.IsIntro()) {
            AudioManager.instance.PlayOneShot(FmodEvents.instance.hoodedFigureTalking);
            playtalkingSoundAtStart = false;
        }

        //timeBetweenDialogueBubbles <= 0
        if (Input.GetKeyDown(KeyCode.Space) && dialogue != 7 && !wait) {
            dialogue++;
            AudioManager.instance.PlayOneShot(FmodEvents.instance.hoodedFigureTalking);
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
                dialogueText.text = "Feed my children. They are growing. They need lots of food. Give them whatever they ask for. They will begin to change, to aberrate. Do not be afraid. This is" +
                    " simply the course of fate...";
                break;
            case 3:
                dialogueText.text = "I will be testing you to ensure that are are truly worthy of surving the dawn of the " +
                    "new world.";
                break;
            case 4:
                dialogueText.text = "At the end of every day I will be here.If you make enough money to satify me you will pass. " +
                    "If you dont, you will die.";
                break;
            case 5:
                dialogueText.text = "I belive you are capable. Prove to me you are. Perhaps if you are succesful you can also have a new form... ";
                break;
            case 6:
                dialogueText.text = " I must leave now. If you make $" + GameValues.Instance.goalMoney + ", you will survive the test. See you tonight.";
                break;
            case 7:
                GameValues.Instance.currentMoney = 0;
                GameValues.Instance.IntroIsOver();
                waitTimer = 6;
                Hide();
                dialogue = 8;
                break;
            case 8:
                break;
        }
    }

    private void GameValues_OnStateChanged(object sender, EventArgs e)
    {
        if (GameValues.Instance.IsIntro()) {
            wait = true;
            hoodedFigureAnimator.SetTrigger("dayOver");
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
        switchDialogue();
    }
}
