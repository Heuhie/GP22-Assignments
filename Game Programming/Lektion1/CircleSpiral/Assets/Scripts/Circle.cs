using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : ProcessingLite.GP21
{
    [Header("Circle Settings")]
    public int circlePointsToDraw;
    public int circleRadius;
    public float circleSpeed;

    private float circleCircumference;
    private float circleSpaceBetween;
    private float circlePosX;
    private float circlePosY;

    [Header("Spiral Settings")]
    public int spiralPointsToDraw;
    public float spiralRadius;
    public float shrinkRate;
    public float spiralSpaceBetween = 0;

    private float spiralPosX;
    private float spiralPosY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Background(0, 0, 0);
        circleCircumference = 2 * Mathf.PI * circleRadius;
        circleSpaceBetween = circleCircumference / (float)circlePointsToDraw;

        DrawCircle();
    }

    public void DrawCircle()
    {
        for (int i = 0; i < circlePointsToDraw; i++)
        {
            float tempPosX = circleRadius * Mathf.Cos(circleSpaceBetween * i + Time.realtimeSinceStartup * circleSpeed);
            float tempPosY = circleRadius * Mathf.Sin(circleSpaceBetween * i + Time.realtimeSinceStartup * circleSpeed);

            circlePosX = tempPosX;
            circlePosY = tempPosY;

            Stroke(155, 155, 0);
            Point(Width / 2 + circlePosX, Height / 2 + circlePosY);
        }
    }
}
