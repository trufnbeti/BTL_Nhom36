using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] private List<Level> levels;
	private Level currentLevel;
	private Vector3 startPoint;

	private void OnEnable() {
		this.RegisterListener(EventID.SavePoint, (param) => SavePoint((Vector3) param));
		this.RegisterListener(EventID.Replay, (_) => OnReset());
		this.RegisterListener(EventID.Revive, (_) => OnRevive());
		this.RegisterListener(EventID.NextLevel, (_) => OnNextLevel());
	}

	private void OnDisable() {
		this.RemoveListener(EventID.SavePoint, (param) => SavePoint((Vector3) param));
		this.RemoveListener(EventID.Replay, (_) => OnReset());
		this.RemoveListener(EventID.Revive, (_) => OnRevive());
		this.RemoveListener(EventID.NextLevel, (_) => OnNextLevel());
	}

	#region Event

	private void OnInit() {
		GameManager.Ins.player.OnInit();
		GameManager.Ins.player.tf.position = startPoint;
	}
	private void OnReset() {
		LoadLevel(DataManager.Ins.Level);
		GameManager.Ins.player.Hp = Constant.MAX_HEALTH;
		OnInit();
	}

	private void OnRevive() {
		GameManager.Ins.ChangeState(GameState.GamePlay);
		OnInit();
	}

	private void OnNextLevel() {
		DataManager.Ins.Level = DataManager.Ins.Level % levels.Count + 1;
		this.PostEvent(EventID.Replay);
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
