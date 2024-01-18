using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	[SerializeField] private float speed;
	
	private Transform tf;
	private bool isMagnet = false;

	private void Awake() {
		tf = transform;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			if (!GameManager.Ins.player.IsDead) {
				this.PostEvent(EventID.AddCoin, tf.position);
				Destroy(gameObject);
			}
		} else if (other.CompareTag(GameTag.Magnet.ToString())) {
			isMagnet = true;
		}
	}

	private void Update() {
		if (isMagnet && !GameManager.Ins.player.IsDead) {
			tf.position = Vector2.MoveTowards(tf.position, GameManager.Ins.player.tf.position, speed * Time.deltaTime);
		}
	}
}
