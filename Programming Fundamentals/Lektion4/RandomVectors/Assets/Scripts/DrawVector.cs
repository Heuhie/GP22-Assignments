using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawVector : ProcessingLite.GP21
{
    [Range(0,256)] public int r, g, b;

    public Vector2Int startPos;
    public Vector2Int endPos;
    public Vector2 userVector;
    public float userVectorX, userVectorY;
    public bool finishedDrawing;
    public TextMeshProUGUI vectorDisplay;
    public TextMeshProUGUI vectorScore;

    private Vector2Int randomVector;
    private Vector2Int startRandomVector;
    private Vector2Int endRandomVector;
    private int totalScore;
    private bool isScoreCalculated;



    // Start is called before the first frame update
    void Start()
    {
        startRandomVector = new Vector2Int(Mathf.RoundToInt(Random.Range(1, Width)), Mathf.RoundToInt(Random.Range(1, Height)));
        endRandomVector = new Vector2Int(Mathf.RoundToInt(Random.Range(1, Width)), Mathf.RoundToInt(Random.Range(1, Height)));
        randomVector = endRandomVector - startRandomVector;
        totalScore = 0;
        GenerateRandomVector();
    }

    // Update is called once per frame
    void Update()
    {
        Background(r, g, b);
        StartPoint();
        DrawLine();
        EndPoint();
        Line(startPos.x, startPos.y, endPos.x, endPos.y);

        if(finishedDrawing)
        {
            CalculateResult();
        }

    }

    //Sets the startingpoint of the uservector to be drawn
    void StartPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            startPos.x = Mathf.RoundToInt(startPos.x);
            startPos.y = Mathf.RoundToInt(startPos.y);
            finishedDrawing = false;
        }
    }

    //Draws a line while mousebutton is pressed
    void DrawLine()
    {
        if(Input.GetMouseButton(0))
        {
            endPos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Line(startPos.x, startPos.y, endPos.x, endPos.y);
        }
    }

    //Sets the endpoint and uservector as soon as mousebutton is released
    void EndPoint()
    {
        if(Input.GetMouseButtonUp(0))
        {
            endPos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            userVector = endPos - startPos;
            finishedDrawing = true;
        }
    }

    //Prints the random vector to screen for comparison
    //also calls calculate score
    void CalculateResult()
    {
        if (userVector.magnitude != 0)
        {
            userVectorX = userVector.x;
            userVectorY = userVector.y;
            Line(startRandomVector.x, startRandomVector.y, endRandomVector.x, endRandomVector.y);
            Debug.Log("X" + userVectorX + "Y" + userVectorY);

            if (!isScoreCalculated)
            {
                CalculateScore();
                isScoreCalculated = true;
            }
        }
    }

    /// <summary>
    /// generates new random vector and prints the coordinates for 
    /// the player to see
    /// </summary>
    public void GenerateRandomVector()
    {
        isScoreCalculated = false;

        //Calculates new random vector
        startRandomVector = Vector2Int.RoundToInt(new Vector2(Random.Range(1, Width), Random.Range(1, Height)));
        endRandomVector = Vector2Int.RoundToInt(new Vector2(Random.Range(1, Width), Random.Range(1, Height)));
        randomVector = endRandomVector - startRandomVector;

        //Prints the random vectors magnitude and direction
        vectorDisplay.text = "Draw Vector: " + " " + randomVector.x + ", " + randomVector.y;
    }


    //Calculates the total score of the player
    void CalculateScore()
    {
        int scoreReductionX = Mathf.RoundToInt(Mathf.Abs(userVector.x - randomVector.x) * 10);
        int scoreReductionY = Mathf.RoundToInt(Mathf.Abs(userVector.y - randomVector.y)) * 10;
        int maxScore = 100;
        int scoreResult = maxScore - (scoreReductionX + scoreReductionY);

        totalScore += scoreResult;
        vectorScore.text = "Score= " + totalScore;
    }

}
