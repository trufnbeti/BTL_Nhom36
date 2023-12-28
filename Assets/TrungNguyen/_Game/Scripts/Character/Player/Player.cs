using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SkeletonAnimation skeleton;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject playerModel;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    
    private float horizontal;
    private int hp;
    private Transform tf;

    #region bool
    private bool isRight = true;
    private bool canJump;
    private bool isGrounded;
    private bool isJumping;
    private bool isPowerUp;
    public bool IsPowerUp => isPowerUp;
    public bool IsFalling => rb.velocity.y < 0 && !isGrounded;
    #endregion
    

    private void Awake() {
        tf = transform;
    }

    private void Start() {
        OnInit();
    }

    private void OnInit() {
        tf = transform;
        ChangeAnim(PlayerAnim.idle.ToString());
        playerModel.transform.localScale = Constant.POWER_OFF;
    }

    public void HitEnemy() {
        rb.velocity = Vector2.zero;
        rb.AddForce(500 * Vector2.up);
        ChangeAnim(PlayerAnim.jump1.ToString());
    }

    public void OnPowerUp() {
        if (isPowerUp) return;
        isPowerUp = true;
        StartCoroutine(WaitForPowerUp());
    }

    public void Jump() {
        if (!canJump) return;
        canJump = false;
        isJumping = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(jumpForce * Vector2.up);
        ChangeAnim(PlayerAnim.jump1.ToString());
    }

    public void Attack() {
        Bullet bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet, firePoint.position, Quaternion.identity);
        bullet.OnInit(isRight ? 1 : -1);
    }

    private IEnumerator WaitForPowerUp() {
        for (int i = 0; i < 5; ++i) {
            yield return CacheComponent.GetWFS(Constant.TIME_TO_BLINK);
            playerModel.transform.localScale = Vector3.zero;
            yield return CacheComponent.GetWFS(Constant.TIME_TO_BLINK);
            playerModel.transform.localScale = Constant.POWER_ON;
        }
    }

    private void Move() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (Mathf.Abs(horizontal) > 0.1f) {
            isRight = horizontal > 0;
            tf.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
            if (isGrounded && !isJumping) {
                ChangeAnim(PlayerAnim.run.ToString());
            }
        } else {
            if (isGrounded && !isJumping) {
                ChangeAnim(PlayerAnim.idle.ToString());
            }
        }
    }

    private void ChangeAnim(string animName) {
        if (skeleton.AnimationName != animName) {
            skeleton.AnimationName = animName;
        }
    }
    
    private bool CheckGrounded() {
        return Physics2D.BoxCast(tf.position, new Vector2(1f - 0.2f, 0.1f), 0, Vector2.down, 1f, groundLayer);
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - Vector3.up, new Vector2(1f - 0.2f, 0.1f));
    }
    
    private void Update() {
        isGrounded = CheckGrounded();
        if (isGrounded && !isJumping) {
            canJump = true;
        }
        horizontal = InputManager.Ins.Horizontal;
        Move();
        if (!isGrounded && rb.velocity.y < 0) {
            isJumping = false;
            ChangeAnim(PlayerAnim.falldown1.ToString());
        }
    }
}
