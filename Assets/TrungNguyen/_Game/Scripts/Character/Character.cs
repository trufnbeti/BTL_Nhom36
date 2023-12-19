using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] protected LayerMask groundLayer;
	[SerializeField] protected Rigidbody2D rb;
	[SerializeField] protected SkeletonAnimation skeleton;
	[SerializeField] protected Transform playerModel;
	[SerializeField] protected float speed;

	protected Transform tf;

	private void Awake() {
		tf = transform;
	}

	public virtual void Move() { }
}
