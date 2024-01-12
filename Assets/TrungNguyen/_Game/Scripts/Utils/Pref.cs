using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pref
{
	public static PlayerData PlayerData{
		set {
			string json = JsonUtility.ToJson(value);
			PlayerPrefs.SetString(PrefKey.PlayerData.ToString(), json);
		}
		get {
			string json = PlayerPrefs.GetString(PrefKey.PlayerData.ToString(), "");
			return JsonUtility.FromJson<PlayerData>(json);
		}
	}
	
}
