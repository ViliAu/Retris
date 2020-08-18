using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleShotgun : Weapon {

    private EnemyAnimator enemyAnimator = null;
    private Transform graphics = null;

    private void Awake() {
        enemyAnimator = GetComponent<EnemyAnimator>();
        graphics = transform.Find("Graphics");
    }

    public override void Fire() {
        if (!base.CanFire(fire)) {
            return;
        }
        fire.lastFired = Time.time;
        enemyAnimator.PlayAttackAnimation();
        SoundSystem.PlaySound(fireSound.name, transform.position);
        for (int i = 0; i < fire.projectileCount; i++) {
            SpawnProjectile(fire);
        }
    }

    protected override void SpawnProjectile(FireMode fireMode) {
        Projectile clone = Instantiate(fireMode.projectile, transform.position, graphics.rotation) as Projectile;
        //clone.transform.LookAt(EntityManager.Player.transform.position);
        clone.SetupProjectile(fireMode.damage, fireMode.velocity, fireMode.lifespan, LayerMask.GetMask("Default"));
        clone.transform.rotation *= GetSpread(fireMode.spread);
    }

    protected override Quaternion GetSpread(float spread) {
        return Quaternion.Euler(graphics.rotation.x + Random.Range(-spread*0.5f, spread*0.5f) , graphics.rotation.y + Random.Range(-spread*0.5f, spread*0.5f), graphics.rotation.z);
    }

}
