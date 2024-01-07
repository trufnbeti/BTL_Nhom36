using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
	[SerializeField] private float speed;
	private int direction;
	private float timer = 0;

	public void OnInit(int direction) {
		timer = 0;
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
		} else if (other.CompareTag(GameTag.Plant.ToString())) {
			Plant e = CacheComponent.GetPlant(other);
			if (!e.IsDead) {
				OnDespawn();
			}
			e.OnDespawn();
		}
	}

	private void Update() {
		timer += Time.deltaTime;
		if (timer >= Constant.TIME_DESPAWN_BULLET) {
			SimplePool.Despawn(this);
			return;
		}
		TF.position += TF.right * speed * direction * Time.deltaTime;
		
	}
}
