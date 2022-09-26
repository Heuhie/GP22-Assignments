using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ProcessingLite.GP21
{
    [Range(0, 255)]
    public int r, g, b;

    public float speed = 15f;
    public float size = 1f;
    public float acceleration = 2f;
    public float maxSpeed = 15f;
    public bool isGravityActive;
    public float gravity = 9.8f;
    public float drag = 2f;

    private Vector2 velocity;
    private float xInput;
    private float yInput;
    private float circlePosX;
    private float circlePosY;
    private Vector2 inputVector;
    private Vector2 position;

    public Player()
    {
        //Set startposition of circle
        circlePosX = Width / 2 + 1;
        circlePosY = Height / 2;
        position = new Vector3(circlePosX, circlePosY);

        //initial color and position of circle
        Fill(0, 255, 0);
        Circle(circlePosX, circlePosY, size);
    }

    //Get player input and normalize vector
    public void GetInput()
    {
        //get input and normalize vector
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        inputVector = new Vector3(xInput, yInput).normalized;
    }

    //Getting player position
    public Vector2 GetPosition()
    {
        return position;
    }

    public void UpdateMovement()
    {
        if (inputVector != Vector2.zero)
        {
            AccelerationMovement();
        }
        else
        {
            Drag();
        }

        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);
        position += velocity * Time.deltaTime;

        //Checks if shape is within bounds
        position = CheckBoundsCircle(position);
    }

    //Draw shapes
    public void DrawPlayer()
    {
        Fill(0, 255, 0);
        Circle(position.x, position.y, size);
    }

    //Calculates velocity
    void AccelerationMovement()
    {
        if(isGravityActive)
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        velocity += inputVector * speed * acceleration * Time.deltaTime;
    }

    //Calculates deacceleration
    void Drag()
    {
        if (isGravityActive)
        {

            velocity.y -= gravity * Time.deltaTime;
            velocity.x -= velocity.x * drag * Time.deltaTime;
        }
        else
        velocity -= velocity * drag * Time.deltaTime;
    }

    //Bool for activating/deactivating gravity
    public void Gravity()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGravityActive = !isGravityActive;
        }
    }

    //Check that player is within screen bounds
    //and what to do when not
    Vector3 CheckBoundsCircle(Vector3 shapePosition)
    {
        if (shapePosition.x - size / 2 > Width)
        {
            shapePosition.x = Mathf.Clamp(shapePosition.x, 0 - size / 2, Width + size / 2);
            shapePosition.x = 0 - size / 2;
        }

        if (shapePosition.x + size / 2 < 0)
        {
            shapePosition.x = Mathf.Clamp(shapePosition.x, 0 - size / 2, Width + size / 2);
            shapePosition.x = Width + size / 2;
        }

        if (shapePosition.y + size/2 > Height || shapePosition.y - size/2 < 0)
        {
            velocity.y *= -0.8f;
            shapePosition.y = Mathf.Clamp(shapePosition.y, 0 + size/2, Height - size/2);
        }

        return new Vector3(shapePosition.x, shapePosition.y);
    }

    //Check for player collision with balls
    public bool PlayerCollision(Player player, Ball[] balls)
    {
        bool isCollision = false;

        foreach (Ball ball in balls)
        {
            float maxDistance = player.size / 2 + ball.size / 2;

            if (Mathf.Abs(player.position.x - ball.position.x) > maxDistance || Mathf.Abs(player.position.y - ball.position.y) > maxDistance)
            {
                isCollision = false;
            }
            else if (Vector2.Distance(player.position, ball.position) > maxDistance)
            {
                isCollision = false;
            }
            else
            {
                player.velocity *= -1;
                ball.velocity *= -1;
                isCollision = true;
                break;
            }
        }
        Debug.Log(isCollision);
        return isCollision;
    }
}
