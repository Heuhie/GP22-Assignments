using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : ScoreNotifier
{
    
    private float timePassed;
    private float previousTime;
    private MeshRenderer playerMesh;
    private float playerColor;
    private string playerName = "Henrik";
    private Rigidbody rb;

    private PlayerSaveData playerSaveData;
    private PlayerSaveData loadedPlayerSaveData;

    public TextMeshProUGUI timerText;
    public HighScoreBoard highscore;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMesh = GetComponent<MeshRenderer>();
        //playerColor = PlayerSaveData.Instance.playerColor;
        previousTime = PlayerSaveData.Instance.finishTime;
        timerText.text = "Time: " + previousTime;
        //playerMesh.material.color = Color.HSVToRGB(playerColor, 0.8f, 0.8f);
        //playerMesh.material.color = Color.HSVToRGB(PlayerSaveData.Instance.playerColor, 0.8f, 0.8f);
        //Debug.Log(PlayerSaveData.Instance.name);
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
            highscore.AddEntry(score);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Abyss"))
        {
            Debug.Log("You failed");
            PlayerSaveData.Instance.finishTime = Time.timeSinceLevelLoad;
            PlayerSaveData.Instance.Save();
            ScoreboardEntryData score = new ScoreboardEntryData();
            score.entryName = name;
            score.entryTime = Time.realtimeSinceStartup;
            highscore.AddEntry(score);
            Notify(Time.realtimeSinceStartup, playerName);
            StartCoroutine(ReloadScene());
        }
        //if (other.CompareTag("HoleOfDeath"))
        //{
        //    other.GetComponent<BoxCollider>().enabled = false;
        //}

        if(other.CompareTag("Finish"))
        {
            Debug.Log("Finish");
        }
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MeshColor(float playerColor)
    {
        playerMesh.material.color = Color.HSVToRGB(playerColor, 1, 1);
    }
}
