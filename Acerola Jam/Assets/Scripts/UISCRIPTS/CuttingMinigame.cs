using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CuttingMinigame : MonoBehaviour
{
    public CuttingCounter cuttingCounter;

    public float cuttingLengthTimer;
    public float maxCuttingLength = 3f;
    public float barSpeed;

    public float goalValueMin = 1f;
    public float GoalValueMax = 1.5f;

    private bool reverse;
    private bool stopMoving;
    public int slicedCorrectValue;

    public Image barImage;
    public Slider goalValSlider;


    private void Start()
    {
        slicedCorrectValue = 3;

        Hide();

        reverse = false;
        stopMoving = false;
        
        cuttingLengthTimer = maxCuttingLength;
        barImage.fillAmount = 0f;

        //THIS WORKS
        //float value = (goalValueMin + GoalValueMax) / 2;
        //goalValSlider.value = 1 - (value/maxCuttingLength);
        UpdateVisual();
    }

    void Update()
    {
        //Kinda like this?
        //Randomization();

        barImage.fillAmount = 1 - (cuttingLengthTimer / maxCuttingLength);
        if (!reverse && !stopMoving) { 
            cuttingLengthTimer -= Time.deltaTime * barSpeed;
            if (cuttingLengthTimer <= 0)
                reverse = true;
        }

        if (reverse && !stopMoving) {
            cuttingLengthTimer += Time.deltaTime * barSpeed;
            if (cuttingLengthTimer >= maxCuttingLength)
                reverse = false;
        }
    }

     public void SLICE() {
        stopMoving = true;

        float slicedVal = cuttingLengthTimer;

        if (slicedVal > goalValueMin && slicedVal < GoalValueMax)
        {
            //Debug.Log("You sliced the correct value: " + slicedVal);
            slicedCorrectValue = 1;
            stopMoving = false;
            cuttingCounter.InteractAlternate();
            SlicesReset();
        }
        else
        {
            //Debug.Log("You sliced the wrong value: " + slicedVal);
            stopMoving = false;
            slicedCorrectValue = 2;
            cuttingCounter.InteractAlternate();
            SlicesReset();
        }
    }

    public void SlicesReset() {
        gameObject.SetActive(false);
        cuttingLengthTimer = 0;
        slicedCorrectValue = 3;
        Randomization();
    }

    public void Randomization() {
        barSpeed = UnityEngine.Random.Range(1f, 5f);
        GoalValueMax = UnityEngine.Random.Range(.5f, 3f);
        goalValueMin = GoalValueMax - 0.5f;
        UpdateVisual();
    }

    void UpdateVisual() {
        float value = (goalValueMin + GoalValueMax) / 2;
        goalValSlider.value = 1 - (value / maxCuttingLength);
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
