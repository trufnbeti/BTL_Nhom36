using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : Enemy
{
    enum Anim {
        move,
        die
    }
    
    public override void Move() {
        base.Move();
        ChangeAnim(Anim.move.ToString());
    }

    protected override void OnDeath() {
        base.OnDeath();
        ChangeAnim(Anim.die.ToString());
    }
}
