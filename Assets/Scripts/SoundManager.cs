using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pointScoredSound;

    private void Awake() {
        Instance = this;
    }

    private void PlaySound(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayScoreSound() {
        PlaySound(pointScoredSound);
    }
}
