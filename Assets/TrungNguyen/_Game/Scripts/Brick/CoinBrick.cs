using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBrick : Brick {
	[SerializeField] private GameObject coiBox;
	[SerializeField] private GameObject coinBoxHitted;
	
	private bool isHitted = false;

	public override void OnInit() {
		coiBox.SetActive(true);
		coinBoxHitted.SetActive(false);
	}

	public override void OnHit() {
		if (isHitted)	return;
		Bounce();
	}

	protected override void Bounce() {
		isHitted = true;
		coiBox.SetActive(false);
		coinBoxHitted.SetActive(true);
		CoinInBrick coin = SimplePool.Spawn<CoinInBrick>(PoolType.Coin, transform.position, Quaternion.identity);
		coin.OnInit();
	}
}
