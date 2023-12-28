using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private void Start() {
        OnInit();
    }

    public virtual void OnInit() { }
    public virtual void OnHit() { }
    
    protected virtual void Bounce(){ }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(GameTag.PlayerCollider.ToString())) {
            OnHit();
        }
    }
    
}
