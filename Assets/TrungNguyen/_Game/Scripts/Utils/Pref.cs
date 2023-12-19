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
}