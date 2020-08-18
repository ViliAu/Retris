using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {
    
    private EnemyAnimator animator = null;
    
    protected override void Awake() {
        base.Awake();
        animator = GetComponent<EnemyAnimator>();
    }

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);
        animator.PlayFlinchAnimation();
    }

    protected override void Die() {
        RaycastHit hit;
        Vector3 paunpos = Vector3.zero;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 200, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore)) {
            paunpos = hit.point;
        }
        float rng = Random.Range(0f,3f);
        string ent = (rng < 1) ? "pickup_health" : (rng > 1 && rng < 2) ? "pickup_ammo_pistol" : "pickup_ammo_shotgun";
        Pickup p = Instantiate(Database.Singleton.GetEntityPrefab(ent), paunpos + Vector3.up * 0.5f, transform.rotation) as Pickup;
        base.Die();
    }

}
