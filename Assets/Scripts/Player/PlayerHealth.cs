using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
    }

    public bool full() {
        return health == maxHealth;
    }

    protected override void Die() {
        transform.position = Vector3.up * 2;
        health = maxHealth;
        //FindObjectOfType<SkeletonSpawner>().spawnInterval = 10;
    }

}
