using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

public class SignIn : MonoBehaviour
{
    private static SignIn instance;
    public static SignIn Instance{get {return instance;}}  

    public TMP_InputField email;
    public TMP_InputField password;
    public TextMeshProUGUI status;

    public Button playButton;

    FirebaseDatabase database;
    FirebaseAuth auth;

    private void Awake()
    {
        if(instance != null)
        { 
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;

            if(auth.CurrentUser == null)
            {
                Debug.Log("anonymous signin");
                
            }
            AnonymousSignIn();
        });


    }

    public void SignInButton()
    {
        SignInFirebase(email.text, password.text);
    }

    private void SignInFirebase(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                  newUser.DisplayName, newUser.UserId);
                status.text = newUser.Email + "is signed in";

                playButton.interactable = true;
            }
        });
    }

    public void RegisterButton()
    {
        RegisterNewUser(email.text, password.text);
    }

    private void RegisterNewUser(string email, string password)
    {
        Debug.Log("Starting Registration");
        status.text = "Starting Registration";
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User Registerd: {0} ({1})",
                  newUser.DisplayName, newUser.UserId);

                playButton.interactable = true;
            }
        });
    }
    
    public void AnonymousSignIn()
    {
        Debug.Log("runs this");
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully:{0} ({1}))",
                    newUser.DisplayName, newUser.UserId);

                Debug.Log("Done");
                WriteInfo("funkar detta då");
               
            }   
        });
    }

    public void WriteInfo(string data)
    {
        database = FirebaseDatabase.DefaultInstance;
        database.RootReference.Child("Testing").SetValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                Debug.Log("DataWritten");
            }
        });
    }

    public void DebugLogIn(int number)
    {
        SignInFirebase("test" + number + "@test.test", "password");
    }
}
