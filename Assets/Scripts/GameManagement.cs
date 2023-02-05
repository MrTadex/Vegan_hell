using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

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
    public TextMeshProUGUI KilledEnemies;

    [SerializeField]
    public TextMeshProUGUI currentTime;

    [SerializeField]
    public TextMeshProUGUI endTime;

    [SerializeField]
    public Image image;

    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    public int maxEnemyKills = 0;
    public float Clock = 0;
    public int Health = 6;

    void Awake() {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.PauseGame);
    }

    private void Update()
    {
        if(Time.deltaTime != 0)
            Clock += Time.deltaTime;

        string v = Clock.ToString("F0");
        currentTime.text = v;

        image.sprite = sprites[Health];

        if (Health < 1)
        {
            UpdateGameState(GameState.GameOver);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (State == GameState.PlayGame) {
                AudioListener.pause = true;
                UpdateGameState(GameState.PauseGame);
            } else {
                AudioListener.pause = false;
                UpdateGameState(GameState.PlayGame);
            }
        }
    }

    public void UpdateGameState(GameState newState) {
        State = newState;


        switch (newState) {
            case GameState.PlayGame:
                Time.timeScale = 1f;
                break;
            case GameState.PauseGame:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                // SoundManager.PlayBackgroundMusic("bad_end_music");
                GameOver();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void GameOver()
    {
        //Time.timeScale = 0f;
        Time.timeScale = 1f;
        KilledEnemies.text = maxEnemyKills.ToString() + " Kills";
        endTime.text = currentTime.text;
    }
}