using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DefaultProjectile", menuName = "DesignerObjects/Projectiles/NormalPlayerProjectile")]
public class WeaponScriptableObject : ScriptableObject
{
    public string Name;
    public int Damage;
    public int Speed;
    public Sprite Sprite;
    public  int Cost;
    //public Projectile projectileType;
}
