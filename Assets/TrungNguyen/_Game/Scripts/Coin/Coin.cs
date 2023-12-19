using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : GameUnit {

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			this.PostEvent(EventID.AddCoin);
			ParticlePool.Play(ParticleType.CollectCoin, TF.position, Quaternion.identity);
			OnDespawn();
		}
	}
}
