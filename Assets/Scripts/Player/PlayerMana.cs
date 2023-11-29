using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField]
    private int MaxMana;
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
    public void PassiveRegen() {
        RegenMana(1);
        TickSystem.Instance.CreateTimer(PassiveRegen, (int)2);
    }
    public void RegenMana(int _amount) {

        mana += _amount;
        if (mana > MaxMana) {
            mana = MaxMana;
        }
    }

}
