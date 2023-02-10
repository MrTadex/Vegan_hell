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
    public TextMeshProUGUI currentlyKilledEnemies;

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

    [Range(0, 6)]
    public int Health = 6;

    bool ClockAlive = true;

    void Awake() {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.PauseGame);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if(Time.deltaTime != 0 && ClockAlive)
            Clock += Time.deltaTime;

        string v = Clock.ToString("F0");
        currentTime.text = v;

        if (Health < 0)
            Health = 0;

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

        currentlyKilledEnemies.text = maxEnemyKills.ToString();
    }

    public void UpdateGameState(GameState newState) {
        State = newState;


        switch (newState) {
            case GameState.PlayGame:
                Time.timeScale = 1f;
                ClockAlive = true;
                Camera.main.GetComponent<CameraFollow>().target.SetActive(true);
                
                break;
            case GameState.PauseGame:
                Time.timeScale = 0f;
                ClockAlive = false;
                Camera.main.GetComponent<CameraFollow>().target.SetActive(false);
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
        foreach (Transform chld in GameObject.Find("Bullets").transform) {
            Destroy(chld.gameObject);
        }

        Camera.main.GetComponent<CameraFollow>().target.SetActive(false);
        ClockAlive = false;
        Time.timeScale = 1f;
        KilledEnemies.text = maxEnemyKills.ToString() + " Kills";
        endTime.text = currentTime.text;
    }
}