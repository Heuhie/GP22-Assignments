using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject highscoreList;

    public PauseMenu buttonBehaviour;
    public TMP_Text timeText;
    public Button highScoresButton;
    public Button mainMenuBotton;
    public Button quitButton;
    public Button backButton;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.gameOverObject = gameOverMenu.gameObject;
        gameOverMenu.SetActive(false);

        highScoresButton.onClick.AddListener(OnHighScoreClick);
        mainMenuBotton.onClick.AddListener(OnMainMenuClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnHighScoreClick()
    {
        gameOverMenu.SetActive(false);
        highscoreList.SetActive(true);
    }

    private void OnMainMenuClick()
    {
        GameManager.instance.UpdateGameState(GameState.MainMenu);
        SceneManager.LoadScene(0);
        gameOverMenu.SetActive(false);
    }

    private void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
