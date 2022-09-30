using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicLines : ProcessingLite.GP21
{
    public float spaceBetweenLines = 0.2f;
    public float yMax;
    public float changeY;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 2;
        yMax = Height;

        Background(125, 166, 240);

        Stroke(128, 128, 128, 64);
        StrokeWeight(0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        Background(125, 166, 240);
        for (int i = 0; i < Height/spaceBetweenLines; i++)
        {
            counter = i % 2;
            Debug.Log(counter);

            ChangeLineColor();
            
            yMax = Height;
            //float y = spaceBetweenLines * i;
            Line(0, yMax - i, Width * i / (Height / spaceBetweenLines), 0);
        }
    }

    void ChangeLineColor()
    {
        if (counter == 0)
            Stroke(Mathf.RoundToInt(Random.value * 255f), Mathf.RoundToInt(Random.value * 255f), Mathf.RoundToInt(Random.value * 255f));
        else
            Stroke(128, 128, 128, 64);
    }
}
