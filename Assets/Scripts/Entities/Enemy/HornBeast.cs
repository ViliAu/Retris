using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HornBeast : Enemy {

    [Tooltip("How often the AI updates in seconds.")][SerializeField] private float pathFindInterval = 1f;
    [SerializeField] private float attackDistance = 4;
    private EnemyAnimator animator = null;
    private NavMeshAgent nav = null;

    private void Awake() {
        animator = transform.GetComponent<EnemyAnimator>();
        nav = transform.GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(CalculatePath), 0.2f, pathFindInterval);
    }

    
    private void Update() {
        transform.position += Vector3.forward * 0.1f * Time.deltaTime;
    }

    private void CalculatePath() {
        nav.destination = EntityManager.LocalPlayer.transform.position;
        if (Vector3.Distance(transform.position, EntityManager.LocalPlayer.transform.position) < 4) {
            Attack();
        }
    }

    private void Attack() {
        
    }

}
