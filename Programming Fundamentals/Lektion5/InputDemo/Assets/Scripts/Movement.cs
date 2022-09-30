using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : ProcessingLite.GP21
{
    [Range(0, 255)]
    public int r, g, b;

    public float speed = 5f;
    public float diameter = 1f;
    public float acceleration;
    public float maxSpeed;
    public bool isGravityActive;
    public float gravity = 9.8f;
    public float drag = 1f;

    private Vector3 velocity;
    private float xInput;
    private float yInput;
    private float circlePosX;
    private float circlePosY;
    private float squarePosX;
    private float squarePosY;
    private Vector3 inputVector;
    private Vector3 circlePos;
    private Vector3 squarePos;


    // Start is called before the first frame update
    void Start()
    {
        Background(r, g, b);

        //Set startposition of circle
        circlePosX = Width / 2 + 1;
        circlePosY = Height / 2;
        circlePos = new Vector3(circlePosX, circlePosY);

        //Set startposition of square
        squarePosX = Width / 2 - 1;
        squarePosY = Height / 2;
        squarePos = new Vector3(squarePosX, squarePosY);

        //initial color and position of circle
        Fill(0, 255, 0);
        Circle(circlePosX, circlePosY, diameter);

        //initial color and position of aquare
        Fill(255, 0, 0);
        Square(squarePos.x, squarePos.y, diameter);
    }

    // Update is called once per frame
    void Update()
    {
        Background(r, g, b);

        //get input and normalize vector
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

        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);
        circlePos += velocity * Time.deltaTime;

        //Checks if shape is within bounds
        circlePos = CheckBoundsCircle(circlePos);
        squarePos = CheckBoundsSquare(squarePos);

        //Draw shapes
        Fill(0, 255, 0);
        Circle(circlePos.x, circlePos.y, diameter);

        Fill(255, 0, 0);
        Square(squarePos.x, squarePos.y, diameter);

        Debug.Log(velocity);
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
        if (isGravityActive)
        {

            velocity.y -= gravity * Time.deltaTime;
        }

        velocity -= velocity * drag * Time.deltaTime;
    }

    void Gravity()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGravityActive = !isGravityActive;
        }
    }


    Vector3 CheckBoundsCircle(Vector3 shapePosition)
    {
        if (shapePosition.x - diameter / 2 > Width)
            shapePosition.x = 0 + diameter/2;

        if (shapePosition.x + diameter / 2 < 0)
            shapePosition.x = Width - diameter/2;

        if (shapePosition.y + diameter/2 > Height || shapePosition.y - diameter/2 < 0)
        {
            velocity.y *= -1;
            shapePosition.y = Mathf.Clamp(shapePosition.y, 0 + diameter / 2, Height - diameter / 2);
        }

        return new Vector3(shapePosition.x, shapePosition.y);
    }

    Vector3 CheckBoundsSquare(Vector3 shapePosition)
    {
        if (shapePosition.x - diameter/2 > Width)
            shapePosition.x = 0 + diameter/2;

        if (shapePosition.x + diameter/2 < 0)
            shapePosition.x = Width - diameter/2;

        if (shapePosition.y + diameter/2 > Height || shapePosition.y - diameter/2 < 0) 
            shapePosition.y = Mathf.Clamp(shapePosition.y, 0 + diameter/2, Height - diameter/2);

        return new Vector3(shapePosition.x, shapePosition.y);
    }

}
