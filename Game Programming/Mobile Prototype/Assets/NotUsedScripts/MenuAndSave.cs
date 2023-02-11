using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuAndSave : MonoBehaviour
{
    public static MenuAndSave instance;

    private const string SCORE_NAME_KEY = "SCORE_NAME_KEY";
    private const string PLAYER_COLOR = "PlayerColor";

    public Slider slider;
    public TMP_InputField nameInput;
    public Button playButton;
    public float playerColor;
    public PlayerSaveData playerSaveData;

    string name;
    float color;

    // Start is called before the first frame update
    void Start()
    {

        name = playerSaveData.name;
        slider.value = playerSaveData.playerColor;

        //if(PlayerPrefs.HasKey(SCORE_NAME_KEY))
        //{
        //    name = PlayerPrefs.GetString(SCORE_NAME_KEY);
        //    nameInput.text = name;
        //}

        //name = PlayerPrefs.GetString(SCORE_NAME_KEY);
        //nameInput.text = name;
        nameInput.onValueChanged.AddListener(delegate { ValueChangedCheck(); });

        //slider.value = PlayerPrefs.GetFloat(PLAYER_COLOR);
        slider.onValueChanged.AddListener(delegate { ColorChanged(); });
    }

    public void ValueChangedCheck()
    {
        PlayerSaveData.Instance.playerName = nameInput.text;
    }

    public void ColorChanged()
    {
        PlayerSaveData.Instance.playerColor = slider.value;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteKey(SCORE_NAME_KEY);
        }
    }

    public void LoadMainScene()
    {
        PlayerSaveData.Instance.Save();
        Debug.Log("Runs menu save");
        SceneManager.LoadScene("MainScene");
    }
}
