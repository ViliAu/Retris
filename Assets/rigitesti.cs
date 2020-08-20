using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigitesti : MonoBehaviour {
    void Update() {
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward*0.01f);
    }
}
