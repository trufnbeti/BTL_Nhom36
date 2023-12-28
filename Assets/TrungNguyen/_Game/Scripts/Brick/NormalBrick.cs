using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NormalBrick : Brick
{
    public override void OnHit() {
        if (!GameManager.Ins.player.IsPowerUp) {
            Bounce();
        } else {
            ParticlePool.Play(ParticleType.BrickExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected override void Bounce() {
        Debug.Log("Bounce");
        transform.DOMoveY(transform.position.y + Constant.BOUNCE_VALUE, Constant.BOUNCE_DURATION).SetEase(Ease.Unset).SetLoops(2, LoopType.Yoyo);
    }
}
