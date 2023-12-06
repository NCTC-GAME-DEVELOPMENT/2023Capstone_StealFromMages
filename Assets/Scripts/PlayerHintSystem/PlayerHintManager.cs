using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PlayerHintManager : MonoBehaviour
{
    public static PlayerHintManager instance; 
    public bool IsShowingMessage = false;
    public TextMeshProUGUI ColorText;
    public TextMeshProUGUI BlackText;

    public float ShowTime = 4;
    float ShowTimer = 4; 
    public float FadeTime = 2;
    float FadeTimer = 2;

    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        HideText(); 
        //ShowMessage("Test Test Test Test", 4, 2, Color.green); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsShowingMessage)
        {
            return; 
        }

        if (ShowTimer > 0)
        {
            ShowTimer -= Time.deltaTime;
            return; 
        }

        if (FadeTimer > 0)
        {
            FadeTimer -= Time.deltaTime;

            float percent = (FadeTimer / FadeTime);

            Color newColor = ColorText.color;
            newColor.a = percent;
            ColorText.color = newColor;

            newColor = BlackText.color;
            newColor.a = percent;
            BlackText.color = newColor;


            return; 
        }

        HideText(); 
    }

    public void HideText()
    {

        IsShowingMessage = false;
        ColorText.color = Color.clear;
        BlackText.color = Color.clear;
    }

    public void ShowMessage(string newMessage, float showingTime, float fadingTime, Color showColor)
    {
        IsShowingMessage = true;
        ShowTime = showingTime;
        ShowTimer = ShowTime; 
        FadeTime = fadingTime;
        FadeTimer = FadeTime;


        ColorText.text = newMessage;
        ColorText.color = showColor; 

        BlackText.text = newMessage;
        BlackText.color = Color.black; 

    }
}
