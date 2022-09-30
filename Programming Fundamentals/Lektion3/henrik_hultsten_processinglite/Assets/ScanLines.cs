using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanLines : ProcessingLite.GP21
{
    public float spaceBetweenLines = 0.5f;
    public float yMovement = 0f;
    public float y;
    public float speed = 1;

    private void Start()
    {
        Application.targetFrameRate = 10;
        yMovement = 0;
    }

    void Update()
    {
        yMovement += speed * Time.deltaTime;
        Background(0, 166, 0);
        DrawScanLines();
    }

    void DrawScanLines()
    {
        Stroke(128, 0, 0, 64);
        StrokeWeight(2f);

        for (int i = 0; i < Height / spaceBetweenLines; i++)
        {
            y = i * spaceBetweenLines;

            Line(0, y + yMovement, Width, y + yMovement);
            if (y + yMovement > Height)
            {
                yMovement = 0f;
            }
        }
    }
}

