using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void Update() {
        rb.velocity = new Vector2(InputManager.Ins.Horizontal * speed, rb.velocity.y);
        
    }
}
