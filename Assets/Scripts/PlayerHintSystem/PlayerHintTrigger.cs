using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHintTrigger : MonoBehaviour
{
    public bool PlayOnce = true;
    bool ignoreRetrigger = false;

    public string Message = "It's Magic!";
    public float ShowTime = 4;
    public float FadeTime = 2;
    public Color TextColor = Color.white;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ignoreRetrigger) { return; }

        ignoreRetrigger = PlayOnce; 
        PlayerHintManager.instance.ShowMessage(Message, ShowTime, FadeTime, TextColor); 
    }
}
