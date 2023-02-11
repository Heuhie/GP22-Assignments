using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject highscoreList;

    public Button pauseButton;
    public Button continueButton;
    public Button highScoresButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Button backButton;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        highscoreList.SetActive(false);

        pauseButton.onClick.AddListener(OnPauseClick);
        continueButton.onClick.AddListener(OnContinueClick);
        highScoresButton.onClick.AddListener(OnHighscoreClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
        quitButton.onClick.AddListener(OnQuitClick);

        backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnPauseClick()
    {
        pauseMenu.SetActive(true);
        GameManager.instance.UpdateGameState(GameState.PausMenu);
    }

    private void OnContinueClick()
    {
        GameManager.instance.UpdateGameState(GameState.RunningGame);
        pauseMenu.SetActive(false);
    }

    private void OnHighscoreClick()
    {
        pauseMenu.SetActive(false);
        highscoreList.SetActive(true);
    }

    private void OnMainMenuClick()
    {
        GameManager.instance.UpdateGameState(GameState.MainMenu);
        SceneManager.LoadScene(0);
        pauseMenu.SetActive(false);
    }

    private void OnBackButtonClick()
    {
        highscoreList.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void OnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
