using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;

[Serializable] //Needed to save to file
public class ScoreBoardSaveData
{
    public List<ScoreboardEntryData> highscoreList = new List<ScoreboardEntryData>();
}

[Serializable]//Needed to save to file
public class ScoreboardEntryData//need to save as object
{
    public float entryTime;
    public string entryName;
}


public class HighScoreBoard : ScoreObserver
{
    private static HighScoreBoard instance;
    public static HighScoreBoard Instance { get { return instance; } }

    public int distanceBetweenLines = 30;

    [SerializeField] private int entriesToShow = 5;
    [SerializeField] private Transform highscoresHolderTransform;
    [SerializeField] private GameObject scoreboardEntryObject;

    private GameObject highscoreEntry;

    [Header("Test")]
    [SerializeField] ScoreboardEntryData testEntryData = new ScoreboardEntryData();

    private string savePath => $"{Application.persistentDataPath}/highscores.json";

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        GameObject.Find("Player").GetComponent<Player>().RegisterObserver(this);

        ScoreBoardSaveData savedScores = GetSavedScores();


        UpdateUI(savedScores);
        
        SaveScores(savedScores);
    }

    [ContextMenu("Ass Test")]
    public void AddTestEntry()
    {
        AddEntry(testEntryData);
    }

    private ScoreBoardSaveData GetSavedScores()
    {
        if(!File.Exists(savePath))
        {
            File.Create(savePath).Dispose();
            return new ScoreBoardSaveData();
        }

        using (StreamReader stream = new StreamReader(savePath))
        {
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<ScoreBoardSaveData>(json);
        }
    }
    
    private void SaveScores(ScoreBoardSaveData scoreBoardSaveData)
    {

        var db = FirebaseDatabase.DefaultInstance;
        //var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string jsson = JsonUtility.ToJson(scoreBoardSaveData, true);

        db.RootReference.SetRawJsonValueAsync(jsson).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);
            else
                Debug.Log("DataTestWrite: Complete");
        });

        using (StreamWriter stream = new StreamWriter(savePath))
        {
            string json = JsonUtility.ToJson(scoreBoardSaveData, true);
            stream.Write(json);

           
        }
    }

    private void UpdateUI(ScoreBoardSaveData savedScores)
    {
        foreach(Transform child in highscoresHolderTransform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < savedScores.highscoreList.Count; i++)
        {
            highscoreEntry = Instantiate(scoreboardEntryObject, highscoresHolderTransform);
            highscoreEntry.GetComponent<ScoreboardEntry>().InitializeScoreboard(savedScores.highscoreList[i]);
            Vector2 anchorPosition = highscoreEntry.GetComponent<RectTransform>().anchoredPosition;
            anchorPosition = new Vector2(anchorPosition.x , anchorPosition.y -distanceBetweenLines * i);
            highscoreEntry.GetComponent<RectTransform>().anchoredPosition = anchorPosition;
        }
    }

    public void AddEntry(ScoreboardEntryData scoreboardEntryData)
    {
        ScoreBoardSaveData savedScores = GetSavedScores();

        bool scoreAdded = false;

        for(int i = 0; i < savedScores.highscoreList.Count; i++)
        {
            if(scoreboardEntryData.entryTime < savedScores.highscoreList[i].entryTime)
            {
                savedScores.highscoreList.Insert(i, scoreboardEntryData);
                scoreAdded = true;
                break;
            }
        }

        if(!scoreAdded && savedScores.highscoreList.Count < entriesToShow)
        {
            savedScores.highscoreList.Add(scoreboardEntryData);
        }

        if(savedScores.highscoreList.Count > entriesToShow)
        {
            savedScores.highscoreList.RemoveRange(entriesToShow, savedScores.highscoreList.Count - entriesToShow);
        }

        UpdateUI(savedScores);
        SaveScores(savedScores);
    }

    public override void OnNotify(float time, string name)
    {
        ScoreboardEntryData score = new ScoreboardEntryData();
        score.entryName = name;
        score.entryTime = time;
        AddEntry(score);
        Debug.Log("runs notify");
    }
}
