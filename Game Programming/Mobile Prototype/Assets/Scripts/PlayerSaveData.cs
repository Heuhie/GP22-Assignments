using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string PlayerName;
    public float PlayerColor;
    public float Time;
}

public class PlayerSaveData : MonoBehaviour
{
    private const string PLAYERSAVEDATA = "PlayerSaveData";

    private static PlayerSaveData instance;
    public static PlayerSaveData Instance { get { return instance; } }

    public SaveData playerData;
    public float finishTime;
    public float playerColor;
    public string playerName;


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
        Load();
    }

    public void Save()
    {
        Debug.Log("Saved");
        //PlayerPrefs.SetFloat(TIMER, timePassed);
        playerData.PlayerName = playerName;
        playerData.PlayerColor = playerColor;
        playerData.Time = finishTime;
        string jsonstring = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAYERSAVEDATA, jsonstring);
    }

    public void Load()
    {
        Debug.Log("Loaded");

        string jsonString = PlayerPrefs.GetString(PLAYERSAVEDATA);

        playerData = JsonUtility.FromJson<SaveData>(jsonString);

        if (playerData == null)
        {
            playerData = new SaveData();
            finishTime = 0f;

        }
        playerName = playerData.PlayerName;
        finishTime = playerData.Time;
        playerColor = playerData.PlayerColor;
    }

}