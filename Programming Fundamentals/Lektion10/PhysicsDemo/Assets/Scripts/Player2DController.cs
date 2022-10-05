using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DController : MonoBehaviour
{
	//Player Movement Speed
	public float speed;
	//how high we can jump
	public float jumpPower = 5;

	//Our Rigidbody2D reference
	Rigidbody2D rb2d;
	//Current movement
	Vector2 movement = new Vector2();
	//If we are on the ground
	public bool grounded;

	// Start is called before the first frame update
	void Start()
	{
		//Find our Rigidbody2D
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		//get input from player
		float x = Input.GetAxis("Horizontal");

		//Only update x direction
		movement.x = x * speed;

		//If we press jump while grounded, then Jump
		if (Input.GetButtonDown("Jump") && grounded)
		{
			//velocity jump
			//rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);

			//impulse jump (same result)
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //Reset our y speed before the jump
			rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}

		//Use our old y velocity, if movement.y = 0, then we mess with gravity
		movement.y = rb2d.velocity.y;

		//Update our movement
		rb2d.velocity = movement;
	}

	//This is not the best way of controlling if we are grounded.
	//We will look at better solutions at a later date.
	void OnTriggerEnter2D(Collider2D other)
	{
		grounded = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		grounded = false;
	}
}
	