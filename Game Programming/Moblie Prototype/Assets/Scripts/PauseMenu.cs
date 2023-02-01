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


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        pauseButton.onClick.AddListener(OnPauseClick);
        continueButton.onClick.AddListener(OnContinueClick);
        highScoresButton.onClick.AddListener(OnHighscoreClick);
        mainMenuButton.onClick.AddListener(OnMainMenuClick);
        quitButton.onClick.AddListener(OnQuitClick);
    }

    private void OnPauseClick()
    {
        pauseMenu.SetActive(true);
    }

    private void OnContinueClick()
    {
        pauseMenu.SetActive(false);
    }

    private void OnHighscoreClick()
    {
        pauseMenu.SetActive(false);
        highscoreList.SetActive(true);
    }

    private void OnMainMenuClick()
    {
        SceneManager.LoadScene(0);
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
