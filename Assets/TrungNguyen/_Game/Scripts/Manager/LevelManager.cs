using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	[SerializeField] private List<Level> levels;
	private Level currentLevel;
	private Vector3 startPoint;

	#region event

	Action<object> actionSavePoint;
	Action<object> actionReplay;
	Action<object> actionRevive;
	Action<object> actionNextLevel;

	#endregion

	private void OnEnable() {
		actionSavePoint = (param) => SavePoint((Vector3)param);
		actionReplay = (param) => OnReset();
		actionRevive = (param) => OnRevive();
		actionNextLevel = (param) => OnNextLevel();
		this.RegisterListener(EventID.SavePoint, actionSavePoint);
		this.RegisterListener(EventID.Replay, actionReplay);
		this.RegisterListener(EventID.Revive, actionRevive);
		this.RegisterListener(EventID.NextLevel, actionNextLevel);
	}

	private void OnDisable() {
		this.RemoveListener(EventID.SavePoint, actionSavePoint);
		this.RemoveListener(EventID.Replay, actionReplay);
		this.RemoveListener(EventID.Revive, actionRevive);
		this.RemoveListener(EventID.NextLevel, actionNextLevel);
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
