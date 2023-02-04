using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverOverlay;

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
        _gameOverOverlay.SetActive(obj == GameState.GameOver);
    }
}
