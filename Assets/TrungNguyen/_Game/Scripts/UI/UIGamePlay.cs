using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] private List<Image> hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Text coinTxt;
    
    Action<object> actionAddCoin;
    
    private void OnEnable() {
        actionAddCoin = (param) => UpdateCoin();
    }

    public override void Open() {
        base.Open();
        this.RegisterListener(EventID.AddCoin, actionAddCoin);
        UpdateCoin();
    }


    public override void CloseDirectly() {
        this.RemoveListener(EventID.AddCoin, actionAddCoin);
        base.CloseDirectly();
    }

    public void OnBtnSettingClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        this.PostEvent(EventID.Pause);
    }

    private void UpdateCoin() {
        coinTxt.text = DataManager.Ins.Coin.ToString();
    }

    private void Update() {
        for (int i = 0; i < hearts.Count; ++i) {
            hearts[i].sprite = emptyHeart;
        }
        for (int i = 0; i < GameManager.Ins.player.Hp; ++i) {
            hearts[i].sprite = fullHeart;
        }
    }
}
