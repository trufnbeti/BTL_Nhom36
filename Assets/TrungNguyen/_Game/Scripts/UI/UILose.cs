using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : UICanvas
{
    public void OnBtnReplayClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        SoundManager.Ins.MuteMusic();
        SoundManager.Ins.PlayMusic(SoundType.Background);
        this.PostEvent(EventID.Replay);
        CloseDirectly();
    }
}
