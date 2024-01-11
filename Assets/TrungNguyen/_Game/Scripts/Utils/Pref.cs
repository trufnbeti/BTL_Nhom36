using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pref
{
	public static int Coin {
		set {
			if (value > Coin) {
				PlayerPrefs.SetInt(PrefKey.Coin.ToString(), value);
			}
		}
		get => PlayerPrefs.GetInt(PrefKey.Coin.ToString(), 0);
	}
	
	public static int Level {
		set => PlayerPrefs.SetInt(PrefKey.Level.ToString(), value);
		get => PlayerPrefs.GetInt(PrefKey.Level.ToString(), 1);
	}

	public static Dictionary<string, int> SkinsPurchased{
		set {
			string json = JsonUtility.ToJson(value);
			PlayerPrefs.SetString(PrefKey.SkinsPurchased.ToString(), json);
		}
		get {
			string json = PlayerPrefs.GetString(PrefKey.SkinsPurchased.ToString(), "{}");
			return JsonUtility.FromJson<Dictionary<string, int>>(json);
		}
	}
}