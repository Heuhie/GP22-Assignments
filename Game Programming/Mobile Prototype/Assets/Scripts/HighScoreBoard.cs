using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public string entryPosition;
}


public class HighScoreBoard : ScoreObserver, IFirebaseObserver
{
    private static HighScoreBoard instance;
    public static HighScoreBoard Instance { get { return instance; } }

    public int distanceBetweenLines = 30;
    public Button backButton;

    [SerializeField] private int entriesToShow = 5;
    [SerializeField] private Transform highscoresHolder;
    [SerializeField] private GameObject scoreboardEntryObject;

    private GameObject highscoreEntry;
    private ScoreBoardSaveData savedScores;

    private string savePath;

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

    private void Start()
    {
        //savePath = Application.persistentDataPath + "/highscores.json";
        GameObject.Find("GameManager").GetComponent<FirebaseSaveData>().RegisterObserver(this);
        GameObject.Find("Player").GetComponent<Player>().RegisterObserver(this);

        FirebaseSaveData.Instance.LoadFromFirebase();
        Debug.Log("runs start");
        //Debug.Log(savedScores);
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
        //FirebaseSaveData.Instance.LoadFromFirebase();

        bool scoreAdded = false;

        if(savedScores == null)
        {
            Debug.Log("No entries");
            savedScores = new ScoreBoardSaveData();
        }

        //check if time is better and adds to list if so
        for (int i = 0; i < savedScores.highscoreList.Count; i++)
        {
            if(scoreboardEntryData.entryTime < savedScores.highscoreList[i].entryTime)
            {
                savedScores.highscoreList.Insert(i, scoreboardEntryData);
                scoreAdded = true;
                break;
            }
        }

        if (scoreAdded)
        {
            //Give correct placement in list 1ST 2ND and so on..
            for (int i = 0; i < savedScores.highscoreList.Count; i++)
            {
                AddPosition(savedScores.highscoreList[i], i);
            }
        }

            //add even if time not better if there are free indexes
            if (!scoreAdded && savedScores.highscoreList.Count < entriesToShow)
        {
            AddPosition(scoreboardEntryData, savedScores.highscoreList.Count);
            savedScores.highscoreList.Add(scoreboardEntryData);
        }

        //delete entries exceeding max allowed
        if(savedScores.highscoreList.Count > entriesToShow)
        {
            savedScores.highscoreList.RemoveRange(entriesToShow, savedScores.highscoreList.Count - entriesToShow);
        }

        UpdateUI(savedScores);

        FirebaseSaveData.Instance.SaveToFirebase(savedScores);
    }

    //Observes if something triggers a new entry
    public override void 
        
        OnNotify(float time, string name)
    {
        ScoreboardEntryData score = new ScoreboardEntryData();
        score.entryName = name;
        score.entryTime = time;
        AddEntry(score);


        Debug.Log("runs notify");
    }

    private void AddPosition(ScoreboardEntryData scoreboardEntry, int position)
    {
        position = position + 1;

        switch(position)
        {
            case 1:
                scoreboardEntry.entryPosition = position.ToString() + "ST";
                break;
            case 2:
                scoreboardEntry.entryPosition = position.ToString() + "ND";
                break; 
            case 3:
                scoreboardEntry.entryPosition = position.ToString() + "RD";
                break;
            default:
                scoreboardEntry.entryPosition = position.ToString() + "TH";
                break;
                
                

        }
    }    

    //Waiting for database to finish loading
    public void OnDatabaseDataLoaded()
    {
        savedScores = FirebaseSaveData.Instance.GetLoadedScoreboard();
        UpdateUI(savedScores);
        Debug.Log("Onloaded Done");
    }
}
