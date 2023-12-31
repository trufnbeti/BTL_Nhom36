using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
	[SerializeField] private List<Transform> pos;
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			this.PostEvent(EventID.Win, pos);
		}
	}
}
