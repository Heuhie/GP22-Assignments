using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource jump;
    [SerializeField]
    private bool inMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        inMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMenu();
    }

    void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(inMenu == false)
            {
                inMenu = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                inMenu = false;
                pauseMenu.SetActive(false);
            }
        }
            
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Invoke("LoadMenuAtDeath", 5);
    }

    public void LoadMenuAtDeath()
    {
        SceneManager.LoadScene("Menu");
    }    
}
