using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour
{
    [SerializeField]
    private Sprite fullHeart, halfHeart, emptyHeart;
    private Image heartSprite;
    public void SetHeartState(HeartState state) {
        switch (state) {
            case HeartState.Empty:
                heartSprite.sprite = emptyHeart;
                break;
            case HeartState.Half: 
                heartSprite.sprite = halfHeart;
                break;
            case HeartState.Full: 
                heartSprite.sprite = fullHeart;
                break;
        }
    }
    public void Awake() {
        heartSprite = GetComponent<Image>();
    }
}
public enum HeartState {
    Empty,
    Half,
    Full
}
