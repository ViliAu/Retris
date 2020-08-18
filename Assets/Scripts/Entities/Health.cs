using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] protected float maxHealth = 100;
    protected float health = 100;

    protected virtual void Awake() {
        health = maxHealth;
    }

    public virtual void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public virtual void AddHealth(float amount) {
        health += amount;
        health = health > maxHealth ? maxHealth : health;
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

}
