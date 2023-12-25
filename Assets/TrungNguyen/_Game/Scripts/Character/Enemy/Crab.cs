using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Enemy
{
    enum Anim {
        attack,
        die
    }
    
    public override void Move() {
        base.Move();
        ChangeAnim(Anim.attack.ToString());
    }

    protected override void OnDeath() {
        base.OnDeath();
        ChangeAnim(Anim.die.ToString());
    }
}
