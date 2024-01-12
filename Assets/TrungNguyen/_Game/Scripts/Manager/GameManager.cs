using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
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
        this.RegisterListener(EventID.AddCoin, (param) => AddCoin((Vector3) param));
        this.RegisterListener(EventID.StartGame, (_) => OnStartGame());
        this.RegisterListener(EventID.MainMenu, (_) => OnMainMenu());
        this.RegisterListener(EventID.Resume, (_) => OnResume());
        this.RegisterListener(EventID.Pause, (_) => OnPause());
        this.RegisterListener(EventID.Replay, (_) => OnReplay());
        this.RegisterListener(EventID.Lose, (_) => OnLose());
        this.RegisterListener(EventID.NextLevel, (_) => OnNextLevel());
        this.RegisterListener(EventID.OpenShop, (_) => OnOpenShop());
        this.PostEvent(EventID.MainMenu);
    }

    private void OnDisable() {
        this.RemoveListener(EventID.AddCoin, (param) => AddCoin((Vector3) param));
        this.RemoveListener(EventID.StartGame, (_) => OnStartGame());
        this.RemoveListener(EventID.MainMenu, (_) => OnMainMenu());
        this.RemoveListener(EventID.Resume, (_) => OnResume());
        this.RemoveListener(EventID.Pause, (_) => OnPause());
        this.RemoveListener(EventID.Replay, (_) => OnReplay());
        this.RemoveListener(EventID.Lose, (_) => OnLose());
        this.RemoveListener(EventID.NextLevel, (_) => OnNextLevel());
        this.RemoveListener(EventID.OpenShop, (_) => OnOpenShop());
    }

    #region Event

    private void OnStartGame() {
        ChangeState(GameState.GamePlay);
        UIManager.Ins.OpenUI<UIGamePlay>();
        this.PostEvent(EventID.Replay);
    }

    private void OnMainMenu() {
        ChangeState(GameState.MainMenu);
        Time.timeScale = 1;
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIMainMenu>();
        SoundManager.Ins.Play(SoundType.MainMenu);
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

    private void OnNextLevel() {
        ChangeState(GameState.GamePlay);
    }

    private void OnOpenShop() {
        UIManager.Ins.OpenUI<UIShop>();
    }

    #endregion

    private void AddCoin(Vector3 pos) {
        DataManager.Ins.Coin += 1;
        ParticlePool.Play(ParticleType.CollectCoin, pos, Quaternion.identity);
    }

    public void ChangeState(GameState state) {
        _gameState = state;
    }
   
}
