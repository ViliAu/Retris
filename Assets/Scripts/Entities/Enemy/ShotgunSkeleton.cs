using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSkeleton : Enemy {

    [Header("Movement")]
    [SerializeField] private float speed = 0.7f;
    [Tooltip("Random value between x and y.")]
    [SerializeField] private Vector2 height = default;
    [SerializeField] private Vector2 distanceX = default;
    [SerializeField] private Vector2 distanceZ = default;
    [Tooltip("How often the skellu changes his position in seconds")]
    [SerializeField] private float posUpdateInterval = 10;

    private Transform graphics = null;
    private Vector3 targetPos = default;
    private float lastUpdated = 0;
    private SkeleShotgun weapon = null;

    private void Awake() {
        graphics = transform.Find("Graphics");
        targetPos = UpdatePosition();
        weapon = transform.GetComponent<SkeleShotgun>();
    }

    private void Update() {
        UpdateMovement();
        Shoot();
    }

    private void UpdateMovement() {
        if (lastUpdated + posUpdateInterval < Time.time) {
            targetPos = UpdatePosition();
        }
        graphics.transform.LookAt(EntityManager.Player.transform, Vector3.up);
        transform.position = Vector3.Lerp(transform.position, EntityManager.Player.transform.position + targetPos, speed * Time.deltaTime);
    }

    private void Shoot() {
        weapon.Fire();
    }

    private Vector3 UpdatePosition() {
        lastUpdated = Time.time;
        float posX = Random.Range(distanceX.x, distanceX.y);
        posX *= (int)Random.Range(0, 1) > 0.5f ? -1 : 1;
        float posZ = Random.Range(distanceZ.x, distanceZ.y);
        posZ *= (int)Random.Range(0, 1) > 0.5f ? -1 : 1;
        return new Vector3(posX, Random.Range(height.x, height.y), posZ);
    }
}
