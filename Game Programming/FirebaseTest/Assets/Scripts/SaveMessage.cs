using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;

[Serializable]
public class InGameMessageData
{
    public Vector3 position;
    public string messageBody;

    //public InGameMessageData() { }

    //public InGameMessageData(Vector3 pos, string msg)
    //{
    //    position = pos;
    //    messageBody = msg;
    //}
}

public class SaveMessage : MonoBehaviour
{
    public TMP_InputField messageText;
    public GameObject messagePrefab;
    public Transform player;
    public int maxNumberOfMessages = 5;
    public const string databaseLocation = "games";


    private void Start()
    {
        FirebaseSaveManager.Instance.LoadData(databaseLocation, Loaded);
        gameObject.SetActive(false);
    }

    public void Loaded(DataSnapshot snap)
    {
        List<InGameMessageData> messages = new List<InGameMessageData>();

        string key = "";

        foreach (var item in snap.Children)
        {
            messages.Add(JsonUtility.FromJson<InGameMessageData>(item.GetRawJsonValue()));

            if (key == "")
                key = item.Key;
        }

        if (snap.ChildrenCount > maxNumberOfMessages)
        {
            FirebaseSaveManager.Instance.RemoveData(databaseLocation + "/" + key);
        }

        foreach (var item in messages)
        {
            InstantiateMessage(item);
        }
    }

    private void InstantiateMessage(InGameMessageData item)
    {
        var newMessage = Instantiate(messagePrefab, item.position, Quaternion.identity);
        newMessage.GetComponent<Message>().message = item;
    }

    public void Save()
    {
        if (messageText.text == "")
            return;

        //InGameMessageData newMessage = new InGameMessageData(player.position, messageText.text); //med konstruktor

        InGameMessageData newMessage = new InGameMessageData();
        newMessage.position = player.position;
        newMessage.messageBody = messageText.text;

        InstantiateMessage(newMessage);

        string jsonString = JsonUtility.ToJson(newMessage);

        FirebaseSaveManager.Instance.PushData(databaseLocation, jsonString);

        gameObject.SetActive(false);
    }

}