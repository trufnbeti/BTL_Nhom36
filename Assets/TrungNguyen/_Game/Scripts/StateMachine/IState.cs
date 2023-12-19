using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState {
    //State for enemy
    void OnEnter(Enemy enemy);
    void OnExcute(Enemy enemy);
    void OnExit(Enemy enemy);
}
