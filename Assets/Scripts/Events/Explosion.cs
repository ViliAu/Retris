using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    //TODO Make 

    public static void SetupExplosion(Vector3 position, float damage, float radius, float force, LayerMask hitMask, ParticleSystem explosionParticles, float particleLifetime) {
        Collider[] targets = Physics.OverlapSphere(position, radius, hitMask, QueryTriggerInteraction.Collide);
        if (targets.Length == 0) {
            return;
        }
        else {
            for (int i = 0; i < targets.Length; i++) {
                Health h;
                if ((h = targets[i].transform.GetComponent<Health>()) != null) {
                    h.TakeDamage(damage * Mathf.Abs(1 - radius / (Vector3.Distance(position, h.transform.position))));
                }
                Rigidbody rig;
                if ((rig = targets[i].transform.GetComponent<Rigidbody>()) != null) {
                    rig.AddExplosionForce(force, position, radius);
                }
            }
        }
        if (explosionParticles != null) {
            GameObject clone = Instantiate(explosionParticles.gameObject, position, Quaternion.identity) as GameObject;
            Destroy(clone, particleLifetime);
        }
    }

}
