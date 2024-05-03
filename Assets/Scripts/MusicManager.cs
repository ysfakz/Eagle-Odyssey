using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set;}
    [SerializeField] private AudioSource audioSource;
    private OptionsUI optionsUI;
    private const string volume = "MUSIC_VOLUME";

    private void Awake() {
        Instance = this;

        optionsUI = FindObjectOfType<OptionsUI>();
        optionsUI.OnVolumeChanged += OptionsUI_OnVolumeChanged;
    }

    private void Start() {
        if(!PlayerPrefs.HasKey(volume)) {
            PlayerPrefs.SetFloat(volume, 1);
            Load();
        } else {
            Load();
        }
    }

    private void OptionsUI_OnVolumeChanged(object sender, EventArgs e) {
        if (optionsUI != null) {
            audioSource.volume = FindObjectOfType<OptionsUI>().GetMusicVolume();
            Save();
        }
        
    }

    private void Load() {
        audioSource.volume = PlayerPrefs.GetFloat(volume);
    }

    private void Save() {
        PlayerPrefs.SetFloat(volume, audioSource.volume);
    }

    public float GetMusicVolume() {
        return audioSource.volume;
    }

}
