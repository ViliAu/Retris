using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideRocket : Projectile {
    
    [Header("Explosion attributes")]
    [SerializeField] private float radius = 2f;
    [SerializeField] private float force = 1000f;
    [SerializeField] private ParticleSystem particles = null;
    [SerializeField] private float particleLifeTime = 0.8f;

    protected override void DestroyProjectile() {
        Explosion.SetupExplosion(transform.position, damage, radius, force, hitMask, particles, particleLifeTime);
        base.DestroyProjectile();
    }

}
