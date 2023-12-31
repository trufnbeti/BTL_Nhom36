using System.Collections.Generic;
using UnityEngine;

public class CacheComponent {
	private static Dictionary<Collider2D, Enemy> enemies = new Dictionary<Collider2D, Enemy>();

	public static Enemy GetEnemy(Collider2D col) {
		if (!enemies.ContainsKey(col)) {
			enemies.Add(col, col.GetComponent<Enemy>());
		}

		return enemies[col];
	}
	
	private static Dictionary<Collider2D, Plant> plants = new Dictionary<Collider2D, Plant>();

	public static Plant GetPlant(Collider2D col) {
		if (!plants.ContainsKey(col)) {
			plants.Add(col, col.GetComponent<Plant>());
		}

		return plants[col];
	}

	private static Dictionary<float, WaitForSeconds> WFS = new Dictionary<float, WaitForSeconds>();

	public static WaitForSeconds GetWFS(float time) {
		if (!WFS.ContainsKey(time)) {
			WFS.Add(time, new WaitForSeconds(time));
		}

		return WFS[time];
	}
}