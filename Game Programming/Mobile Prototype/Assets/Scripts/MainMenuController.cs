using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private Touch screenTouch;
    private Vector2 touchposition;

    private const string PLAYERDATA = "PlayerSaveData";
    private const string SCORE_NAME_KEY = "SCORE_NAME_KEY";
    private const string PLAYER_COLOR = "PlayerColor";

    public Slider slider;
    public TMP_InputField nameInput;
    public Button playButton;
    public float playerColor;

    //private PlayerSaveData playerSaveData;

    string name;
    float color;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.UpdateGameState(GameState.MainMenu);

        //if (PlayerPrefs.HasKey(SCORE_NAME_KEY))
        //{
        //    name = PlayerPrefs.GetString(SCORE_NAME_KEY);
        //    nameInput.text = name;
        //}

        name = PlayerSaveData.Instance.playerName;
        nameInput.text = name;
        nameInput.onValueChanged.AddListener(delegate { ValueChangedCheck(); });

        slider.value = PlayerSaveData.Instance.playerColor;
        slider.onValueChanged.AddListener(delegate { ColorChanged(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteKey(PLAYERDATA);
        }
    }

    public void ValueChangedCheck()
    {
        //PlayerPrefs.SetString(SCORE_NAME_KEY, nameInput.text);

        PlayerSaveData.Instance.playerName = nameInput.text;
    }

    public void ColorChanged()
    {
        //PlayerPrefs.SetFloat(PLAYER_COLOR, slider.value);

        PlayerSaveData.Instance.playerColor = slider.value;
    }

    public void StartGame()
    {
        GameManager.instance.UpdateGameState(GameState.RunningGame);
        PlayerSaveData.Instance.Save();
        SceneManager.LoadScene("MainScene");
    }
     
    public void SetName()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
        Debug.Log("Called");
    }
}
