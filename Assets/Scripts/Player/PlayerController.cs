﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    /* User data */
    public float speed = 15;

    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float airAcceleration = 1f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float gravity = 5f;
    [SerializeField] private float maximumGravity = 120f;
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float jumpHeight = 15f;
    
    /* Memory data */
    [HideInInspector] public Vector3 velocity;
    private Rigidbody rig;

    /* Used in explosions, etc */
    public Vector3 additionalForce = default;

    /* Groundcheck data */
    [SerializeField] LayerMask groundMask = default;
    [SerializeField] private bool isGrounded;
    private CapsuleCollider col;

    /* Initialize vars */
    private void Start() {
        rig = transform.GetComponent<Rigidbody>();
        col = transform.GetComponent<CapsuleCollider>();
        velocity = Vector3.zero;
    }

    private void Update() {
        GroundCheck();
        StepCheck();
        Acceleration();
        Deceleration();
        ApplyGravity();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump(); //Temppi fiksaa
        ApplyVelocity();

        if (transform.position.y < -100)
            transform.position = Vector3.zero;
    }

    /* Creates a velocity vector from given input and current speed */ 
    private void Acceleration() {
        Vector3 dir = EntityManager.Player.Player_Camera.transform.rotation * EntityManager.Player.Player_Input.input;

        if (!isGrounded) {
            AirAcceleration(dir);
            return;
        }
        velocity.x = Mathf.Lerp(velocity.x, speed * dir.x, acceleration * Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, speed * dir.z, acceleration * Time.deltaTime);
    }

    private void AirAcceleration(Vector3 dir) {
        if (EntityManager.Player.Player_Input.input == Vector3.zero) {
            return;
        }
        if (velocity.magnitude > speed) {
            if ((velocity + dir).magnitude > velocity.magnitude) {
                return;
            }
        }
        velocity.x = Mathf.Lerp(velocity.x, speed * dir.x, airAcceleration * Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, speed * dir.z, airAcceleration * Time.deltaTime);
    }

    /* Decelerates player */
    private void Deceleration() {
        if (!isGrounded) {
            return;
        }
        velocity.x = Mathf.Lerp(velocity.x, 0, deceleration * Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, 0, deceleration * Time.deltaTime);
    }

    /* Handles gravity */
    private void ApplyGravity() {
        if (isGrounded) {
            if (velocity.y < 0)
                velocity.y = 0;
        }
        else {
            velocity.y = Mathf.Clamp(velocity.y - gravity * Time.deltaTime, -maximumGravity, 1000);
        }
    }

    /* Handles jumping */
    private void Jump() {
        velocity.y = jumpHeight;
    }

    /* Applies velocity vector to rigidbody (moves the player) */
    private void ApplyVelocity() {
        rig.velocity = velocity;
    }

    /* Checks if the player's grounded */
    private void GroundCheck() {
        RaycastHit hit;
        Vector3 upperPos = transform.position + new Vector3(0, col.height - col.radius, 0);
        Vector3 lowerPos = transform.position + new Vector3(0, col.radius+0.2f, 0);
        if (Physics.CapsuleCast(upperPos, lowerPos, col.radius, -Vector3.up, out hit, 0.5f, groundMask, QueryTriggerInteraction.Ignore)){
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    /* Checks if there's anything in the vicinity to step on */
    private void StepCheck() {
        RaycastHit hit;
        Vector3 stepUpper = transform.position + new Vector3(0, col.height - col.radius+stepHeight, 0);
        Vector3 stepLower = transform.position + new Vector3(0, col.radius+stepHeight, 0);
        if (Physics.CapsuleCast(stepUpper, stepLower, col.radius+0.1f, -Vector3.up, out hit, stepHeight+0.2f, groundMask, QueryTriggerInteraction.Ignore)){
            if(hit.point.y > transform.position.y + 0.005f || hit.point.y < transform.position.y - 0.005f) {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
        }
    }
}