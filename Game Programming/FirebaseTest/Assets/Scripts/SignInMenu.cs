using UnityEngine;
using TMPro;

public class SignInMenu : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    // Start is called before the first frame update
    public void SignInButton()
    {
        FirebaseAuthenticator.Instance.SignInFirebase(email.text, password.text);
    }

    public void RegisterButton()
    {
        FirebaseAuthenticator.Instance.RegisterNewUser(email.text, password.text);
    }

    public void DebugLogIn(int number)
    {
        FirebaseAuthenticator.Instance.SignInFirebase("test" + number + "@test.test", "password");
    }
}
