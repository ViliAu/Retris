using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    [SerializeField] private Vector2 swing = default;

    private MeleeAnimator meleeAnimator = null;
    private bool swinging = false;

    private Transform[] bladeRay = new Transform[2]; 
    private List<int> targets = new List<int>();

    protected override void Start() {
        base.Start();
        meleeAnimator = GetComponent<MeleeAnimator>();
        bladeRay[0] = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("BladeRay.A");
        bladeRay[1] = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).Find("BladeRay.B");
    }

    void Update() {
        CastOverlap();
        Debug.DrawLine(bladeRay[0].transform.position, bladeRay[1].transform.position);
    }

    private void CastOverlap() {
        if (!swinging) {
            return;
        }
        Collider[] cols;
        cols = Physics.OverlapCapsule(bladeRay[0].transform.position, bladeRay[1].transform.position, 0.07f, fire.hitMask, QueryTriggerInteraction.Ignore);
        if (cols.Length > 0) {
            HitTargets(cols);
        }
    }

    private void HitTargets(Collider[] cols) {
        Health h;
        for (int i = 0; i < cols.Length; i++) {
            if (targets.Count == 0) {
                if ((h = cols[i].GetComponent<Health>()) != null) {
                        GiveDamage(h);
                }
                targets.Add(cols[i].gameObject.GetInstanceID());
                continue;
            }
            for (int j = 0; j < targets.Count; j++) {
                if (cols[i].gameObject.GetInstanceID() == targets[j]) {
                    continue;
                }
            }
            if ((h = cols[i].GetComponent<Health>()) != null) {
                GiveDamage(h);
            }
            targets.Add(cols[i].gameObject.GetInstanceID());
        }
    }

    private void GiveDamage(Health health) {
        health.TakeDamage(fire.damage);
        SoundSystem.PlaySound2D("impact_shit");
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

    public override void Fire() {
        if (!CanFire(fire)) {
            return;
        }
        meleeAnimator.PlayFireAnimation();
        SoundSystem.PlaySound2D(fireSound.name);
        fire.lastFired = Time.time;

        // Melee logic
        swinging = true;
        Invoke("StartSwing", swing.x);
        Invoke("CancelSwing", swing.y);
    }

    private void StartSwing() {
        swinging = true;
    }

    private void CancelSwing() {
        swinging = false;
        targets.Clear();
    }

    public override void AltFire() {
        meleeAnimator.PlayAltFireAnimation();
        SoundSystem.PlaySound2D(altFireSound.name);
    }

    public override void Reload() {
        meleeAnimator.PlayReloadAnimation();
        SoundSystem.PlaySound2D(reloadSound.name);
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
