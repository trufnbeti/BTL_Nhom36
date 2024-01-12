using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

[System.Serializable]
public struct CharacterSkin {
    public string skinName;
    public int price;
}
[CreateAssetMenu(menuName = "SkinData")]
public class SkinData : ScriptableObject {
    public List<CharacterSkin> skins;
}
