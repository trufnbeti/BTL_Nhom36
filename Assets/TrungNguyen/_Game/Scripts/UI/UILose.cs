using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : UICanvas
{
    public void OnBtnReplayClick() {
        this.PostEvent(EventID.Replay);
        CloseDirectly();
    }
}
