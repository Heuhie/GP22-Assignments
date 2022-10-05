using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;

    //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        //Turn of gravity
        rb2d.gravityScale = 0;

        rb2d.drag = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        //Use the camera to convert pixel postion to world position
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = Vector2.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);
        //Set our position to the mouse world position
        //transform.position = mousePos;
        transform.up = (Vector3)mousePos - transform.position;
        //TranslateMovement();
        //VelocityMovement();
    }

    void FixedUpdate()
    {
        ////Store the current horizontal input in the float moveHorizontal.
        //float moveHorizontal = Input.GetAxis("Horizontal");

        ////Store the current vertical input in the float moveVertical.
        //float moveVertical = Input.GetAxis("Vertical");

        ////Use the two store floats to create a new Vector2 variable movement.
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        ////Call the AddForce function of our Rigidbody2D rb2d supplying movement
        ////multiplied by speed to move our player.
        //rb2d.AddForce(movement * speed);
    }

    void TranslateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, 0) * speed * Time.deltaTime;

        transform.Translate(movement);
    }

    void VelocityMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //We don't need to multiply with Time.deltaTime since it's already the right unit.
        Vector2 movement = new Vector2(x, y) * speed;

        rb2d.velocity = movement;
    }
}
