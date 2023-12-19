using System.Collections.Generic;
using UnityEngine;

public class CacheComponent {
	private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

	public static Character GetCharacter(Collider col) {
		if (!characters.ContainsKey(col)) {
			characters.Add(col, col.GetComponent<Character>());
		}

		return characters[col];
	}

	private static Dictionary<float, WaitForSeconds> WFS = new Dictionary<float, WaitForSeconds>();

	public static WaitForSeconds GetWFS(float time) {
		if (!WFS.ContainsKey(time)) {
			WFS.Add(time, new WaitForSeconds(time));
		}

		return WFS[time];
	}
}