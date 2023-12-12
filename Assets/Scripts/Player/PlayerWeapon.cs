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
    [SerializeField]
    private int weaponSlot = -1;

    //audio
    public AudioClip attack1;
    private AudioSource audioSource;

    void Start()
    {       
            weaponSlot = 0;   
        if (playerMana == null)
            playerMana = GetComponent<PlayerMana>();

        //assign audio source
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        input = InputPoller.reference.GetInput(0);
        if (input.buttonEast) {
            weaponSlot += 1;
            if (weaponSlot >= weapons.Count) {
                weaponSlot = 0;
            }
        }
        else if (input.buttonWest){
            weaponSlot -= 1;
             if (weaponSlot < 0) {
                weaponSlot = weapons.Count - 1;
             }

        }
        if (input.rightTrigger > triggerActAt && isOnCooldown && playerMana.UseMana(weapons[weaponSlot].Cost)) {
            Vector2 relative = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(new Vector3(input.rightStick.x, input.rightStick.y, 1)));
            ProjectileHandler.Instance.ShootProjectile(this.transform.position, relative.normalized, weapons[weaponSlot], ProjectileHandler.ProjectileTarget.Enemy);
            TickSystem.Instance.CreateTimer(ResetCooldown, castCooldown);
            isOnCooldown = false;
            //play sound
            audioSource.PlayOneShot(attack1);
        }
        //float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
    }
    public void ResetCooldown() {
        isOnCooldown = true;
    }

}
