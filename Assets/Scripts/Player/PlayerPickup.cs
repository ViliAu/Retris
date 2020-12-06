using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

    [SerializeField] private float range = 1;

    private void Update() {
        Collider[] targetList;
        targetList = CastOverLapSphere();
        ScanPickups(targetList);
    }

    Collider[] CastOverLapSphere() {
        Collider[] targets = Physics.OverlapSphere(transform.position, range);
        return targets;
    }

    private void ScanPickups(Collider[] targets) {
        if (targets.Length == 0)
            return;
        for (int i = 0; i < targets.Length; i++) {
            Pickup p;
            if ((p = targets[i].transform.GetComponent<Pickup>()) != null) {
                ManagePickup(p);
            }
        }
    }

    private void ManagePickup(Pickup p) {
        // Pickup is health
        if (p.weaponType == 0) {
            ManageHealth(p);
            return;
        }
        else {
            ManageAmmo(p);
            return;
        }
    }

    private void ManageHealth(Pickup p) {
        if (EntityManager.LocalPlayer.Player_Health.full()) {
            return;
        }
        else {
            EntityManager.LocalPlayer.Player_Health.AddHealth(p.amount);
            p.DestroyPickup();
        }
    }

    private void ManageAmmo(Pickup p) {
        if (EntityManager.LocalPlayer.Player_Weapon.weapons[p.weaponType].full()) {
            return;
        }
        else {
            EntityManager.LocalPlayer.Player_Weapon.weapons[p.weaponType].AddAmmo(p.amount, p.unlock);
            p.DestroyPickup();
        }
    }
}
