using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public void OnBtnPlayClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        this.PostEvent(EventID.StartGame);
        SoundManager.Ins.MuteMusic();
        SoundManager.Ins.PlayMusic(SoundType.Background);
        CloseDirectly();
    }
    
    public void OnBtnShopClick() {
        SoundManager.Ins.PlaySound(SoundType.ButtonClick);
        this.PostEvent(EventID.OpenShop);
        CloseDirectly();
    }
}
