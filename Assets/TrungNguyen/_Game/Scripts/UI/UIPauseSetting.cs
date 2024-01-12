using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseSetting : UICanvas {
    [SerializeField] private Toggle toggle;
    
    [SerializeField] private Image toggleOn;
    [SerializeField] private Image toggleOf;
    
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

    public void OnToggleChange(Toggle isOn) {
        
        toggleOn.gameObject.SetActive(isOn.isOn ? true : false);
        toggleOf.gameObject.SetActive(isOn.isOn ? false : true);
    }
}
