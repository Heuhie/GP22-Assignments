using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class CharacterSetup : MonoBehaviour
{
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        FirebaseSaveManager.Instance.LoadData("users/" + FirebaseAuthenticator.Instance.GetUserID, UserLoaded);
    }

    private void UserLoaded(DataSnapshot snapshot)
    {
        var user = JsonUtility.FromJson<UserInfo>(snapshot.GetRawJsonValue());
        spriteRenderer.color = Color.HSVToRGB(user.color, 1f, 1f);
        spriteRenderer.sprite = sprites[user.sprite];
        gameObject.name = user.name;
    }
}
