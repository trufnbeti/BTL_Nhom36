using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIPauseSetting : UICanvas {
    public void OnBtnCloseClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        this.PostEvent(EventID.Resume);
        CloseDirectly();
    }

    public void OnBtnHomeClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        SoundManager.Ins.MuteMusic();
        SoundManager.Ins.PlayMusic(SoundType.MainMenu);
        this.PostEvent(EventID.MainMenu);
        CloseDirectly();
    }

    public void OnBtnReplayClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        SoundManager.Ins.MuteMusic();
        SoundManager.Ins.PlayMusic(SoundType.Background);
        this.PostEvent(EventID.Replay);
        CloseDirectly();
    }

}
