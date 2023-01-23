using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentState;
    public static event Action<GameState> OnGameStateChanged;

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
            case GameState.Menu:
                break;
            case GameState.StartMap:
                break;
            case GameState.Die:
                break;
            case GameState.Lose:
                break;
        }

        OnGameStateChanged?.Invoke(currentState);
    }

}

public enum GameState
{
    Menu,
    StartMap,
    Die,
    Lose,
}

