using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class ScoreboardEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryNameText = null;
    [SerializeField] private TextMeshProUGUI entryScoreText = null;
    [SerializeField] private TextMeshProUGUI entryScorePositionText = null;

    public void InitializeScoreboard(ScoreboardEntryData scoreboardEntry)
    {
        entryNameText.text = scoreboardEntry.entryName;
        entryScoreText.text = scoreboardEntry.entryTime.ToString();
        entryScorePositionText.text = scoreboardEntry.entryPosition;
    }
}
