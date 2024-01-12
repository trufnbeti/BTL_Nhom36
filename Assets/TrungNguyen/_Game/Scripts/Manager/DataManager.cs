using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
	public SkinData skinData;
	public PlayerData playerData;

	private void Awake() {
		LoadData();
	}

	public int Coin {
		set {
			playerData.coin = value;
			Pref.PlayerData = playerData;
		}
		get => playerData.coin;
	}

	public int Level {
		set {
			playerData.level = value;
			Pref.PlayerData = playerData;
		}
		get => playerData.level;
	}

	public int[] SkinStatus {
		set {
			playerData.skinStatus = value;
			Pref.PlayerData = playerData;
		}
		get => playerData.skinStatus;
	}

	public int IdSkin {
		set {
			playerData.idSkin = value;
			Pref.PlayerData = playerData;
		}
		get => playerData.idSkin;
	}

	private void LoadData() {
		playerData = Pref.PlayerData;
	}
}

[System.Serializable]
public class PlayerData
{
	public int coin = 0;
	public int level = 1;
	public int idSkin = 0;
	public int[] skinStatus = new int[] { 2, 0, 0, 0, 0, 0 };
}
