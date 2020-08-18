using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private PlayerCamera playerCamera = null;

    private PlayerController pc;
    public PlayerController Player_Controller {
        get {
            if (pc == null) {
                pc = transform.GetComponent<PlayerController>();
            }
            return pc;
        }
    }

    private PlayerInput pi;
    public PlayerInput Player_Input {
        get {
            if (pi == null) {
                pi = transform.GetComponent<PlayerInput>();
            }
            return pi;
        }
    }

    private PlayerCamera pcam;
    public PlayerCamera Player_Camera {
        get {
            if (pcam == null) {
                pcam = FindObjectOfType<PlayerCamera>();
                if (pcam == null) {
                     if (playerCamera == null) {
                        Debug.LogError("PlayerCamera not assigned to player");
                    }
                    pcam = Instantiate(playerCamera, transform.position, transform.rotation);
                }
            }
            return pcam;
        }
    }

    private PlayerWeapon pwep;
    public PlayerWeapon Player_Weapon {
        get {
            if (pwep == null) {
                pwep = transform.Find("Head").Find("Gunhold").GetComponent<PlayerWeapon>();
            }
            return pwep;
        }
    }

    private PlayerHealth phea;
    public PlayerHealth Player_Health {
        get {
            if (phea == null) {
                phea = transform.GetComponent<PlayerHealth>();
            }
            return phea;
        }
    }

}
