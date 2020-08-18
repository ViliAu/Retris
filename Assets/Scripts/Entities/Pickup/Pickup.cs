using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Entity {
    [Tooltip("Weapon type, 0 is health")] public int weaponType = 0;
    [Tooltip("Amount")] public int amount = 0;
    [Tooltip("Unlocks the weapon?")] public bool unlock = false;
    public AudioClip pickupSound = null;

    public void DestroyPickup() {
        SoundSystem.PlaySound2D(pickupSound.name);
        Destroy(gameObject);
    }
}
