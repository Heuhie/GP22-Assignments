using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : ScoreNotifier
{
    
    private float timePassed;
    private float previousTime;
    private MeshRenderer playerMesh;
    private float playerColor;
    private string playerName;
    private string playerPosition;
    private Rigidbody rb;
    private Vector3 startPosition;
    private bool hasWon;

    //private PlayerSaveData playerSaveData;
    //private PlayerSaveData loadedPlayerSaveData;

    public int ballsLeft = 1;
    public HighScoreBoard highscore;
    public GameObject highscoreList;


    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        playerMesh = GetComponent<MeshRenderer>();
        playerName = PlayerSaveData.Instance.playerName; 
        playerColor = PlayerSaveData.Instance.playerColor;
        previousTime = PlayerSaveData.Instance.finishTime;
        playerMesh.material.color = Color.HSVToRGB(playerColor, 0.8f, 0.8f);

        //RegisterObserver(HighScoreBoard.Instance);
        
   
        Debug.Log(PlayerSaveData.Instance.playerName);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Button pressed");
            Notify(Time.realtimeSinceStartup, playerName);
            ScoreboardEntryData score = new ScoreboardEntryData();
            score.entryName = name;
            score.entryTime = Time.realtimeSinceStartup;
            //highscore.AddEntry(score);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            TriggerWin();
        }

        //Debug.Log(hasWon);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Abyss"))
        {
            ballsLeft--;
            if(ballsLeft > 0)
            {
                StartCoroutine(Respawn());
            }
            else
            {
                GameManager.instance.UpdateGameState(GameState.GameOver); //<----funkar detta?
                //highscoreList.SetActive(true);
            }
            Debug.Log(ballsLeft);
        }

        if (other.CompareTag("HoleOfDeath"))
        {
            other.transform.root.GetComponent<MovePlane>().EnableFalling();
        }

        if (other.CompareTag("Finish") && !hasWon)
        {
            Debug.Log("Finish");

            TriggerWin();
            hasWon = true;
            ScoreboardEntryData score = new ScoreboardEntryData();
            score.entryName = name;
            score.entryTime = Time.timeSinceLevelLoad;

            //highscore.AddEntry(score);
            Notify(Time.realtimeSinceStartup, playerName);
            //highscoreList.SetActive(true);
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5);
        hasWon = false;
        transform.position = startPosition;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("HoleOfDeath"))
        {
            other.transform.root.GetComponent<MovePlane>().DisableFalling();
        }
    }

    private IEnumerator Reloadcene()
    {
        yield return new WaitForSeconds(5);
        highscoreList.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MeshColor(float playerColor)
    {
        playerMesh.material.color = Color.HSVToRGB(playerColor, 1, 1);
    }

    private void TriggerWin()
    {
        GameManager.instance.UpdateGameState(GameState.Win);
        hasWon= true;
        //highscoreList.SetActive(true);
        ScoreboardEntryData score  = new ScoreboardEntryData();
        score.entryName = name;
        Debug.Log("Gets to name");
        score.entryTime = (float)Math.Round(Time.timeSinceLevelLoad, 2);
        Notify((float)Math.Round(Time.timeSinceLevelLoad, 2), playerName);
        Debug.Log("runs Notify");
    }
}
