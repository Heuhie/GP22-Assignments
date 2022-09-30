using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name : ProcessingLite.GP21
{
    private float hx, hy, ex, ey, nx, ny, rx, ry, ix, iy, kx, ky;

    //x and y for the whole text
    public float x = 0;
    public float y = -6;

    float timer = 0;
    float colorToggleTimer = 5f;

    void Update()
    {
        timer += Time.deltaTime;

        Background(0);

        //Toggle text color
        ColorToggle();

        LetterH(x, y);
        LetterE(x, y);
        LetterN(x, y);
        LetterR(x, y);
        LetterI(x, y);
        LetterK(x, y);
    }

    void ColorToggle()
    {
        if (timer >= colorToggleTimer)
        {
            int r = Random.Range(0, 255);
            int g = Random.Range(0, 255);
            int b = Random.Range(0, 255);
            Stroke(r, g, b);
            timer = 0f;
        }
    }

    public void LetterH(float x, float y)
    {
        Line(1+x, 2+y, 1+x, 6+y);
        Line(3+x , 2+y, 3+x, 6+y);
        Line(1+x, 4+y, 3+x, 4+y);
    }

    public void LetterE(float x, float y)
    {
        Line(5+x, 2+y, 5+x, 6+y);
        Line(5+x, 2+y, 7+x, 2+y);
        Line(5+x, 4+y, 7+x, 4+y);
        Line(5+x, 6+y, 7+x, 6+y);
    }

    public void LetterN(float x, float y)
    {
        Line(8+x, 2+y, 8+x, 6+y);
        Line(8+x, 6+y, 10+x, 2+y);
        Line(10+x, 2+y, 10+x, 6+y);
    }

    public void LetterR(float x, float y)
    {
        Line(12+x, 2+y, 12+x, 6+y);
        Line(12+x, 6+y, 13+x, 5+y);
        Line(13+x, 5+y, 12+x, 4+y);
        Line(12+x, 4+y, 13+x, 2+y);
    }

    public void LetterI(float x, float y)
    {
        Line(15+x, 2+y, 15+x, 6+y);
    }

    public void LetterK(float x, float y)
    {
        Line(17+x, 2+y, 17+x, 6+y);
        Line(17+x, 4+y, 18+x, 6+y);
        Line(17+x, 4+y, 18+x, 2+y);
    }





}
