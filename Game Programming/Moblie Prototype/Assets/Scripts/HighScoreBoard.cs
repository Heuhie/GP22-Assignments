using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


[Serializable] //Needed to save to file
public class ScoreBoardSaveData
{
    public List<ScoreboardEntryData> highscoreList = new List<ScoreboardEntryData>();
}

[Serializable]//Needed to save to file
public class ScoreboardEntryData//need to save as for list
{
    public float entryTime;
    public string entryName;
}


public class HighScoreBoard : ScoreObserver, IFirebaseObserver
{
    private static HighScoreBoard instance;
    public static HighScoreBoard Instance { get { return instance; } }

    public int distanceBetweenLines = 30;

    [SerializeField] private int entriesToShow = 5;
    [SerializeField] private Transform highscoresHolder;
    [SerializeField] private GameObject scoreboardEntryObject;

    private GameObject highscoreEntry;
    private ScoreBoardSaveData savedScores;

    public delegate void OnScoreboardLoaded();
    public static event OnScoreboardLoaded onScoreboardLoaded;

    delegate void OnScoreBoardFetched();
    OnScoreBoardFetched onScoreBoardFetched;

    [Header("Test")]
    [SerializeField] ScoreboardEntryData testEntryData = new ScoreboardEntryData();

    private string savePath;
   
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

    private void OnEnable()
    {
        onScoreboardLoaded += UpdateLoadedScoreboard;
    }

    private void OnDisable()
    {
        onScoreboardLoaded -= UpdateLoadedScoreboard;
    }

    private void Start()
    {
        //savePath = Application.persistentDataPath + "/highscores.json";
        GameObject.Find("GameManager").GetComponent<FirebaseSaveData>().RegisterObserver(this);
        GameObject.Find("Player").GetComponent<Player>().RegisterObserver(this);
        
        Debug.Log(savedScores);

        //CallLoadScoreBoard();
        //ScoreBoardSaveData savedScores = GetSavedScores();
        //StartCoroutine(StartupDelay());
        //UpdateUI(savedScores);

        FirebaseSaveData.Instance.LoadFromFirebase();

        //FirebaseSaveData.Instance.SaveToFirebase(savedScores);
        //SaveScores(savedScores);
    }


    private void UpdateLoadedScoreboard()
    {
        //savedScores = FirebaseSaveData.Instance.GetLoadedScoreboard();
        UpdateUI(savedScores);
        Debug.Log("Called Updated");
    }

    [ContextMenu("Add Test")]
    public void AddTestEntry()
    {
        AddEntry(testEntryData);
    }

    //update visual scoreboard
    public void UpdateUI(ScoreBoardSaveData savedScores)
    {
        foreach(Transform child in highscoresHolder)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < savedScores.highscoreList.Count; i++)
        {
            highscoreEntry = Instantiate(scoreboardEntryObject, highscoresHolder);
            highscoreEntry.GetComponent<ScoreboardEntry>().InitializeScoreboard(savedScores.highscoreList[i]);
            Vector2 anchorPosition = highscoreEntry.GetComponent<RectTransform>().anchoredPosition;
            anchorPosition = new Vector2(anchorPosition.x , anchorPosition.y -distanceBetweenLines * i);
            highscoreEntry.GetComponent<RectTransform>().anchoredPosition = anchorPosition;
        }
    }



    public void AddEntry(ScoreboardEntryData scoreboardEntryData)
    {
        FirebaseSaveData.Instance.LoadFromFirebase();
        //FirebaseSaveData.Instance.GetLoadedScoreboard();
        //ScoreBoardSaveData savedScores = FirebaseSaveData.Instance.GetLoadedScoreboard();
        //ScoreBoardSaveData savedScores = GetSavedScores();

        bool scoreAdded = false;


        //check if time is better and adds to list if so
        for(int i = 0; i < savedScores.highscoreList.Count; i++)
        {
            if(scoreboardEntryData.entryTime < savedScores.highscoreList[i].entryTime)
            {
                savedScores.highscoreList.Insert(i, scoreboardEntryData);
                scoreAdded = true;
                break;
            }
        }

        //add even if time not better if there are free indexes
        if(!scoreAdded && savedScores.highscoreList.Count < entriesToShow)
        {
            savedScores.highscoreList.Add(scoreboardEntryData);
        }

        //delete entries exceeding max allowed
        if(savedScores.highscoreList.Count > entriesToShow)
        {
            savedScores.highscoreList.RemoveRange(entriesToShow, savedScores.highscoreList.Count - entriesToShow);
        }

        UpdateUI(savedScores);

        FirebaseSaveData.Instance.SaveToFirebase(savedScores);
        //SaveScores(savedScores);
    }

    //Observes if something triggers a new entry
    public override void OnNotify(float time, string name)
    {
        ScoreboardEntryData score = new ScoreboardEntryData();
        score.entryName = name;
        score.entryTime = time;
        AddEntry(score);
        Debug.Log("runs notify");
    }

    //Waiting for database to finish loading
    public void OnDatabaseDataLoaded()
    {
        savedScores = FirebaseSaveData.Instance.GetLoadedScoreboard();
        UpdateUI(savedScores);
        Debug.Log("Onloaded Triggered");
    }


    /// <summary>
    /// LEGACY for saving to desktop
    /// and coroutines for loading from firebase
    /// </summary>
    /// <returns></returns>

    //Legacy Methods
    private IEnumerator StartupDelay()
    {
        yield return new WaitForSeconds(1);
        savedScores = FirebaseSaveData.Instance.GetLoadedScoreboard();
        UpdateUI(savedScores);
    }

    private void CallLoadScoreBoard()
    {
        FirebaseSaveData.Instance.LoadFromFirebase();
        onScoreboardLoaded();
        Debug.Log("CAlled");
    }


    private ScoreBoardSaveData GetSavedScores()
    {
        if (!File.Exists(savePath))
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
        using (StreamWriter stream = new StreamWriter(savePath))
        {
            string json = JsonUtility.ToJson(scoreBoardSaveData, true);
            stream.Write(json);     
        }
    }
}
