using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : ProcessingLite.GP21
{
    [SerializeField]
    private Vector3 circlePos;
    [SerializeField]
    private float circleDiameter = 1f;
    [SerializeField]
    float color;
    [SerializeField]
    Vector3 lineEndPos;
    [SerializeField]
    Vector3 circleMovement;
    [SerializeField]
    float speedLimit = 4f;
    Vector3 velocity;
    [SerializeField]
    float speed;
    public bool left, right, up ,down;
    public bool goingUp;


    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        color = Random.Range(0, 255);
        circlePos = new Vector3(Width / 2, Height / 2);
        Circle(circlePos.x, circlePos.y, circleDiameter);
    }

    // Update is called once per frame
    void Update()
    {
        Background(Mathf.RoundToInt(color));

        UpdateOnMouseClick();
        circlePos += velocity * speed * Time.deltaTime;

        if (circlePos.x < 0 && !right)
        {
            right = true;
            left = false;
            velocity.x = -velocity.x;
        }
        if(circlePos.x > Width && !left)
        {
            right = false;
            left = true;
            velocity.x = -velocity.x;
        }
        if(circlePos.y < 0 && !down)
        {
            down = true;
            up = false;
            velocity.y = -velocity.y;
        }
        if (circlePos.y >Height && !up)
        {
            down = false;
            up = true;
            velocity.y = -velocity.y;
        }

        Circle(circlePos.x, circlePos.y, circleDiameter);
        //Debug.Log(circlePos);

        DrawLineOnMouseDown();
        //CircleMovement();

    }

    void UpdateOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            circlePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void DrawLineOnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            lineEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Line(circlePos.x, circlePos.y, lineEndPos.x, lineEndPos.y);
            velocity = lineEndPos - circlePos;
            //speed = velocity.magnitude;

            if (velocity.magnitude >= speedLimit)
            {
                velocity = velocity.normalized * speedLimit;
                Debug.Log(velocity);
            }

        }
    }
}
