using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : UICanvas
{
    public void OnBtnReplayClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        SoundManager.Ins.MuteMusic();
        SoundManager.Ins.PlayMusic(SoundType.Background);
        this.PostEvent(EventID.Replay);
        CloseDirectly();
    }

    public void OnBtnNextLevelClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        SoundManager.Ins.PlayMusic(SoundType.Background);
        this.PostEvent(EventID.NextLevel);
        CloseDirectly();
    }
}
