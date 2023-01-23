using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class PlayerData
{
    public Vector2 position;

}

public class FirebaseSaveData : MonoBehaviour
{
    private static FirebaseSaveData instance;

    public static FirebaseSaveData Instance { get { return instance; } }

    private FirebaseDatabase database;
    private PlayerData playerData;
    private ScoreBoardSaveData scoreBoardSaveData;


    public GameObject player;
    public float playerPosition;

    public string jsonSaveData;

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

    }

    // Start is called before the first frame update
    void Start()
    {

        //playerData = new PlayerData();
        //playerData.position = player.transform.position;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            database = FirebaseDatabase.DefaultInstance;

            string json = JsonUtility.ToJson(playerData);
            database.RootReference.Child("Hello").SetValueAsync(json);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            playerData.position = player.transform.position;
            string json = JsonUtility.ToJson(playerData);
            database.RootReference.Child("New Transform").SetValueAsync(json);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadFromFirebase();
            player.transform.position = playerData.position;
        }
    }

    public void SaveToFirebase(string jsonSaveData)
    {
        database.RootReference.Child("HighScore").SetValueAsync(jsonSaveData);
    }

    public ScoreBoardSaveData LoadFromFirebase()
    {
        database.RootReference.Child("HighScore").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
                scoreBoardSaveData = new ScoreBoardSaveData();
            }

            DataSnapshot snap = task.Result;

            scoreBoardSaveData = JsonUtility.FromJson<ScoreBoardSaveData>(snap.GetRawJsonValue());
        });
        return scoreBoardSaveData;
    }
}