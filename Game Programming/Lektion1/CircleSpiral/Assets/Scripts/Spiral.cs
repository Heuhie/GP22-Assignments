using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : ProcessingLite.GP21
{
    public float spiralRadius;
    public float spiralSpaceBetween;

    private float spiralPosX;
    private float spiralPosY;

    // Start is called before the first frame update
    void Start()
    {
        Background(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        DrawSpiral();
    }

    public void DrawSpiral()
    {
        spiralPosX = spiralRadius * Mathf.Cos(spiralSpaceBetween);
        spiralPosY = spiralRadius * Mathf.Sin(spiralSpaceBetween);

        Stroke(0, 155, 155);
        Point(Width / 2 + spiralPosX, Height / 2 + spiralPosY);

        spiralSpaceBetween += 0.01f;
        spiralRadius += 0.0001f;
    }
}
