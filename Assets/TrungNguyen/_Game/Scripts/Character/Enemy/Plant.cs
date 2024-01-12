using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class Plant : MonoBehaviour
{
    enum Anim {
        attack,
        die
    }
    [SerializeField] private DOTweenAnimation doTween;
    [SerializeField] private SkeletonAnimation skeleton;

    private bool isDead = false;

    public bool IsDead => isDead;

    public void OnDespawn() {
        if (IsDead) return;
        isDead = true;
        doTween.DOPause();
        skeleton.loop = false;
        ChangeAnim(Anim.die.ToString());
        StartCoroutine(WaitForDespawn());
    }

    private IEnumerator WaitForDespawn() {
        yield return CacheComponent.GetWFS(Constant.TIME_DESPAWN_ENEMY);
        Destroy(gameObject);
    }
    
    private void ChangeAnim(string animName) {
        if (skeleton.AnimationName != animName) {
            skeleton.state.SetAnimation(0, animName, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (IsDead) return;
        if (other.CompareTag(GameTag.Player.ToString())) {
            this.PostEvent(EventID.PlayerDie);
        }
    }
}
