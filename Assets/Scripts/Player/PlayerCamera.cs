using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public Transform head = null;
    private Vector2 camEuler = default;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector3 offset = default;

    private void Start() {
        //head = transform.Find("Head");
        head = transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        UpdateCamera();
    }

    private void UpdateCamera() {
        // Get vars
        camEuler.x -= EntityManager.Player.Player_Input.mouseInput.y * EntityManager.Player.Player_Input.sensitivity * Time.deltaTime;
        camEuler.y += EntityManager.Player.Player_Input.mouseInput.x * EntityManager.Player.Player_Input.sensitivity * Time.deltaTime;

        // Clamp vertical look
        camEuler.x = Mathf.Clamp(camEuler.x, -89, 89);

        EntityManager.Player.transform.rotation = Quaternion.Euler (new Vector3(0, camEuler.y, 0));
        transform.rotation = Quaternion.Euler (new Vector3(camEuler.x, camEuler.y, 0));
        
        transform.position = Vector3.Lerp(transform.position, EntityManager.Player.transform.position + offset, speed * Time.deltaTime);
    }
}
