using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DefaultProjectile", menuName = "DesignerObjects/Projectiles/NormalProjectile")]
public class NormalProjectileScriptableObject : ScriptableObject {
    [SerializeField]
    public float Speed;
    [SerializeField]
    public float Damage;
    [SerializeField]
    public Sprite Sprite; 
}
