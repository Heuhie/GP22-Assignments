using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;


public class FirebaseSaveData : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void SaveToFirebase(ScoreBoardSaveData scoreBoard)
    {
        string json = JsonUtility.ToJson(scoreBoard);
        database.RootReference.Child("HighScore").SetRawJsonValueAsync(json);
    }


    public void LoadFromFirebase()
    {
        database.RootReference.Child("HighScore").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log("runs error");
                Debug.LogError(task.Exception);
            }

            Debug.Log("Gets snap almost");
            DataSnapshot snap = task.Result;

            scoreBoardSaveData = JsonUtility.FromJson<ScoreBoardSaveData>(snap.GetRawJsonValue());
        });
    }

    public ScoreBoardSaveData GetLoadedScoreboard()
    {
        return scoreBoardSaveData;
    }
}