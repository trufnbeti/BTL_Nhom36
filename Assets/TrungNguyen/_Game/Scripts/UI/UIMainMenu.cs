using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public void OnBtnPlayClick() {
        this.PostEvent(EventID.StartGame);
        CloseDirectly();
    }
    
    public void OnBtnShopClick() {
        this.PostEvent(EventID.OpenShop);
        CloseDirectly();
    }
}
