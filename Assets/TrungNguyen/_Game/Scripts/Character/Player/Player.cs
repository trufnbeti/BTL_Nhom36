using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SkeletonAnimation skeleton;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Collider2D collider2D;
    
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    public Transform tf;
    private Vector2 moveDirection;
    
    private float horizontal;
    private int hp;

    #region bool
    private bool isDead;
    private bool isRight = true;
    private bool canJump;
    private bool isGrounded;
    private bool isJumping;
    private bool isPowerUp;
    public bool IsPowerUp => isPowerUp;
    public bool IsDead => isDead;
    public bool IsFalling => rb.velocity.y < 0 && !isGrounded;
    #endregion

    public void OnDeath() {
        if (IsDead) return;
        isDead = true;
        GameManager.Ins.ChangeState(GameState.GameEnd);
        collider2D.isTrigger = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce);
        ChangeAnim(PlayerAnim.die.ToString());
    }

    private void Awake() {
        tf = transform;
    }
    
    public void OnInit() {
        tf = transform;
        ChangeAnim(PlayerAnim.idle.ToString());
        playerModel.transform.localScale = Constant.POWER_OFF;
        collider2D.isTrigger = false;
        this.RegisterListener(EventID.LowerFlag, (param) => LowerFlag((Vector3) param));
        this.RegisterListener(EventID.Win, (param) => WinGame((List<Transform>) param));
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
        if (!canJump || !GameManager.Ins.IsState(GameState.GamePlay)) return;
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

    private void OnDespawn() {
        gameObject.SetActive(false);
    }
    private void LowerFlag(Vector3 pos) {
        GameManager.Ins.ChangeState(GameState.GameEnd);
        rb.velocity = Vector2.zero;
        if (tf.position.y > pos.y) {
            ChangeAnim(PlayerAnim.climb_down.ToString());
            tf.DOMove(Vector3.right * tf.position.x + Vector3.up * pos.y, Constant.TIME_LOWER_FLAG).SetEase(Ease.OutQuad);
        } else {
            ChangeAnim(PlayerAnim.idle.ToString());
        }

        StartCoroutine(WaitForRace());
    }

    private void WinGame(List<Transform> pos) {
        rb.velocity = Vector2.zero;
        ChangeAnim(PlayerAnim.idle.ToString());
        StartCoroutine(WaitForConfetti(pos));
    }

    private IEnumerator WaitForPowerUp() {
        for (int i = 0; i < 5; ++i) {
            yield return CacheComponent.GetWFS(Constant.TIME_BLINK);
            playerModel.transform.localScale = Vector3.zero;
            yield return CacheComponent.GetWFS(Constant.TIME_BLINK);
            playerModel.transform.localScale = Constant.POWER_ON;
        }
    }

    private IEnumerator WaitForRace() {
        yield return CacheComponent.GetWFS(Constant.TIME_LOWER_FLAG);
        RaceToWin();
    }

    private IEnumerator WaitForConfetti(List<Transform> pos) {
        for (int i = 0; i < 3; ++i) {
            yield return CacheComponent.GetWFS(0.5f);
            ParticlePool.Play(ParticleType.Confetti, pos[i].position, Quaternion.identity);
        }
    }

    private IEnumerator WaitForDespawn() {
        yield return CacheComponent.GetWFS(Constant.TIME_DESPAWN_PLAYER);
        OnDespawn();
    }

    private void RaceToWin() {
        Vector2 move = new Vector2(speed, rb.velocity.y);
        rb.velocity = move;
        ChangeAnim(PlayerAnim.run.ToString());
    }

    private void Move() {
        moveDirection.Set(horizontal * speed, rb.velocity.y);
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

    private void CheckHitEnemy() {
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(tf.position, new Vector2(1f - 0.2f, 0.1f), 0, Vector2.down, 1f, enemyLayer);
        if (hit && IsFalling) {
            Enemy e = CacheComponent.GetEnemy(hit.collider);
            if (e.IsDead) {
                return;
            }

            if (!IsDead) {
                HitEnemy();
            }
            e.OnDespawn();
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - Vector3.up, new Vector2(1f - 0.2f, 0.1f));
    }
    
    private void Update() {
        if (!GameManager.Ins.IsState(GameState.GamePlay) || IsDead) return;
        isGrounded = CheckGrounded();
        CheckHitEnemy();
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
