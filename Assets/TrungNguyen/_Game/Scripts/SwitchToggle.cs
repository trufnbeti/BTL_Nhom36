using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
	[SerializeField] private RectTransform rectTransform;
	[SerializeField] private Image checkMark;
	[SerializeField] private Image bg;
	[SerializeField] private Sprite imgBgOn;
	[SerializeField] private Sprite imgBgOff;
	[SerializeField] private Sprite imgCheckOn;
	[SerializeField] private Sprite imgCheckOff;
	[SerializeField] private Toggle toggle;
	[SerializeField] private SoundCategory soundCategory;

	private Vector2 handlePos;

	private void OnEnable() {
		handlePos = rectTransform.anchoredPosition;
		Debug.Log(handlePos);
		toggle.onValueChanged.AddListener(OnSwitch);
		switch (soundCategory) {
			case SoundCategory.Music:
				toggle.isOn = DataManager.Ins.IsMusic;
				OnSwitch(DataManager.Ins.IsMusic);
				break;
			case SoundCategory.SoundFX:
				toggle.isOn = DataManager.Ins.IsSound;
				OnSwitch(DataManager.Ins.IsSound);
				break;
		}
	}

	private void OnDisable() {
		toggle.onValueChanged.RemoveListener(OnSwitch);
	}

	private void OnSwitch(bool isOn) {
		bg.sprite = isOn ? imgBgOn : imgBgOff;
		checkMark.sprite = isOn ? imgCheckOn : imgCheckOff;
		handlePos.x = isOn ? Mathf.Abs(handlePos.x) : -Mathf.Abs(handlePos.x);
		rectTransform.anchoredPosition = handlePos;
		switch (soundCategory) {
			case SoundCategory.Music:
				DataManager.Ins.IsMusic = isOn;
				this.PostEvent(EventID.SwitchMusic);
				break;
			case SoundCategory.SoundFX:
				DataManager.Ins.IsSound = isOn;
				this.PostEvent(EventID.SwitchSound);
				break;
		}
	}
}
