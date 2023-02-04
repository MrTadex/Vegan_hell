using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState {
    GameOver,
    PlayGame,
    PauseGame
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State;

    [SerializeField]
    public GameOverScreen GameOverScreen;
    public int maxEnemyKills = 0;

    public int Score = 0;
    public int Health = 3;

    void Awake() {
        Instance = this;
    }

    public void UpdateGameState(Gamestate newState) {
        state = newState;

        switch (newState) {
            case GameState.PlayGame:
                break;
            case GameState.PauseGame:
                break;
            case GameState.GameOver:
                break;
            case default:
                break;
        }
    }

    public void PlayerTakeDamage (float amount)
    {

    }

    public void EnemyTakeDamage (float amount)
    {

    }
}