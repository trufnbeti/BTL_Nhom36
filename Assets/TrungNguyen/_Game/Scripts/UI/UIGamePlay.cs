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

    public void OnBtnSettingClick() {
        this.PostEvent(EventID.Pause);
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
