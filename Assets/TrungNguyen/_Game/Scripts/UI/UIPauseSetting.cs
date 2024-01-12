using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIPauseSetting : UICanvas {
    public void OnBtnCloseClick() {
        this.PostEvent(EventID.Resume);
        CloseDirectly();
    }

    public void OnBtnHomeClick() {
        this.PostEvent(EventID.MainMenu);
        CloseDirectly();
    }

    public void OnBtnReplayClick() {
        this.PostEvent(EventID.Replay);
        CloseDirectly();
    }

}
