using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float minX, maxX;
    public float minY, maxY;
    public Vector2 movementVector;
    public Vector2 goalPosition;
    public Vector2 worlCoordinates;
    public float worldWidth;
    public float worldHeight;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        worlCoordinates = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        minX = -worlCoordinates.x + 1f;
        maxX = worlCoordinates.x - 1f;
        minY = -worlCoordinates.y + 1f;
        maxY = worlCoordinates.y - 1f;
        Debug.Log(worlCoordinates.x);
        //goalPosition = GetNewGoal();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = goalPosition - (Vector2)transform.position;
        movementVector += direction.normalized * acceleration * Time.fixedDeltaTime;
        //Debug.Log(movementVector.magnitude);

        float distanceToTarget = ((Vector2)transform.position - goalPosition).magnitude;
        //Debug.Log(distanceToTarget);
        if (Mathf.Abs(distanceToTarget) < 0.2f)
        {
            goalPosition = GetNewGoal();
            //Debug.Log("Gets new goal");
        }
        
        if(movementVector.magnitude > maxSpeed)
        {
            movementVector = direction.normalized * maxSpeed;
            //Debug.Log("runs this");
        }

        rb.AddForce(movementVector, ForceMode2D.Impulse);

        Debug.Log(distanceToTarget);
    }

    private Vector2 GetNewGoal()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }
}
