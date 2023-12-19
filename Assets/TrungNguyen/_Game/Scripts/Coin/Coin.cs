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
			this.PostEvent(EventID.AddCoin);
			ParticlePool.Play(ParticleType.CollectCoin, tf.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
