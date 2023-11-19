using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	[SerializeField] private LayerMask playerLayer;
	[SerializeField] private GameObject coiBox;
	[SerializeField] private GameObject coinBoxHitted;
	
	private bool idHitted = false;
	private bool isHit;

	public void OnHit() {
		
	}
	private bool CheckPlayerHit() {
		return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, playerLayer);
		}
    
	private void OnDrawGizmos() {
		Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.5f, Color.red);
	}

	private void Update() {
		isHit = CheckPlayerHit();
		// if ()
	}
}
