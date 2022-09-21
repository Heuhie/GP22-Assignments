using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : ProcessingLite.GP21
{
    [Range(0, 255)]
    public int r, g, b;

    public float speed = 5f;
    public float diameter = 1f;
    public float circlePosX;
    public float circlePosY;
    public float squarePosX;
    public float squarePosY;
    public float acceleration;
    public float maxSpeed;
    public bool isGravityActive;
    public float gravity = 9.8f;
    public float drag = 1f;

    private Vector3 velocity;
    private float xInput;
    private float yInput;
    private Vector3 inputVector;
    private Vector3 circlePos;
    private Vector3 squarePos;


    // Start is called before the first frame update
    void Start()
    {
        Background(r, g, b);

        circlePosX = Width / 2 + 1;
        circlePosY = Height / 2;
        circlePos = new Vector3(circlePosX, circlePosY);

        squarePosX = Width / 2 - 1;
        squarePosY = Height / 2;
        squarePos = new Vector3(squarePosX, squarePosY);


        Fill(0, 255, 0);
        Circle(circlePosX, circlePosY, diameter);

        Fill(255, 0, 0);
        Square(squarePos.x, squarePos.y, diameter);
    }

    // Update is called once per frame
    void Update()
    {
        Background(r, g, b);
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        inputVector = new Vector3(xInput, yInput).normalized;

        //Update movement
        Gravity();
        AccelerationMovement();
        ConstantMovement();

        if(inputVector != Vector3.zero)
        {
            AccelerationMovement();
        }
        else
        {
            Drag();
        }

        circlePos += velocity * Time.deltaTime;

        circlePos = CheckBounds(circlePos);
        squarePos = CheckBounds(squarePos);

        //Draw shapes
        Fill(0, 255, 0);
        Circle(circlePos.x, circlePos.y, diameter);

        Fill(255, 0, 0);
        Square(squarePos.x, squarePos.y, diameter);

    }

    void AccelerationMovement()
    {
        if(isGravityActive)
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        velocity += inputVector * speed * acceleration * Time.deltaTime;
    }

    void ConstantMovement()
    {
        if (isGravityActive)
        {
            squarePos.y -= gravity * Time.deltaTime;
        }
        squarePos += inputVector * speed * Time.deltaTime;
    }

    void Drag()
    {
        velocity -= velocity * drag * Time.deltaTime;
    }

    void Gravity()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGravityActive = !isGravityActive;
        }
    }


    Vector3 CheckBounds(Vector3 shapePosition)
    {
        if (shapePosition.x > Width)
            shapePosition.x = 0;

        if (shapePosition.x < 0)
            shapePosition.x = Width;

        if (shapePosition.y > Height)
            shapePosition.y = 0;

        if (shapePosition.y < 0)
            shapePosition.y = Height;

        return new Vector3(shapePosition.x, shapePosition.y);
    }
}
