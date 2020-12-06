using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    [Header("Weapon list")]
    public Weapon[] weapons = new Weapon[10];

    [Header("Sway")]
    [SerializeField] private float swayAmount = 0.2f;
    [SerializeField] private float swaySpeed = 6f;
    [SerializeField] private Vector2 swayBounds = new Vector2(0.1f, 0.1f);

    private int activeWeapon = 1;
    private Vector3 initialPos = default;

    private void Start() {
        if (weapons[0] == null) {
            Debug.LogError("Player doesn't have any weapons, please add one.");
            return;
        }
        ChangeWeapon(activeWeapon, false);
        initialPos = transform.localPosition;
    }

    private void Update() {
        CheckInput();
        Sway();
    }

    private void Sway() {
        Vector2 swayPos = EntityManager.LocalPlayer.Player_Input.mouseInput * swayAmount;
        swayPos.x = Mathf.Clamp(swayPos.x, -swayBounds.x, swayBounds.x);
        swayPos.y = Mathf.Clamp(swayPos.y, -swayBounds.y, swayBounds.y);
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos - (Vector3)swayPos, swaySpeed * Time.deltaTime);
    }

    private void CheckInput() {
        // Check if we have autofire on
        if (EntityManager.LocalPlayer.Player_Input.autoFire && weapons[activeWeapon].fire.autofire) {
            weapons[activeWeapon].Fire();
        }
        if (EntityManager.LocalPlayer.Player_Input.fired) {
            weapons[activeWeapon].Fire();
            return;
        }
        if (EntityManager.LocalPlayer.Player_Input.altFired) {
            weapons[activeWeapon].AltFire();
            return;
        }
        if (EntityManager.LocalPlayer.Player_Input.reloaded) {
            weapons[activeWeapon].Reload();
            return;
        }
        if (EntityManager.LocalPlayer.Player_Input.pressedNum != -1) {
            ChangeWeapon(EntityManager.LocalPlayer.Player_Input.pressedNum, false);
        }
    }

    private void ChangeWeapon(int num, bool scroll) {
        num--;
        if (weapons[num] == null) {
            if (scroll) {
                ChangeWeapon(num++, true);
            }
            else return;
        }
        weapons[activeWeapon].gameObject.SetActive(false);
        activeWeapon = num;
        weapons[activeWeapon].gameObject.SetActive(true);
        weapons[activeWeapon].Equip();
    }
}
