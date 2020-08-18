using UnityEngine;

public class Projectile : MonoBehaviour {

    private float damage;
    /*private float range;*/
    private float velocity;
    private float instantiateMoment;
    private float lifespan;
    private LayerMask layerMask;
    
    /* RAycast stuff */
    Ray ray;
    RaycastHit hit;

    

    GameObject OverlapCheck() {
        Collider[] cols = Physics.OverlapSphere(transform.position, .2f, 0, QueryTriggerInteraction.Collide);
        // Return the first collider that has a health script,  if there's one
        for (int i = 0; i < cols.Length; i++) {
            Health health = cols[i].GetComponent<Health>();
            if (health != null) {
                return health.gameObject;
            }
        }

        // Otherwise return the first object we hit
        if (cols.Length > 0) {
            return cols[0].gameObject;
        }
        return null;
    }
        
    private void Update() {
        CollisionCheck();
        UpdateProjectile();
    }

    void CollisionCheck() {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, velocity * Time.deltaTime, layerMask)) {
            Health health = hit.transform.GetComponent<Health>();
            if (health != null) {
                health.TakeDamage(damage);
                DestroyProjectile();
            }
        }
    }

    void UpdateProjectile() {
        if (Time.time > instantiateMoment + lifespan)
            DestroyProjectile();
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    void DestroyProjectile() {
        Destroy(gameObject);
    }

    public void SetupProjectile(float damage,/* float range,*/ float velocity, float lifespan, LayerMask layerMask) {
        this.damage = damage;
        /*this.range = range;*/
        this.velocity = velocity;
        this.layerMask = layerMask;
        this.lifespan = lifespan;
        instantiateMoment = Time.time;

        // Overlap check
        GameObject go = OverlapCheck();
        if (go != null) {
            Health h = go.GetComponent<Health>();
            if (h != null) {
              h.TakeDamage(damage);  
            }
            DestroyProjectile();
        }
    }
}