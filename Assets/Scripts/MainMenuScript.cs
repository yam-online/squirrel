using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Canvas rulesCanvas;

    public void PlayGame() {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void ShowRules() {
        if(rulesCanvas != null) {
            rulesCanvas.gameObject.SetActive(true);
        }
    }

    public void Back() {
        if(rulesCanvas != null) {
            rulesCanvas.gameObject.SetActive(false);
        }
    }

}
