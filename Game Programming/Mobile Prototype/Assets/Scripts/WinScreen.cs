using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public TextMeshProUGUI finishTime;

    public GameObject highScoreBoard;
    public GameObject winPanel;
    public GameObject backButton;
    public GameObject pauseButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Button retryButton;

    private bool hasWon;

    private void Start()
    {
        mainMenuButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);

        mainMenuButton.onClick.AddListener(onLoadMainMenuClick);
        quitButton.onClick.AddListener(onQuitGameClick);
        retryButton.onClick.AddListener(onRetryLevelClick);
    }


    // Update is called once per frame
    void Update()
    {
        timer.text = "Timer: " + Math.Round(Time.timeSinceLevelLoad, 2);
        //Debug.Log(GameManager.instance.currentState);
        if (GameManager.instance.currentState == GameState.Win && !hasWon)
        {
            timer.transform.parent.gameObject.SetActive(false);
            winPanel.gameObject.SetActive(true);
            backButton.SetActive(false);
            pauseButton.SetActive(false);
            highScoreBoard.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);

            finishTime.text = timer.text;
            hasWon = true;
        }
    }

    private void onRetryLevelClick()
    {
        timer.transform.parent.gameObject.SetActive(true);
        backButton.SetActive(true);
        pauseButton.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        highScoreBoard.SetActive(false);
        winPanel.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        hasWon = false;

        GameManager.instance.UpdateGameState(GameState.RunningGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void onLoadMainMenuClick()
    {
        timer.transform.parent.gameObject.SetActive(false);
        backButton.SetActive(false);
        pauseButton.SetActive(false);
        highScoreBoard.SetActive(false);
        winPanel.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        hasWon = false;

        GameManager.instance.UpdateGameState(GameState.MainMenu);
        SceneManager.LoadScene(0);
    }

    private void onQuitGameClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
