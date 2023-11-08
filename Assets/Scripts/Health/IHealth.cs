using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth {
    public void ApplyDamage(float _damage);
    public bool ApplyHeal(float _heal);
}
