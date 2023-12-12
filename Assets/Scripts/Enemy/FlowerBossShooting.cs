using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
/* 
    Beam attack uses a raycast to determin Length of Beam???
    
 
 */
public class FlowerBossShooting : MonoBehaviour
{
    #region Fields and Properties
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
    [SerializeField]
    private MegaLaserScript MegaLaser;
    [SerializeField]
    private VineGate vineGate;
    private bool isAggro;
    #endregion
    public void Start() {
        attackPhase = AttackPhase.Individual;
        isAggro = false;
        if (vineGate != null )
            vineGate.OnStartFight += TurnAggro;
        else 
            TurnAggro();
    }
    public void TurnAggro() {
        if (!isAggro) {
            TickSystem.Instance.CreateTimer(ChooseAttackPhase, (int)10);
            IsOffCooldown = true;
            isAggro = true;
            TickSystem.Instance.CreateTimer(SwitchLaserCoolDown, 20);
        }
    }
    public float GetMegaCharge() => megaCharge; 

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
                    TickSystem.Instance.CreateTimer(SwitchCoolDown, TotalAttackTime + 2);
                    break;
                case AttackPhase.Group:
                    IsOffCooldown = false;
                    for (int i = 0; i < petalTransforms.Count; i++) {
                        petalTransforms[i].GetComponent<FlowerBossPetalShooter>().Shoot();
                    }
                    TotalAttackTime = petalTransforms.Count;
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
                    TickSystem.Instance.CreateTimer(SwitchCoolDown, TotalAttackTime + 2);
                    break;
                case AttackPhase.Mega:
                    megaChargeStart += Time.deltaTime;
                    megaCharge = megaChargeStart / megaChargeTime;
                    if (megaCharge > 2) {
                        MegaLaser.Shoot(petalTransforms.Count);
                        Debug.Log("Mega Laser");
                        IsMegaOffCooldown = false;
                        IsOffCooldown = false;
                        TickSystem.Instance.CreateTimer(SwitchCoolDown, 3);
                        TickSystem.Instance.CreateTimer(SwitchLaserCoolDown, 12);
                        ChooseAttackPhase();
                    }
                    break;
            }
        }
    }
    public void SwitchCoolDown() {
        IsOffCooldown = true;
    }
    public void SwitchLaserCoolDown() {
        IsMegaOffCooldown = true;
    }
    public void ChooseAttackPhase() {
        if (!IsMegaOffCooldown) {
            attackPhase = (AttackPhase)Random.Range(0, 2);
        }
        else {

            attackPhase = Random.Range(0,1) == 1 ? (AttackPhase)Random.Range(0, 2) : (AttackPhase)3;
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
