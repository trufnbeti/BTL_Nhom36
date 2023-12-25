using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
	[SerializeField] private float speed;
	private int direction;

	public void OnInit(int direction) {
		this.direction = direction;
	}

	public override void OnDespawn() {
		ParticlePool.Play(ParticleType.FireExplosion, TF.position, Quaternion.identity);
		base.OnDespawn();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Enemy.ToString())) {
			Enemy e = CacheComponent.GetEnemy(other);
			if (!e.IsDead) {
				OnDespawn();
			}
			e.OnDespawn();
		} else if (other.CompareTag(GameTag.Platform.ToString())){
			OnDespawn();
		}
	}

	private void Update() {
		TF.position += TF.right * speed * direction * Time.deltaTime;
	}
}
