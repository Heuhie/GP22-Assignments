using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class PlayerData
{
    public Vector2 position;

}

public class FirebaseTest : MonoBehaviour
{
    FirebaseDatabase database;
    public GameObject player;
    public float playerPosition;
    PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = new PlayerData();
        playerData.position = player.transform.position;

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
        if(Input.GetKeyDown(KeyCode.U))
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

    private void LoadFromFirebase()
    {
        database.RootReference.Child("New Transform").GetValueAsync().ContinueWithOnMainThread(task => 
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            DataSnapshot snap = task.Result;

            playerData = JsonUtility.FromJson<PlayerData>(snap.GetRawJsonValue());
        });
    }
}
