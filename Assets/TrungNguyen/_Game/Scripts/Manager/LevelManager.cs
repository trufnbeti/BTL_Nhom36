using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] private List<Level> levels;
	private Level currentLevel;
	private int currentLevelIdx;
	private Vector3 startPoint;

	private void Awake() {
		currentLevelIdx = Pref.Level;
	}

	private void Start() {
		OnReset();
		this.RegisterListener(EventID.SavePoint, (param) => SavePoint((Vector3) param));
		this.RegisterListener(EventID.Replay, (_) => OnReset());
		this.RegisterListener(EventID.Revive, (_) => OnRevive());
	}

	private void OnDisable() {
		this.RemoveListener(EventID.SavePoint, (param) => SavePoint((Vector3) param));
		this.RemoveListener(EventID.Replay, (_) => OnReset());
		this.RemoveListener(EventID.Revive, (_) => OnRevive());
	}

	#region Event

	private void OnInit() {
		GameManager.Ins.player.OnInit();
		GameManager.Ins.player.tf.position = startPoint;
	}
	private void OnReset() {
		LoadLevel(currentLevelIdx);
		GameManager.Ins.player.Hp = Constant.MAX_HEALTH;
		OnInit();
	}

	private void OnRevive() {
		GameManager.Ins.ChangeState(GameState.GamePlay);
		OnInit();
	}

	#endregion

	private void SavePoint(Vector3 pos) {
		startPoint = pos;
	}

	private void LoadLevel(int idx) {
		if (currentLevel != null) {
			Destroy(currentLevel.gameObject);
		}
        
		currentLevel = Instantiate(levels[idx - 1]);
		currentLevel.OnInit();
		startPoint = currentLevel.StartPoint;
	}

}
