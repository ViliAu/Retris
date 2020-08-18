﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float sensitivity = 1f;
    [HideInInspector] public Vector3 input = Vector3.zero;
    [HideInInspector] public Vector2 mouseInput = Vector2.zero;
    [HideInInspector] public bool jumped = false;
    [HideInInspector] public bool fired = false;
    [HideInInspector] public bool autoFire = false;
    [HideInInspector] public bool altFired = false;
    [HideInInspector] public bool reloaded = false;
    [HideInInspector] public int pressedNum = -1;

    private void Update() {
        GetInput();
    }

    private void GetInput() {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        jumped = Input.GetKeyDown(KeyCode.Space);
        fired = Input.GetKeyDown(KeyCode.Mouse0);
        autoFire = Input.GetAxisRaw("Fire1") == 1;
        altFired = Input.GetKeyDown(KeyCode.Mouse1);
        reloaded = Input.GetKeyDown(KeyCode.R);
        GetPressedNum();

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            sensitivity += Input.GetAxis("Mouse ScrollWheel") * 50;
    }

    public void GetPressedNum() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            pressedNum = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            pressedNum = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            pressedNum = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            pressedNum = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            pressedNum = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            pressedNum = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            pressedNum = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) {
            pressedNum = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9)) {
            pressedNum = 9;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0)) {
            pressedNum = 0;
        }
        else pressedNum = -1;
    }
}
