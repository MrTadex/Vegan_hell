using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState {
    GameOver,
    PlayGame,
    PauseGame
}

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField]
    public GameOverScreen gameOverScreen;
    public int maxEnemyKills = 0;

    public float Clock = 0;
    public int Health = 6;

    void Awake() {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.PlayGame);
    }

    private void Update()
    {
        Clock *= Time.deltaTime;

        if(Health < 1)
        {
            UpdateGameState(GameState.GameOver);
        }
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.PlayGame:
                break;
            case GameState.PauseGame:
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void GameOver()
    {
        gameOverScreen.Setup(maxEnemyKills);
        gameOverScreen.enabled = true;
    }
}