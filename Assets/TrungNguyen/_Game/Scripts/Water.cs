using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Water : MonoBehaviour
{

	[SerializeField] private DOTweenAnimation doTween;

	private void OnDisable() {
		doTween.DOKill();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			SoundManager.Ins.PlaySound(SoundType.Water);
			this.PostEvent(EventID.PlayerDie);
		}
	}
}
