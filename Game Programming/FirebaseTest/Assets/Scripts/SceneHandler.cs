using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public int sceneToLoad = 1;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
