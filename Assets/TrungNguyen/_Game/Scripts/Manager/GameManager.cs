using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public Player player;
    public Camera cam;
    [SerializeField]private GameState _gameState;
    public bool IsState(GameState state) => _gameState == state;

    #region event
    
    Action<object> actionAddCoin;
    Action<object> actionStartGame;
    Action<object> actionMainMenu;
    Action<object> actionResume;
    Action<object> actionPause;
    Action<object> actionReplay;
    Action<object> actionLose;
    Action<object> actionNextLevel;
    Action<object> actionOpenShop;

    #endregion
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
        this.PostEvent(EventID.MainMenu);
    }
    
    private void OnEnable() {
        actionAddCoin = (param) => AddCoin((Vector3)param);
        actionStartGame = (param) => OnStartGame();
        actionMainMenu = (param) => OnMainMenu();
        actionResume = (param) => OnResume();
        actionPause = (param) => OnPause();
        actionReplay = (param) => OnReplay();
        actionLose = (param) => OnLose();
        actionNextLevel = (param) => OnNextLevel();
        actionOpenShop = (param) => OnOpenShop();
        this.RegisterListener(EventID.AddCoin, actionAddCoin);
        this.RegisterListener(EventID.StartGame, actionStartGame);
        this.RegisterListener(EventID.MainMenu, actionMainMenu);
        this.RegisterListener(EventID.Resume, actionResume);
        this.RegisterListener(EventID.Pause, actionPause);
        this.RegisterListener(EventID.Replay, actionReplay);
        this.RegisterListener(EventID.Lose, actionLose);
        this.RegisterListener(EventID.NextLevel, actionNextLevel);
        this.RegisterListener(EventID.OpenShop, actionOpenShop);
    }

    private void OnDisable() {
        this.RemoveListener(EventID.AddCoin, actionAddCoin);
        this.RemoveListener(EventID.StartGame, actionStartGame);
        this.RemoveListener(EventID.MainMenu, actionMainMenu);
        this.RemoveListener(EventID.Resume, actionResume);
        this.RemoveListener(EventID.Pause, actionPause);
        this.RemoveListener(EventID.Replay, actionReplay);
        this.RemoveListener(EventID.Lose, actionLose);
        this.RemoveListener(EventID.NextLevel, actionNextLevel);
        this.RemoveListener(EventID.OpenShop, actionOpenShop);
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
        SoundManager.Ins.PlaySound(SoundType.CollectCoin);
        ParticlePool.Play(ParticleType.CollectCoin, pos, Quaternion.identity);
    }

    public void ChangeState(GameState state) {
        _gameState = state;
    }
   
}
