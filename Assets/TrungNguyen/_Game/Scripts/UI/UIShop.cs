using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIShop : UICanvas {
	[SerializeField] private SkeletonGraphic characterSkin;
	[SerializeField] private Button btnPurchase;
	[SerializeField] private Button btnEquip;
	[SerializeField] private Button btnEquipped;
	[SerializeField] private Text priceTxt;
	
	private SkinData skinData;
	private List<CharacterSkin> listSkin;
	private int price, totalSkin, currentIdx;
	private string nextSkin;
	private CharacterSkin currentSkin;
	private int[] skinStatus = new int[6];

	private void OnEnable() {
		skinData = DataManager.Ins.skinData;
		skinStatus = DataManager.Ins.playerData.skinStatus;
		listSkin = skinData.skins;
		totalSkin = listSkin.Count;
		DisableAllBtn();
		currentIdx = DataManager.Ins.IdSkin;
		characterSkin.Skeleton.SetSkin(listSkin[currentIdx].skinName);
		currentSkin = listSkin[currentIdx];
		btnEquipped.gameObject.SetActive(true);
	}

	public void OnBtnRightClick() {
		currentIdx = (currentIdx + 1) % totalSkin;
		currentSkin = listSkin[currentIdx];
		nextSkin = listSkin[currentIdx].skinName;
		characterSkin.Skeleton.SetSkin(nextSkin);
		LoadBtn(skinStatus[currentIdx]);
	}
	
	public void OnBtnLeftClick() {
		currentIdx = (currentIdx + totalSkin - 1) % totalSkin;
		currentSkin = listSkin[currentIdx];
		nextSkin = currentSkin.skinName;
		characterSkin.Skeleton.SetSkin(nextSkin);
		LoadBtn(skinStatus[currentIdx]);
	}

	public void OnBtnPurchaseClick() {
		DataManager.Ins.Coin -= listSkin[currentIdx].price;
		DisableAllBtn();
		btnEquip.gameObject.SetActive(true);
		skinStatus[currentIdx] = 1;
		DataManager.Ins.SkinStatus = skinStatus;
	}

	public void OnBtnEquipClick() {
		DisableAllBtn();
		btnEquipped.gameObject.SetActive(true);
		skinStatus[DataManager.Ins.IdSkin] = 1;
		skinStatus[currentIdx] = 2;
		DataManager.Ins.IdSkin = currentIdx;
	}

	public void OnBtnBackClick() {
		this.PostEvent(EventID.MainMenu);
		CloseDirectly();
	}

	private void DisableAllBtn() {
		btnPurchase.gameObject.SetActive(false);
		btnEquip.gameObject.SetActive(false);
		btnEquipped.gameObject.SetActive(false);
	}
	

	private void LoadBtn(int index) {
		switch (index) {
			case 0:
				DisableAllBtn();
				btnPurchase.gameObject.SetActive(true);
				priceTxt.text = currentSkin.price.ToString();
				btnPurchase.interactable = (DataManager.Ins.Coin >= listSkin[currentIdx].price) ? true : false;
				break;
			case 1:
				DisableAllBtn();
				btnEquip.gameObject.SetActive(true);
				break;
			case 2:
				DisableAllBtn();
				btnEquipped.gameObject.SetActive(true);
				break;
		}
	}
}

