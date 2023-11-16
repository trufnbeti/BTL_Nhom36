using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private float horizontal;

    public float Horizontal => horizontal;
    private void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Jump") > 0) {
            GameManager.Ins.player.Jump();
        }
    }
}
