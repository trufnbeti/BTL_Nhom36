using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy {
    enum Anim {
        hiding
    }
    
    protected override void OnDeath() {
        base.OnDeath();
        skeleton.AnimationName = Anim.hiding.ToString();
    }
}
