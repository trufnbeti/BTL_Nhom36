using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation skeleton;
    [SerializeField] private float speed;
    [SerializeField] private GameObject parent;
    
    private Transform tf;

    private IState currentState;
    
    public Transform leftPos, rightPos;
    private Vector3 targetPos;
    private bool isDead = false, isRight;

    public bool IsDead => isDead;

    private void Awake() {
        tf = transform;
    }

    private void Start() {
        OnInit();
    }

    protected virtual void OnInit() {
        ChangeState(new PatrolState());
        isRight = Random.value < 0.5f;
        SetTarget();
        ChangeDirection(isRight);
    }

    public void OnDespawn() {
        if (IsDead) return;
        isDead = true;
        OnDeath();
    }

    public virtual void Move() {
        tf.position = Vector3.MoveTowards(tf.position, targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(tf.position, targetPos) < 0.1f) {
            ChangeDirection(!isRight);
            SetTarget();
        }
    }
    
    public void ChangeState(IState newState) {
        if (currentState != null) {
            currentState.OnExit(this);
        }
		
        currentState = newState;

        if (currentState != null) {
            currentState.OnEnter(this);
        }
    }
    

    public void ChangeDirection(bool isRight) {
        this.isRight = isRight;
        tf.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    protected virtual void OnDeath() {
        StartCoroutine(WaitForDespawn());
    }
    
    protected void ChangeAnim(string animName) {
        if (skeleton.AnimationName != animName) {
            skeleton.AnimationName = animName;
        }
    }

    private void SetTarget() {
        targetPos = (isRight) ? rightPos.position : leftPos.position;
    }

    private IEnumerator WaitForDespawn() {
        yield return CacheComponent.GetWFS(Constant.TIME_TO_DESPAWN);
        Destroy(parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isDead) return;
        if (other.CompareTag(GameTag.PlayerHit.ToString())) {
            if (GameManager.Ins.player.IsFalling) {
                Debug.Log("HI");
                GameManager.Ins.player.HitEnemy();
                OnDespawn();
            }
        } else if (other.CompareTag(GameTag.Player.ToString())) {
            Debug.Log("DIE");
        }
    }

    private void Update() {
        if (currentState != null && !isDead) {
            currentState.OnExcute(this);
        }
    }
}
