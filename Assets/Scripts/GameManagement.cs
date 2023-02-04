using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

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
    // [SerializeField]
    // public GameOverScreen pauseScreen;

    [SerializeField]
    public TextMeshProUGUI pointsText;

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
        Clock += Time.deltaTime;
        pointsText.text = Clock.ToString("F0");
        if (Health < 1)
        {
            UpdateGameState(GameState.GameOver);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(State.ToString());
            if (State.ToString() == "PlayGame") {
                Time.timeScale = 0f;
                AudioListener.pause = true;
                UpdateGameState(GameState.PauseGame);
                Debug.Log("Paused");
            } else {
                Time.timeScale = 1;
                AudioListener.pause = false;
                UpdateGameState(GameState.PlayGame);
                Debug.Log("UnPaused");
            }
        }


    }

    public void UpdateGameState(GameState newState) {
        State = newState;


        switch (newState) {
            case GameState.PlayGame:
                // Health = 6;
                // Clock = 0;
                // maxEnemyKills = 0;
                break;
            case GameState.PauseGame:
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                GameOver();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void GameOver()
    {
        gameOverScreen.Setup(maxEnemyKills);
    }
}