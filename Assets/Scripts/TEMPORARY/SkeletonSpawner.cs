using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour {

    public float spawnInterval = 10f;
    private float spawnIncrease = 0.02f;

    private float lastSpawn = -10;


    // Update is called once per frame
    void Update() {
        if (spawnInterval + lastSpawn < Time.time) {
            lastSpawn = Time.time;
            Instantiate(Database.Singleton.GetEntityPrefab("shotgun_skele"), transform.position, transform.rotation);
        }



    }
}
