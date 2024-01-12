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

    private void OnEnable() {
        this.RegisterListener(EventID.AddCoin, (_) => UpdateCoin());
        UpdateCoin();
    }

    private void OnDisable() {
        this.RemoveListener(EventID.AddCoin, (_) => UpdateCoin());
    }

    public void OnBtnSettingClick() {
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
