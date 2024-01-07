using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    enum Anim {
        hiding,
        move
    }

    public override void Move() {
        base.Move();
        ChangeAnim(Anim.move.ToString());
    }

    protected override void OnDeath() {
        base.OnDeath();
        ChangeAnim(Anim.hiding.ToString());
    }
}
