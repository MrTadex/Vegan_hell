using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverOverlayGameOver;
    [SerializeField] private GameObject _gameOverOverlayPauseGame;

    private void Awake()
    {
        GameManagement.OnGameStateChanged += GameManagement_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManagement.OnGameStateChanged -= GameManagement_OnGameStateChanged;
    }

    private void GameManagement_OnGameStateChanged(GameState obj)
    {
        Debug.Log("HA" + obj.ToString());

        switch (obj) {
            case GameState.PlayGame:
                _gameOverOverlayPauseGame.SetActive(false);
                break;
            case GameState.PauseGame:
                _gameOverOverlayPauseGame.SetActive(true);
                break;
            case GameState.GameOver:
                _gameOverOverlayGameOver.SetActive(obj == GameState.GameOver);
                break;
        }
    }
}
