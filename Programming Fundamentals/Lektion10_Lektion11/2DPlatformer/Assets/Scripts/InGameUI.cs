using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    private TextMeshProUGUI uiScore;


    // Update is called once per frame
    void Update()
    {
        score = player.score;
        uiScore.text = "Score: " + score;
       
    }
}
