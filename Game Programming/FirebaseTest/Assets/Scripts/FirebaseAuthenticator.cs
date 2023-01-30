using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseAuthenticator : MonoBehaviour
{
    private static FirebaseAuthenticator instance;
    public static FirebaseAuthenticator Instance { get { return instance; } }

    public TMP_InputField email;
    public TMP_InputField password;
    public TextMeshProUGUI status;

    public Button playButton;

    private FirebaseAuth auth;
    public string GetUserID { get { return auth.CurrentUser.UserId; } }



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
            return;
        }

        //if(auth == null)
        //{
        //    SceneManager.LoadScene(0);
        //}

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;

            if(auth.CurrentUser == null)
            {
                AnonymousSignIn();
            }
            else
            {
                Debug.Log(auth.CurrentUser.Email + " is logged in");
                //playButton.interactable = true;
                LoadNextScene();
            }
        });
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void AnonymousSignIn()
    {
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User logged in succesfully: {0} ({1})", newUser.DisplayName, newUser.UserId);

                playButton.interactable = true;
            }
        });
    }

    public void SignInFirebase(string email, string password)
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

                //playButton.interactable = true;
                LoadNextScene();
            }
        });
    }

    //public void RegisterButton()
    //{
    //    RegisterNewUser(email.text, password.text);
    //}

    public void RegisterNewUser(string email, string password)
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
                Debug.LogFormat("User Registered: {0} ({1})",
                  newUser.DisplayName, newUser.UserId);

                playButton.interactable = true;
            }
        });
    }

    public void StartCharacterSelectScene()
    {
        SceneManager.LoadScene(1);
    }
}
