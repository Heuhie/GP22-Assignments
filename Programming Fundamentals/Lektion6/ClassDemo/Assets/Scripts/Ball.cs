using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ProcessingLite.GP21
{
    //Ball color
    private int r, g, b;

    public float size;
    public Vector2 velocity;
    public Vector2 position;


    //Constructor that sets position
    public Ball(float x, float y)
    {
        position.x = x;
        position.y = y;

        r = Random.Range(0, 255);
        g = Random.Range(0, 255);
        b = Random.Range(0, 255);

        size = Random.Range(0.5f, 1.5f);

        velocity.x = Random.Range(-5, 5);
        velocity.y = Random.Range(-5, 5);
    }

    //Parameterless constructor
    public Ball()
    {
        position.x = Random.Range(5, 15);
        position.y = Random.Range(2, 8);

        r = Random.Range(0, 255);
        g = Random.Range(0, 255);
        b = Random.Range(0, 255);

        size = Random.Range(0.5f, 1.5f);

        velocity.x = Random.Range(-5, 5);
        velocity.y = Random.Range(-5, 5);
    }
    
    //Update Position
    public void UpdatePosition()
    {
        position += velocity * Time.deltaTime;
    }

    public Vector2 GetBallPosition()
    {
        return position;
    }

    //Draw shape to screen
    public void DrawBall()
    {
        Fill(r, g, b);
        Circle(position.x, position.y, size);
    }

    //Check for screencollisions
    public void CheckBounds()
    {
        if(position.x - size/2 < 0 || position.x + size/2 > Width)
        {
            position.x = Mathf.Clamp(position.x, 0 - size / 2, Width + size / 2);
            velocity.x *= -1;
        }

        if(position.y - size/2 < 0 || position.y + size/2 > Height)
        {
            position.y = Mathf.Clamp(position.y, 0 - size / 2, Height - size / 2);
            velocity.y *= -1;
        }
    }

    //Check for collisions with other shapes
    public bool CheckCollision(Ball ball1, Ball[] ballz, int index)
    {
        bool isCollision = false;
        for (int i = index + 1; i < ballz.Length; i++)
        {
            Ball ball2 = ballz[i];
            float maxDistance = ball1.size / 2 + ball2.size / 2;

            if (Mathf.Abs(ball1.position.x - ball2.position.x) > maxDistance || Mathf.Abs(ball1.position.y - ball2.position.y) > maxDistance)
            {
                isCollision = false;
            }
            else if (Vector2.Distance(ball1.position, ball2.position) > maxDistance)
            {
                isCollision = false;
            }
            else
            {

                ball1.velocity *= -1f;
                ball2.velocity *= -1f;

                isCollision = true;
                break;
            }
           
        }
        return isCollision;
    }
}
