using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private Transform tf;

	private void Awake() {
		tf = transform;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			if (!GameManager.Ins.player.IsDead) {
				this.PostEvent(EventID.AddCoin, tf.position);
				Destroy(gameObject);
			}
		}
	}
}
