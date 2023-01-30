using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;


public class FirebaseSaveManager : MonoBehaviour
{
    private static FirebaseSaveManager instance;
    public static FirebaseSaveManager Instance { get { return instance; } }

    public delegate void OnLoadedDelegate(DataSnapshot snapshot);
    //public delegate void OnLoadedDelegate<T>(T data);
    //public delegate void OnLoadedMultipleDataDelegare<T>(List<T> data);
    public delegate void OnSaveDelegate();

    FirebaseDatabase database;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        database = FirebaseDatabase.DefaultInstance;
        database.SetPersistenceEnabled(false);
    }

    public void LoadData(string path, OnLoadedDelegate onLoadedDelegate)
    {
        database.RootReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log(task.Exception);
                Debug.Log(path);
            }

            onLoadedDelegate(task.Result);
        });
    }

    public void SaveData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        database.RootReference.Child(path).SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }

            onSaveDelegate?.Invoke();
        });
    }

    public void PushData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        database.RootReference.Child(path).Push().SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if(task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }

            onSaveDelegate?.Invoke();
        });
    }

    public void RemoveData(string path)
    {
        database.RootReference.Child(path).RemoveValueAsync();
    }
}
