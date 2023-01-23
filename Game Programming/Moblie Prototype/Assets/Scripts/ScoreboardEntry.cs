using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryNameText = null;
    [SerializeField] private TextMeshProUGUI entryScoreText = null;

    public void InitializeScoreboard(ScoreboardEntryData scoreboardEntry)
    {
        entryNameText.text = scoreboardEntry.entryName;
        entryScoreText.text = scoreboardEntry.entryTime.ToString();
    }
}
