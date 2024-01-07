using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	[SerializeField] private Transform startPoint;
	private Vector3 savePoint;

	public Vector3 StartPoint => startPoint.position;
	public Vector3 SavePoint => savePoint;

	public void OnInit() {
		savePoint = startPoint.position;
	}
}
