using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawVector : ProcessingLite.GP21
{
    public int xDrawVector;
    public int yDrawVector;
    public Vector2Int drawVector;
    public int r, g, b;
    public Vector2 startPos;
    public Vector2 endPos;
    public float circleDiameter = 1f;
    public Vector2 userVector;
    public float userVectorX, userVectorY;
    private Vector2 randomVector;
    private Vector2 startRandomVector;
    private Vector2 endRandomVector;
    public bool finishedDrawing;
    public bool newVectorGenerated; 

    // Start is called before the first frame update
    void Start()
    {
        startRandomVector = new Vector2(Random.Range(0, Width), Random.Range(0, Height));
        endRandomVector = new Vector2(Random.Range(0, Width), Random.Range(0, Height));
        randomVector = endRandomVector - startRandomVector;
        

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

        PressToContinue();

    }

    void StartPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            finishedDrawing = false;
        }
    }

    void DrawLine()
    {
        if(Input.GetMouseButton(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Line(startPos.x, startPos.y, endPos.x, endPos.y);
        }
    }

    void EndPoint()
    {
        if(Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            userVector = endPos - startPos;
            finishedDrawing = true;
        }
    }

    void CalculateResult()
    {
        userVectorX = userVector.x;
        userVectorY = userVector.y;
        Line(startRandomVector.x, startRandomVector.y, endRandomVector.x, endRandomVector.y);
        Debug.Log("X" + userVectorX + "Y" + userVectorY);

    }

    void GenerateRandomVector()
    {
        startRandomVector = new Vector2(Random.Range(0, Width), Random.Range(0, Height));
        endRandomVector = new Vector2(Random.Range(0, Width), Random.Range(0, Height));
        randomVector = endRandomVector - startRandomVector;
    }

    void PressToContinue()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GenerateRandomVector();
        }
    }
}
