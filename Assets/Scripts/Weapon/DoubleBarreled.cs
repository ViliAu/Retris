using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarreled : Weapon {

    [Header("Alt fire")]
    [SerializeField] private float force = 1f; 

    public override void Fire() {
        if (!base.CanFire(base.fire)) {
            return;
        }
        base.Fire();
    }

    public override void AltFire() {
        if (!base.CanFire(base.altFire)) {
            return;
        }
        base.AltFire();
        // Throw player
        Vector3 dbForce = -EntityManager.LocalPlayer.Player_Camera.transform.forward *
            Mathf.Clamp01(Mathf.Cos(Vector3.Angle(EntityManager.LocalPlayer.Player_Camera.transform.forward, Vector3.down)*Mathf.PI/180)) * force;
        EntityManager.LocalPlayer.Player_Controller.velocity += dbForce;
    }

    public override void Reload() {
        base.Reload();
    }
}
