using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    [Header("Weapon attributes")]
    [SerializeField] private int magSize = 10;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private int maxReserve = 60;
    [SerializeField] private LayerMask aimMask = default;

    [Header("Firemodes")]
    [SerializeField] public FireMode fire = new FireMode();
    [SerializeField] public FireMode altFire = new FireMode();

    [Header("Sounds")]
    [SerializeField] protected AudioClip equipSound = null;
    [SerializeField] protected AudioClip fireSound = null;
    [SerializeField] protected AudioClip altFireSound = null;
    [SerializeField] protected AudioClip reloadSound = null;

    [Header("Data")]
    [SerializeField] protected int currentMag = 0;
    [SerializeField] protected int currentReserve = 0;

    private WeaponAnimator animator = null;
    
    private bool reloading = false;
    protected RaycastHit hit;
    public bool unlocked = false;

    protected virtual void Start() {
        currentMag = magSize;
        animator = GetComponent<WeaponAnimator>();
    }

    /// <summary>
    /// Plays all the visual overhead of equipping (animation, sounds)
    /// </summary>
    public virtual void Equip() {
        if (animator == null) {
            animator = GetComponent<WeaponAnimator>();
        }
        reloading = false;
        animator.SetAnimationId();
        animator.PlayEquipAnimation();
        SoundSystem.PlaySound2D(equipSound.name);
    }

    /// <summary>
    /// Plays all the visual overhead of shooting (animation, sounds), subtracts ammo and sets last fired
    /// </summary>
    public virtual void Fire() {
        currentMag -= fire.ammoConsumed;
        fire.lastFired = Time.time;
        animator.PlayFireAnimation();
        SoundSystem.PlaySound2D(fireSound.name);
        for (int i = 0; i < fire.projectileCount; i++) {
            SpawnProjectile(fire);
        }
    }

    /// <summary>
    /// Plays all the visual overhead of alt shooting (animation, sounds), subtracts ammo and sets last fired
    /// </summary>
    public virtual void AltFire() {
        currentMag -= altFire.ammoConsumed;
        altFire.lastFired = Time.time;
        animator.PlayAltFireAnimation();
        SoundSystem.PlaySound2D(altFireSound.name);
        for (int i = 0; i < altFire.projectileCount; i++) {
            SpawnProjectile(altFire);
        }
    }

    /// <summary>
    /// Plays all the visual overhead of reloading (animation, sounds) and starts a reload coroutine
    /// </summary>
    public virtual void Reload() {
        if (currentReserve <= 0 || currentMag == magSize || reloading) {
            return;
        }
        reloading = true;
        animator.PlayReloadAnimation();
        SoundSystem.PlaySound2D(reloadSound.name);
        StartCoroutine(ReloadTimer());
    }

    /// <summary>
    /// Spawns the projectile(s)
    /// </summary>
    protected virtual void SpawnProjectile(FireMode fireMode) {
        Projectile clone = Instantiate(fireMode.projectile, transform.position + transform.rotation * fireMode.bulletOffset, transform.parent.rotation) as Projectile;
        if (Physics.Raycast(transform.parent.position + transform.parent.forward, transform.parent.forward, out hit, 1000, aimMask, QueryTriggerInteraction.Ignore)) {
            clone.transform.LookAt(hit.point);
        }
        clone.SetupProjectile(fireMode.damage, fireMode.velocity, fireMode.lifespan, fireMode.hitMask);
        clone.transform.rotation *= GetSpread(fireMode.spread);
    }

    protected virtual bool CanFire(FireMode fireMode) {
        if (reloading || fireMode.lastFired + (60 / fireMode.firerate) > Time.time) {
            return false;
        }
        else if (currentMag < fireMode.ammoConsumed) {
            Reload();
            return false;
        }
        else {
            return true;
        }
    }

    private IEnumerator ReloadTimer() {
        yield return new WaitForSecondsRealtime(reloadTime);
        reloading = false;
        // Check reload conditions
        if (currentReserve - (magSize - currentMag) < 0) {
            magSize = currentReserve;
            currentReserve = 0;
        }
        else {
            currentReserve -= magSize - currentMag;
            currentMag = magSize;
        }
        
    }

    protected virtual Quaternion GetSpread(float spread) {
        return Quaternion.Euler(transform.parent.rotation.x + Random.Range(-spread*0.5f, spread*0.5f) , transform.parent.rotation.y + Random.Range(-spread*0.5f, spread*0.5f), transform.parent.rotation.z);
    }

    public void AddAmmo(int amount, bool unlock) {
        if (!unlocked && unlock) {
            unlocked = unlock;
        }
        if (currentReserve == maxReserve) {
            return;
        }
        else {
            currentReserve = currentReserve + amount > maxReserve ? maxReserve : currentReserve + amount;
        }
    }

    public bool full() {
        return currentReserve == maxReserve;
    }

    [System.Serializable]
    public struct FireMode {
        [Header("Firemode settings")]
        public bool autofire;
        [Tooltip("Firerate in RPM")] [SerializeField] public float firerate;
        [Tooltip("How many bullets consumed per shot")] [SerializeField] public int ammoConsumed;
        [SerializeField] public float spread;
        [SerializeField] public Vector3 bulletOffset;
        [SerializeField] public LayerMask hitMask;
        [Header("Projectile settings")]
        [SerializeField] public Projectile projectile;
        [SerializeField] public float damage;
        [Tooltip("How many shots per 1 pew")] [SerializeField] public int projectileCount;
        [SerializeField] public float velocity;
        [SerializeField] public float lifespan;
        [HideInInspector] public float lastFired;
    }
}
