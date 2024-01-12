using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : UICanvas
{
    public void OnBtnReplayClick() {
        this.PostEvent(EventID.Replay);
        CloseDirectly();
    }

    public void OnBtnNextLevelClick() {
        this.PostEvent(EventID.NextLevel);
        CloseDirectly();
    }
}
