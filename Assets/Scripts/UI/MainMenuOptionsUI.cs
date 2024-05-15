using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptionsUI : MonoBehaviour {

    [SerializeField] private Button backButton;
    [SerializeField] private Slider musicSlider;
    public event EventHandler OnVolumeChanged;

    private void Awake() {
        backButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    private void Start() {
        gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.SetActive(false);
        }
        musicSlider.value = MusicManager.Instance.GetMusicVolume();
    }

    private void OnMusicVolumeChanged(float volume) {
        OnVolumeChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetMusicVolume() {
        return musicSlider.value;
    }

}
