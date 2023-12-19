using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected SkeletonAnimation skeleton;
    [SerializeField] private float speed;
    
    public Transform leftPos, rightPos;

    private bool isDead = false, isRight = true;
    private Transform tf;

    private void Awake() {
        tf = transform;
    }

    public virtual void Move() { }
    

    public void ChangeDirection(bool isRight) {
        this.isRight = isRight;
        tf.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    protected virtual void OnDeath() {
        isDead = true;
        StartCoroutine(WaitForDespawn());
    }

    private IEnumerator WaitForDespawn() {
        yield return CacheComponent.GetWFS(Constant.TIME_TO_DESPAWN);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isDead) return;
        if (other.CompareTag(GameTag.PlayerHit.ToString())) {
            if (GameManager.Ins.player.IsFalling) {
                Debug.Log("HI");
                GameManager.Ins.player.HitEnemy();
                OnDeath();
            }
        } else if (other.CompareTag(GameTag.Player.ToString())) {
            Debug.Log("DIE");
        }
    }
}
