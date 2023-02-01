using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;


public class FirebaseSaveData : IFirebaseSubject
{
    private static FirebaseSaveData instance;
    public static FirebaseSaveData Instance { get { return instance; } }

    private FirebaseDatabase database;
    private ScoreBoardSaveData scoreBoardSaveData;


    public GameObject player;
    public float playerPosition;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
       
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            database = FirebaseDatabase.DefaultInstance;
            Debug.Log("database set");
        });
    }

    // Update is called once per frame
    public void SaveToFirebase(ScoreBoardSaveData scoreBoard)
    {
        database = FirebaseDatabase.DefaultInstance;
        string json = JsonUtility.ToJson(scoreBoard);
        database.RootReference.Child("HighScore").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);
            else
                Debug.Log("Data saved");
        });
        Debug.Log(database.RootReference.Child("HighScore").SetRawJsonValueAsync(json));
    }


    public void LoadFromFirebase()
    {
        database = FirebaseDatabase.DefaultInstance;
        database.RootReference.Child("HighScore").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log("runs error");
            }


            DataSnapshot snap = task.Result;
            scoreBoardSaveData = JsonUtility.FromJson<ScoreBoardSaveData>(snap.GetRawJsonValue());

            DatabaseDataLoaded();   //notifies observer
        }); 
    }

    public ScoreBoardSaveData GetLoadedScoreboard()
    {
        return scoreBoardSaveData;
    }
}