using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // public GameState gs;

    public void PlayGame() {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainScene");
        // if(gs != null) {
        //     gs.ResetGame();
        // }
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
