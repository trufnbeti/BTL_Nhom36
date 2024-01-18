using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private Sound[] audioSources;
    [System.Serializable]
    private struct Sound {
        public SoundType type;
        public AudioSource audioSource;
    }

    private Dictionary<SoundType, AudioSource> sounds = new Dictionary<SoundType, AudioSource>();
    private Dictionary<SoundType, AudioSource> musics = new Dictionary<SoundType, AudioSource>();

    #region event

    Action<object> actionSound;
    Action<object> actionMusic;

    #endregion
    
    private void Start() {
        PlayMusic(SoundType.MainMenu);
    }

    private void OnEnable() {
        actionSound = (param) => OnSwitchSound();
        actionMusic = (param) => OnSwitchMusic();
        this.RegisterListener(EventID.SwitchSound, actionSound);
        this.RegisterListener(EventID.SwitchMusic, actionMusic);
    }

    private void OnDisable() {
        this.RemoveListener(EventID.SwitchSound, actionSound);
        this.RemoveListener(EventID.SwitchMusic, actionMusic);
    }

    public bool IsLoaded(SoundType type) {
        return sounds.ContainsKey(type) || musics.ContainsKey(type);
    }

    public void PlayMusic(SoundType type) {
        if (!IsLoaded(type)) {
            musics.Add(type, GetAudio(type));
            musics[type].Play();
            OnSwitchMusic();
        }
        musics[type].Play();
    }

    public void PlaySound(SoundType type) {
        if (!IsLoaded(type)) {
            sounds.Add(type, GetAudio(type));
            sounds[type].Play();
            OnSwitchSound();
        }
        sounds[type].Play();
    }

    public void MuteSound(SoundType type) {
        foreach (var item in sounds) {
            if (!item.Value) {
                item.Value.Stop();
            }
        }
    }
    
    public void MuteMusic() {
        foreach (var item in musics) {
            item.Value.Stop();
        }
    }

    public void MuteAll() {
        foreach (var item in sounds) {
            item.Value.Stop();
        }
    }
    
    

    public AudioSource GetAudio(SoundType type) {
        for (int i = 0; i < audioSources.Length; ++i) {
            if (audioSources[i].type == type) {
                return audioSources[i].audioSource;
            }
        }

        return null;
    }

    private void OnSwitchSound() {
        foreach (var item in sounds) {
            item.Value.volume = DataManager.Ins.IsSound ? 0.5f : 0;
        }
    }
    
    private void OnSwitchMusic() {
        foreach (var item in musics) {
            item.Value.volume = DataManager.Ins.IsMusic ? 0.5f : 0;
        }
    }

}
