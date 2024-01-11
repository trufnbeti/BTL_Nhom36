using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState {
	public void OnEnter(Enemy enemy) {
	}

	public void OnExcute(Enemy enemy) {
		enemy.Move();
	}

	public void OnExit(Enemy enemy) {
		
	}
}
