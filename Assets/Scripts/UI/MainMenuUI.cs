using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Transform loadingScreen;
    [SerializeField] private Transform optionsScreen;
    [SerializeField] private Animator animator;

    private void Awake() {
        Time.timeScale = 1f;
        playButton.onClick.AddListener(() => {
            animator.SetTrigger("Leave");
            StartCoroutine(loadScene());
        });
        optionsButton.onClick.AddListener(() => {
            optionsScreen.gameObject.SetActive(true);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    private void Start() {
        loadingScreen.gameObject.SetActive(false);
        optionsScreen.gameObject.SetActive(false);
    }

    /*Shows a loading screen and loads the game scene.*/
    public IEnumerator loadScene() {
        yield return new WaitForSeconds(1);
        loadingScreen.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
