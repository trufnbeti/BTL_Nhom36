using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInBrick : GameUnit {
    [SerializeField] private Rigidbody2D rb;
    
    private Vector3 startPos;

    public override void OnInit() {
        startPos = TF.position;
        rb.velocity = new Vector2(0, 8f);
    }

    private void Update() {
        if (TF.position.y < startPos.y) {
            this.PostEvent(EventID.AddCoin);
            ParticlePool.Play(ParticleType.CollectCoin, TF.position, Quaternion.identity);
            OnDespawn();
        }
    }
}
