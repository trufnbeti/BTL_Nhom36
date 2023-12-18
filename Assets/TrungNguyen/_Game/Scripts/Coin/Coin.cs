using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : GameUnit {
    [SerializeField] private Rigidbody2D rb;
    
    public Vector3 startPos;

    private void OnEnable() {
        rb.velocity = new Vector2(0, 8f);
    }

    public void OnInit() {
        startPos = TF.position;
    }

    private void Update() {
        if (TF.position.y < startPos.y) {
            OnDespawn();
        }
    }
}
