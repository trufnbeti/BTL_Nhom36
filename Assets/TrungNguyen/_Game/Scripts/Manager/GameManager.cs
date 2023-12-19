using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public Player player;
    [SerializeField] private int coin;
    public int Coin {
        get => coin;
        set => coin = value;
    }

    private void Start() {
        coin = Pref.Coin;
        this.RegisterListener(EventID.AddCoin, (_) => AddCoin());
    }

    private void OnDisable() {
        this.RegisterListener(EventID.AddCoin, (_) => AddCoin());
    }

    public void AddCoin() {
        ++coin;
        Pref.Coin = coin;
    }
}
