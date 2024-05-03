using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    [SerializeField] private Button backButton;
    [SerializeField] private Slider musicSlider;
    public event EventHandler OnVolumeChanged;

    private void Awake() {
        backButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    private void OnMusicVolumeChanged(float volume) {
        OnVolumeChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Start() {
        gameObject.SetActive(false);

        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
    }

    private void Update() {
        musicSlider.value = MusicManager.Instance.GetMusicVolume();    
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }

    public float GetMusicVolume() {
        return musicSlider.value;
    }

}
