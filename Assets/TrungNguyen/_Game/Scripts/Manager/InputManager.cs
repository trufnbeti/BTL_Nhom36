using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private float horizontal, vertical;

    public float Horizontal => horizontal;
    private void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
    }
}
