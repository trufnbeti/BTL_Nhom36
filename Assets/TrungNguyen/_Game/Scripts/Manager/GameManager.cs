using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private int coin;
    public Player player;
    public Camera cam;
    private GameState _gameState;
    public bool IsState(GameState state) => _gameState == state;
    private  void Awake() {
        ChangeState(GameState.MainMenu);
        // Tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        // Target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        // Tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Xu tai tho
        int maxScreenHeight = 1920;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight) {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }
    
    private void Start() {
        ChangeState(GameState.GamePlay);
        coin = Pref.Coin;
        this.RegisterListener(EventID.AddCoin, (param) => AddCoin((Vector3) param));
    }

    private void OnDisable() {
        this.RemoveListener(EventID.AddCoin, (param) => AddCoin((Vector3) param));
    }

    public void AddCoin(Vector3 pos) {
        ++coin;
        Pref.Coin = coin;
        ParticlePool.Play(ParticleType.CollectCoin, pos, Quaternion.identity);
    }

    public void ChangeState(GameState state) {
        _gameState = state;
    }
   
}
