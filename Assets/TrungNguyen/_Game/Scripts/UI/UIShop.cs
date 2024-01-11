using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class UIShop : UICanvas {
	[SerializeField] private SkinData skinData;
	[SerializeField] private SkeletonGraphic characterSkin;
	
	private List<CharacterSkin> listSkin;
	private int price, totalSkin, index;
	private string nextSkin;

	public Dictionary<string, int> test = new Dictionary<string, int>();

	protected override void OnInit() {
		listSkin = skinData.skins;
		totalSkin = listSkin.Count;
		for (int i = 0; i < totalSkin; ++i) {
			if (listSkin[i].skinName == characterSkin.initialSkinName) {
				index = listSkin[i].index;
			}
		}
		test.Add("hi", 1);
		Debug.Log(JsonUtility.ToJson(test));
	}

	public void OnBtnRightClick() {
		index = (index + 1) % totalSkin;
		nextSkin = listSkin[index].skinName;
		characterSkin.Skeleton.SetSkin(nextSkin);
	}
	
	public void OnBtnLeftClick() {
		index = (index + totalSkin - 1) % totalSkin;
		nextSkin = listSkin[index].skinName;
		characterSkin.Skeleton.SetSkin(nextSkin);
	}
}

