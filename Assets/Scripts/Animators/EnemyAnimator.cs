using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {
    
    [SerializeField] private int enemyId = 0;

    private Animator animator;
    private Enemy enemy;

    private void Start() {
        animator = transform.GetComponent<Animator>();
        enemy = transform.GetComponent<Enemy>();
        SetAnimationId();
    }

    public void SetAnimationId() {
        if (animator != null) {
            animator.SetFloat("enemyId", enemyId);
        }
    }

    public void PlayAttackAnimation() {
        if (animator == null) {
            return;
        }
        animator.SetTrigger("Attack");
    }

    public void PlayFlinchAnimation() {
        if (animator == null) {
            return;
        }
        animator.SetTrigger("Flinch");
    }
}
