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
	}

	private void OnDisable() {
		this.RemoveListener(EventID.SavePoint, (param) => SavePoint((Vector3) param));
	}

	private void OnInit() {
		GameManager.Ins.player.tf.position = startPoint;
		GameManager.Ins.player.OnInit();
	}
	private void OnReset() {
		LoadLevel(currentLevelIdx);
		OnInit();
	}

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
