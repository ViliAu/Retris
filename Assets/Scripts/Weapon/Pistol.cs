using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {
    
    [Header("Alt fire")]
    [SerializeField] private Projectile altProjectile = null;

    public override void Equip() {
        base.Equip();
    }

    public override void Fire() {
        if (!base.CanFire(base.fire)) {
            return;
        }
        base.Fire();
    }

    public override void AltFire() {
        if (!base.CanFire(base.altFire)) {
            return;
        }
        base.AltFire();
    }

    public override void Reload() {
        base.Reload();
    }

}
