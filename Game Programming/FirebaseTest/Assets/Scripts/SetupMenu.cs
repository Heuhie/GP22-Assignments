using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;


//[Serializable]
//public class UserInfo
//{
//    public string name;
//    public float color;
//    public int sprite;
//    public int arenaProgress;
//    public int gameState;
//    public int victories;
//}

public class SetupMenu : MonoBehaviour
{
    public TMP_InputField playerName;
    public Image spriteImage;
    public Slider colorSlider;
    public Slider spriteSlider;
    public Sprite[] sprites;
    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        colorSlider.onValueChanged.AddListener(ColorUpdate);
        spriteSlider.onValueChanged.AddListener(SpriteUpdate);

        playButton.onClick.AddListener(ButtonClick);

        //if(FirebaseAuthenticator.Instance.GetUserID == null)
        //{
        //    SceneManager.LoadScene(0);
        //}

        FirebaseSaveManager.Instance.LoadData("users/" + FirebaseAuthenticator.Instance.GetUserID, UserLoaded);
    }

    private void UserLoaded(DataSnapshot snapshot)
    {
        var loadedUser = JsonUtility.FromJson<UserInfo>(snapshot.GetRawJsonValue());
        colorSlider.value = loadedUser.color;
        spriteSlider.value = loadedUser.sprite;
        playerName.text = loadedUser.name;
    }

    private void ButtonClick()
    {
        var user = new UserInfo();
        user.color = colorSlider.value;
        user.sprite = (int)spriteSlider.value;
        user.name = playerName.text;

        string jsonString = JsonUtility.ToJson(user);
        string path = "users/" + FirebaseAuthenticator.Instance.GetUserID;
        FirebaseSaveManager.Instance.SaveData(path, jsonString);

        SceneManager.LoadScene(2);

        //FirebaseSaveManager.Instance.SaveData("user/" + FirebaseAuthenticator.Instance.GetUserID, JsonUtility.ToJson(user));
    }

    private void ColorUpdate(float hue)
    {
        spriteImage.color = Color.HSVToRGB(hue, 1f, 1f);
    }

    private void SpriteUpdate(float spriteIndex)
    {
        int index = (int)spriteIndex;
        spriteImage.sprite = sprites[index];
    }
}
