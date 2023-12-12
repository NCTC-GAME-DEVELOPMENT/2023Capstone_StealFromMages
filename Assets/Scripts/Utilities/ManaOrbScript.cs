using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaOrbScript : MonoBehaviour
{
    [SerializeField]
    private Sprite fullOrb, halfOrb, emptyOrb;
    private Image orbSprite;
    public void SetOrbState(OrbState state) {
        switch (state) {
            case OrbState.Empty:
                orbSprite.sprite = emptyOrb;
                break;
            case OrbState.Half:
                orbSprite.sprite = halfOrb;
                break;
            case OrbState.Full:
                orbSprite.sprite = fullOrb;
                break;
        }
    }
    public void Awake() {
        orbSprite = GetComponent<Image>();
    }
}
public enum OrbState {
    Empty,
    Half,
    Full
}
