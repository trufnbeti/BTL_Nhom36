using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	[SerializeField] private float speed;

	private Transform tf;
	private Vector3 pos;

	private void Awake() {
		tf = transform;
	}

	private void LateUpdate() {
		pos = target.position + offset;
		// pos.y = (pos.y < 0) ? 0 : pos.y;
		if (GameManager.Ins.player.IsDead) return;
		tf.position = Vector3.Lerp(tf.position, pos, speed * Time.deltaTime);
	}
	
}
