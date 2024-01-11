using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

[System.Serializable]
public struct CharacterSkin {
    public int index;
    public SkinType skinType;
    public string skinName;
    public int price;
    public SkeletonAnimation skeletonAnimation;
}
[CreateAssetMenu(menuName = "SkinData")]
public class SkinData : ScriptableObject {
    public List<CharacterSkin> skins;
    
    private Dictionary<SkinType, string> listSkins = new Dictionary<SkinType, string>();
    private void OnEnable() {
        for (int i = 0; i < skins.Count; ++i) {
            if (!listSkins.ContainsKey(skins[i].skinType)) {
                listSkins.Add(skins[i].skinType, skins[i].skinName);
            }
        }
    }
    
    public string GetSkinName(SkinType skinType) {
        return listSkins[skinType];
    }
}
