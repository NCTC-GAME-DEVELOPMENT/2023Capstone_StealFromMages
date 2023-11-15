using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private readonly WeaponScriptableObject basicWeapon;
    [SerializeField]
    private PlayerMana playerMana;
    InputData input;
    [SerializeField]
    private List<WeaponScriptableObject> weapons;
    [SerializeField]
    private float triggerActAt = .9f;
    [SerializeField]
    private NormalProjectileScriptableObject TempProjectile;
    [SerializeField]
    private uint castCooldown;
    [SerializeField]
    private bool isOnCooldown;
    private int weaponSlot = -1;
    void Start()
    {
        weapons = new List<WeaponScriptableObject>();
        if (basicWeapon != null) {
            weapons.Add(basicWeapon);
            weaponSlot = 0;
        }
        if (playerMana == null)
            playerMana = GetComponent<PlayerMana>();
    }
    void Update()
    {
        input = InputPoller.reference.GetInput(0);
        if (input.buttonEast) {
            if (++weaponSlot !< weapons.Count) {
                weaponSlot--;
            } if (weaponSlot !> -1) {
                weaponSlot = 0;
            }
        }
        else if (input.buttonWest){

        }
        if (input.rightTrigger > triggerActAt && playerMana.UseMana() && isOnCooldown) {
            Vector2 relative = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(new Vector3(input.rightStick.x, input.rightStick.y, 1)));
            Debug.Log(relative);
            ProjectileHandler.Instance.ShootProjectile(this.transform.position, relative.normalized, TempProjectile, ProjectileHandler.ProjectileTarget.Enemy);
            TickSystem.Instance.CreateTimer(ResetCooldown, castCooldown);
            isOnCooldown = false;
        }
        //float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
    }
    public void ResetCooldown() {
        isOnCooldown = true;
    }

}
