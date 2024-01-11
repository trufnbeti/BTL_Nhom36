using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Grass : MonoBehaviour
{
	enum Anim {
		idle,
		impact
	}
	[SerializeField] private SkeletonAnimation skeleton;
	
	private void ChangeAnim(string animName) {
		if (skeleton.AnimationName != animName) {
			skeleton.state.SetAnimation(0, animName, true);
		}
	}

	private IEnumerator WaitForReset() {
		yield return CacheComponent.GetWFS(Constant.TIME_RESET_GRASS);
		ChangeAnim(Anim.idle.ToString());
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(GameTag.Player.ToString())) {
			ChangeAnim(Anim.impact.ToString());
			StartCoroutine(WaitForReset());
		}
	}
}
