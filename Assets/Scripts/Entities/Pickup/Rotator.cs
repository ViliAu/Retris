#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    [Header("Rotation")]
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] Vector3 rotateAxis = new Vector3(0, 1, 0);
    [SerializeField] float hoverSpeed = 0.7f;
    [SerializeField] float hoverAmount = 0.1f;

    float hover;
    Vector3 org;

    void Awake() {
        org = transform.position;    
    }

    void Update() {
        Rotate();
        Hover();
    }

    void Rotate() {
        transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
    }

    void Hover() {
        hover += hoverSpeed * Time.deltaTime;
        transform.position = org + Vector3.up * Mathf.Sin(hover) * hoverAmount;
    }
}
