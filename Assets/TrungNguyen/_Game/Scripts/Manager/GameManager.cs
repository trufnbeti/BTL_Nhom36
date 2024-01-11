using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [SerializeField] private int coin;
    public Player player;
    public Camera cam;
    [SerializeField]private GameState _gameState;
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
        coin = Pref.Coin;
        ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<UIMainMenu>();
        this.RegisterListener(EventID.AddCoin, (param) => AddCoin((Vector3) param));
        this.RegisterListener(EventID.StartGame, (_) => OnStartGame());
        this.RegisterListener(EventID.MainMenu, (_) => OnMainMenu());
        this.RegisterListener(EventID.Resume, (_) => OnResume());
        this.RegisterListener(EventID.Pause, (_) => OnPause());
        this.RegisterListener(EventID.Replay, (_) => OnReplay());
        this.RegisterListener(EventID.Lose, (_) => OnLose());
    }

    private void OnDisable() {
        this.RemoveListener(EventID.AddCoin, (param) => AddCoin((Vector3) param));
        this.RemoveListener(EventID.StartGame, (_) => OnStartGame());
        this.RemoveListener(EventID.MainMenu, (_) => OnMainMenu());
        this.RemoveListener(EventID.Resume, (_) => OnResume());
        this.RemoveListener(EventID.Pause, (_) => OnPause());
        this.RemoveListener(EventID.Replay, (_) => OnReplay());
        this.RegisterListener(EventID.Lose, (_) => OnLose());
    }

    #region Event

    private void OnStartGame() {
        ChangeState(GameState.GamePlay);
        UIManager.Ins.OpenUI<UIGamePlay>();
    }

    private void OnMainMenu() {
        ChangeState(GameState.MainMenu);
        Time.timeScale = 1;
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    private void OnResume() {
        ChangeState(GameState.GamePlay);
        Time.timeScale = 1;
        UIManager.Ins.CloseUI<UIPauseSetting>();
    }

    private void OnPause() {
        ChangeState(GameState.Pause);
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<UIPauseSetting>();
    }

    private void OnReplay() {
        ChangeState(GameState.GamePlay);
        Time.timeScale = 1;
    }

    private void OnLose() {
        ChangeState(GameState.Pause);
        UIManager.Ins.OpenUI<UILose>();
    }

    #endregion

    private void AddCoin(Vector3 pos) {
        ++coin;
        Pref.Coin = coin;
        ParticlePool.Play(ParticleType.CollectCoin, pos, Quaternion.identity);
    }

    public void ChangeState(GameState state) {
        _gameState = state;
    }
   
}
