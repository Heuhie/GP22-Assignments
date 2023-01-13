using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Curves : ProcessingLite.GP21
{
    public float magnitude;
    public float frequency;
    public float radius;
    public float posX;
    public float posY;
    public float offsetX;
    public float spaceBetween;
    public int numberToDraw = 100;

    // Start is called before the first frame update
    void Start()
    {
        Background(155, 155, 155);
        
    }

    // Update is called once per frame
    void Update()
    {
        spaceBetween = (Width / numberToDraw);
        Background(0, 0, 0);
        SinCurve();
        CosCurve();

    }

    public void SinCurve()
    {
        for (int i = 0; i < numberToDraw; i++)
        {
            posX = spaceBetween * i;
            posY = Mathf.Sin(Time.realtimeSinceStartup - spaceBetween * i);
            Stroke(0, 255, 0);
            Point(posX, Height / 2 + posY * magnitude);
        }
    }

    public void CosCurve()
    {
        for(int i = 0; i<numberToDraw;i++)
        {
            posX = spaceBetween * i;
            posY = Mathf.Cos(Time.realtimeSinceStartup - spaceBetween * i);
            Stroke(0, 0, 255);
            Point(posX, Height / 2 + posY * magnitude);
        }
    }

    public void Circle()
    {
        for (int i = 0; i < 100; i++)
        {

        }
    }
}