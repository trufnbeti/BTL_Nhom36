using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBridge : MonoBehaviour
{
    [SerializeField] private Transform aPoint, bPoint;
    [SerializeField] private float speed;
    private Vector3 target;
    private Transform tf;

    private void Awake() {
        tf = transform;
    }

    private void Start() {
        tf.position = aPoint.position;
        target = bPoint.position;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag(GameTag.Player.ToString())) {
            other.transform.SetParent(tf);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.CompareTag(GameTag.Player.ToString())) {
            other.transform.SetParent(null);
        }
    }

    private void Update() {
        tf.position = Vector3.MoveTowards(tf.position, target, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, aPoint.position) < 0.01f) {
            target = bPoint.position;
        } else if (Vector2.Distance(transform.position, bPoint.position) < 0.01f) {
            target = aPoint.position;
        } 
    }
}
