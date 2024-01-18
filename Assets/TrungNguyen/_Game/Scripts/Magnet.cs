using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			SoundManager.Ins.PlaySound(SoundType.CollectMagnet);
			GameManager.Ins.player.OnMagnet();
			Destroy(gameObject);
		}
	}
}
