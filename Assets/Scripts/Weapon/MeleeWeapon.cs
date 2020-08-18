using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    private MeleeAnimator meleeAnimator = null;

    [SerializeField] private Transform a;
    [SerializeField] private Transform b;

    private void Update() {
        Debug.DrawLine(a.transform.position, b.transform.position);
    }

    protected override void Start() {
        base.Start();
        meleeAnimator = GetComponent<MeleeAnimator>();
    }

    /// <summary>
    /// Plays all the visual overhead of equipping (animation, sounds)
    /// </summary>
    public override void Equip() {
        if (meleeAnimator == null) {
            meleeAnimator = GetComponent<MeleeAnimator>();
        }
        meleeAnimator.SetAnimationId();
        meleeAnimator.PlayEquipAnimation();
        SoundSystem.PlaySound2D(equipSound.name);
    }

    /// <summary>
    /// Plays all the visual overhead of shooting (animation, sounds), subtracts ammo and sets last fired
    /// </summary>
    public override void Fire() {
        if (!CanFire(fire)) {
            return;
        }
        meleeAnimator.PlayFireAnimation();
        SoundSystem.PlaySound2D(fireSound.name);
        fire.lastFired = Time.time;
    }

    /// <summary>
    /// Plays all the visual overhead of alt shooting (animation, sounds), subtracts ammo and sets last fired
    /// </summary>
    public override void AltFire() {
        meleeAnimator.PlayAltFireAnimation();
        SoundSystem.PlaySound2D(altFireSound.name);
    }

    /// <summary>
    /// Plays all the visual overhead of reloading (animation, sounds) and starts a reload coroutine
    /// </summary>
    public override void Reload() {
        meleeAnimator.PlayReloadAnimation();
        SoundSystem.PlaySound2D(reloadSound.name);
    }

    /// <summary>
    /// Spawns the projectile(s)
    /// </summary>
    protected override void SpawnProjectile(FireMode fireMode) {
        Projectile clone = Instantiate(fireMode.projectile, transform.position + transform.rotation * fireMode.bulletOffset, transform.parent.rotation) as Projectile;
        if (Physics.Raycast(transform.parent.position + transform.parent.forward, transform.parent.forward, out hit, 1000, fireMode.hitMask, QueryTriggerInteraction.Ignore)) {
            clone.transform.LookAt(hit.point);
        }
        else {
            clone.transform.LookAt(transform.parent.forward * 100);
        }
        clone.SetupProjectile(fireMode.damage, fireMode.velocity, fireMode.lifespan, LayerMask.GetMask("Default"));
        clone.transform.rotation *= GetSpread(fireMode.spread);
    }

    protected override bool CanFire(FireMode fireMode) {
        if (fireMode.lastFired + (60 / fireMode.firerate) > Time.time) {
            return false;
        }
        else {
            return true;
        }
    }
}
