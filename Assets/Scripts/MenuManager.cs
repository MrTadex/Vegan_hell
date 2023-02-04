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
        _gameOverOverlayGameOver.SetActive(obj == GameState.GameOver);
        _gameOverOverlayPauseGame.SetActive(obj == GameState.PauseGame);
    }
}
