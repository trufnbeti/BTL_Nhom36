using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SkeletonAnimation skeleton;
    
    [SerializeField] private Transform playerModel;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    
    private float horizontal;
    private int hp;

    [SerializeField] private bool canJump;
    private bool isGrounded;
    private bool isJumping;

    private void Start() {
        ChangeAnim(PlayerAnim.idle.ToString());
    }

    public void Jump() {
        if (!canJump) return;
        canJump = false;
        isJumping = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(jumpForce * Vector2.up);
        // rb.velocity = new Vector2(rb.velocity.x, 15);
        ChangeAnim(PlayerAnim.jump1.ToString());
    }

    private void Move() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (Mathf.Abs(horizontal) > 0.1f) {
            playerModel.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
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
        return Physics2D.BoxCast(transform.position, new Vector2(1f - 0.2f, 0.1f), 0, Vector2.down, 1f, groundLayer);
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
        if (!isGrounded && rb.velocity.y <= 0) {
            isJumping = false;
            ChangeAnim(PlayerAnim.falldown1.ToString());
        }
    }
}
