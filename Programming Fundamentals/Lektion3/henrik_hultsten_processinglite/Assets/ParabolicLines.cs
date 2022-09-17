using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicLines : ProcessingLite.GP21
{
    public float spaceBetweenLines = 1.0f;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 2;
        yMax = Height;

        Background(125, 166, 240);

        Stroke(128, 128, 128, 64);
        StrokeWeight(0.5f);


        int counter = 0;
        for (int i = 0; i < Height; i++)
        {
            counter = i % 3;
            Debug.Log(counter);
            if (counter == 0)
                Stroke(Mathf.RoundToInt(Random.value * 255f), Mathf.RoundToInt(Random.value * 255f), Mathf.RoundToInt(Random.value * 255f));

            yMax = Height;
            float y = spaceBetweenLines * i;
            //Stroke(Mathf.RoundToInt(Random.value * 255.0f), Mathf.RoundToInt(255.0f * Random.value), Mathf.RoundToInt(255.0f * Random.value));
            Line(0, yMax - y, Width * i / (Height / spaceBetweenLines), 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //int counter = 0;
        //for (int i = 0; i < Height / spaceBetweenLines; i++)
        //{
        //    counter = (i + 1) % 4;
        //    Debug.Log(counter);
        //    if (counter == 3)
        //        Stroke(Mathf.RoundToInt(Random.value * 255f));
            
        //    yMax = Height;
        //    float y = spaceBetweenLines * i;
        //    //Stroke(Mathf.RoundToInt(Random.value * 255.0f), Mathf.RoundToInt(255.0f * Random.value), Mathf.RoundToInt(255.0f * Random.value));
        //    Line(0, yMax - y, 0 + i, 0);
        //}
    }
}
