using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameManagement gameManager;

    void start() {
        gameManager = FindObjectOfType<GameManagement>();
    }

    public void PlayGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void BackToMenu () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1); 
    }

    public void QuitGame () {
        Application.Quit();
    }
}
