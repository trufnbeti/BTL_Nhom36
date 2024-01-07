using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class Flag : MonoBehaviour
{
	[SerializeField] private GameObject sprite;
	[SerializeField] private Transform target;

	private void LowerFlag() {
		sprite.transform.DOMove(target.position, 2.5f).SetEase(Ease.OutQuad);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			LowerFlag();
			this.PostEvent(EventID.LowerFlag, target.position);
		}
	}
}
