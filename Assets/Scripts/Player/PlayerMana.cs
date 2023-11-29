using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField]
    private int maxMana;
    [SerializeField]
    private int mana;

    public void Start() {
        TickSystem.Instance.CreateTimer(PassiveRegen, (int)2);
    }
    public bool UseMana(int _cost) {
        Debug.Log(_cost);
        if (mana - _cost > -1) {
            mana -= _cost;
            return true;
        }
        else {
            return false; 
        }
    }
    public float GetManaFill() {
    return mana / maxMana;
    }
    public void PassiveRegen() {
        RegenMana(1);
        TickSystem.Instance.CreateTimer(PassiveRegen, (int)2);
    }
    public void RegenMana(int _amount) {

        mana += _amount;
        if (mana > maxMana) {
            mana = maxMana;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "ManaPotion" && mana != maxMana) {
            RegenMana(10);
            collision.gameObject.SetActive(false);
        }
    }

}
