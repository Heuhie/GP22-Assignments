using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ProcessingLite.GP21
{
    [Range(0, 100)]
    public int r, g, b;
    [Range(0, 1)]
    public float friction;

    public float acceleration;
    public float speed;
    public float diameter;
    public Vector3 player1Pos;
    public Vector3 player2Pos;
    public bool isGravityActive;

    private float gravity = -9.8f;
    private float verticalMov;
    private float horizontalMov;
    private Vector3 inputVector;
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        Background(r, g, b);
        player1Pos.x = (Width / 2) -1;
        player1Pos.y = (Height / 2);
        Fill(0, 255, 0);
        Circle(player1Pos.x, player1Pos.y, diameter);

        player2Pos.x = (Width / 2) + 1;
        player2Pos.y = (Height / 2);
        Square(player2Pos.x, player2Pos.y, diameter);
    }

    // Update is called once per frame
    void Update()
    {
        Background(r, g, b);

        horizontalMov = Input.GetAxisRaw("Horizontal");
        verticalMov = Input.GetAxisRaw("Vertical");
        inputVector = new Vector3(horizontalMov, verticalMov).normalized;

        if (Input.GetKeyDown(KeyCode.G))
        {
            Gravity();
        }

        CalculateSpeed();
        //Deceleration();
        ConstSpeed();
        player1Pos = CheckBounds(player1Pos);
        player2Pos = CheckBounds(player2Pos);

        //Draw Player1
        Fill(0, 255, 0);
        Circle(player1Pos.x, player1Pos.y, diameter);

        //Draw Player2
        Fill(255, 0, 0);
        Square(player2Pos.x, player2Pos.y, diameter);

        
    }

    //Control speed of Circle
    void CalculateSpeed()
    {
        if (isGravityActive)
        {
            
            velocity += inputVector * acceleration * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity += inputVector * acceleration * Time.deltaTime;
        }

        player1Pos += velocity * Time.deltaTime;

  
    }

    //void Deceleration()
    //{
    //    if (inputVector.x == 0 && velocity.x > 0 )
    //    {
    //        velocity.x *= friction;
    //    }

    //    if(inputVector.y == 0 && velocity.y > 0)
    //    {
    //        velocity.y *= friction;
    //    }
    //}

    void ConstSpeed()
    {
        if (isGravityActive)
        {
            player2Pos.y += gravity * Time.deltaTime;
            player2Pos += inputVector * speed * Time.deltaTime;
        }
        else
        {
            player2Pos += inputVector * speed * Time.deltaTime;
        }
    }

    void Gravity()
    {
        isGravityActive = !isGravityActive;
    }

    Vector3 CheckBounds(Vector3 playerPos)
    {
        if(playerPos.x > Width)
            playerPos.x = 0;

        if (playerPos.x < 0)
            playerPos.x = Width;

        if (playerPos.y >= Height)
            playerPos.y = Height;

        if (playerPos.y <= 0)
            playerPos.y = 0;

        return playerPos;
    }
    //void Decelerate()
    //{
    //    player1pos.x -= velocity * time.deltatime;
    //    velocity += vector3.one * deceleration * time.deltatime;
    //    player1pos.x -= velocity * time.deltatime;
    //}
}

