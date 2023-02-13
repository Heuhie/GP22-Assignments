using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentState;
    public static event Action<GameState> OnGameStateChanged;

    public bool isPauseActive;
    public GameObject gameOverObject;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                {
                    Time.timeScale = 1.0f;
                    break;
                }
            case GameState.PausMenu:
                {
                    Time.timeScale = 0.0f;
                    Debug.Log("runs updatepause");
                    break;
                }
            case GameState.RunningGame:
                Time.timeScale = 1.0f;
                break;
            case GameState.Die:
                break;
            case GameState.GameOver:
                gameOverObject.SetActive(true);
                break;  
            case GameState.Win:
                Time.timeScale = 0.0f;
                break;
        }

        Debug.Log(currentState);
        //OnGameStateChanged?.Invoke(currentState);
    }
}

public enum GameState
{
    MainMenu,
    PausMenu,
    RunningGame,
    Die,
    GameOver,
    Win
}

