using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T Ins;

    public void Awake() {
        Ins = GetComponent<T>();
    }
}
