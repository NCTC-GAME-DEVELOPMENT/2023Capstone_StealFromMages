using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
/* 
    Beam attack uses a raycast to determin Length of Beam???
    
 
 */
public class FlowerBossShooting : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> petalTransforms;
    [SerializeField]
    private bool IsMegaOffCooldown;
    [SerializeField]
    private AttackPhase attackPhase;
    [SerializeField]
    private bool IsOffCooldown;
    [SerializeField]
    private float megaChargeTime;
    private float megaChargeStart;
    private float megaCharge;
    public void Start() {
        attackPhase = AttackPhase.Individual;
        TickSystem.Instance.CreateTimer(ChooseAttackPhase, (int)10);
        IsOffCooldown = true;
        /* Gotta make Sure Boss Gets Aggro'd Before it starts Shooting
         * 
         */
    }
    public float GetMegaCharge() {
        return megaCharge; 
    }
    public void Update() {
        if (IsOffCooldown) {
            int TotalAttackTime = 0;
            switch (attackPhase) {
                case AttackPhase.Cyclic:
                    IsOffCooldown = false;
                    for (int i = 0; i < petalTransforms.Count; i++) {
                        TickSystem.Instance.CreateTimer(petalTransforms[i].GetComponent<FlowerBossPetalShooter>().Shoot, 1+ i*2);
                    }
                    TotalAttackTime = 1 + petalTransforms.Count * 2;
                    Debug.Log("Attack Cooldown : " + TotalAttackTime + " for Attack Type : " + attackPhase);
                    TickSystem.Instance.CreateTimer(SwitchCoolDown, TotalAttackTime + 2);
                    break;
                case AttackPhase.Group:
                    IsOffCooldown = false;
                    for (int i = 0; i < petalTransforms.Count; i++) {
                        petalTransforms[i].GetComponent<FlowerBossPetalShooter>().Shoot();
                    }
                    TotalAttackTime = petalTransforms.Count;
                    Debug.Log("Attack Cooldown : " + TotalAttackTime + " for Attack Type : " + attackPhase);
                    TickSystem.Instance.CreateTimer(SwitchCoolDown, TotalAttackTime + 2);
                    break;
                case AttackPhase.Individual:
                    IsOffCooldown = false;
                    for (int j = 0; j < Random.Range(1, 3); j++) {
                        for (int i = 0; i < petalTransforms.Count; i++) {
                            int time = Random.Range(1, 2) + i + j;
                            TickSystem.Instance.CreateTimer(petalTransforms[i].GetComponent<FlowerBossPetalShooter>().Shoot, time);
                            if (TotalAttackTime < time) {
                                TotalAttackTime = time; 
                            }
                        }
                    }
                    Debug.Log("Attack Cooldown : " + TotalAttackTime +" for Attack Type : " + attackPhase);
                    TickSystem.Instance.CreateTimer(SwitchCoolDown, TotalAttackTime + 2);
                    break;
                case AttackPhase.Mega:
                    megaChargeStart += Time.deltaTime;
                    //Possible Bug if lag occurs before this frame
                    megaCharge = megaChargeStart / megaChargeTime;
                    if (megaCharge > 1) {
                        Debug.Log("Mega Laser");
                        IsMegaOffCooldown = false;
                    }
                    break;
            }
        }
    }
    public void SwitchCoolDown() {
        IsOffCooldown = true;
    }
    public void ChooseAttackPhase() {
        if (!IsMegaOffCooldown) {
            attackPhase = (AttackPhase)Random.Range(0, 2);
        }
        else {
            attackPhase = (AttackPhase)Random.Range(0, 3);
        }
        TickSystem.Instance.CreateTimer(ChooseAttackPhase, Random.Range(8, 16));
    }

    public enum AttackPhase {
        Cyclic,
        Group,
        Individual,
        Mega
    }
}
