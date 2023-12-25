using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState {
	private Vector3 left, right;
	public void OnEnter(Enemy enemy) {
		left = enemy.leftPos.position;
		right = enemy.rightPos.position;
	}

	public void OnExcute(Enemy enemy) {
		enemy.Move();
	}

	public void OnExit(Enemy enemy) {
		
	}
}
