using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 2f;
    public bool isGrounded;
    public float playerBoundsY;
    public LayerMask ground;
    public bool facingLeft;
    public int score;
    public string horizontalMovement;
    public string jumpButton;

    float horizontal;
    Vector2 movement;
    Rigidbody2D rb;
    Animator animator;
    AudioSource jump;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerBoundsY = GetComponent<BoxCollider2D>().bounds.extents.y + 0.1f;
        jump = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(horizontalMovement);
        movement.x = horizontal * speed;
        //CheckFalling();

        if (Input.GetButtonDown(jumpButton) && isGrounded)
        {
            Jump();
        }

        movement.y = rb.velocity.y;
        animator.SetFloat("speed", Mathf.Abs(movement.x));
        //Debug.Log(movement.x);
        rb.velocity = movement;
        FacingDirection();
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();
    }

    //Raycast for groundcheck
    void CheckIfGrounded()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - playerBoundsY), Vector2.down, 0.2f);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            //Debug.Log(hit.collider.gameObject.name);
            isGrounded = true;
            animator.SetBool("jump", false);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("jump", true);
        }

        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - playerBoundsY), Vector2.down, Color.cyan);
        Debug.Log(animator.GetBool("jump"));
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    void Jump()
    {
        isGrounded = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        animator.SetBool("jump", true);
        jump.Play();  
    }

    //Checks if player is falling, if so
    //disables jump
    void CheckFalling()
    {
        if (rb.velocity.y < -1)
        {
            isGrounded = false;
        }
    }

    //Change direction the character is looking
    //based on movement direction
    void FacingDirection()
    {
        if(horizontal < 0 && !facingLeft)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            facingLeft = true;
        }
        
        if(horizontal > 0 && facingLeft)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingLeft = false;
        }
    }


    //Check if character collides with a pickup
    //if so destroys the pickup and increments score
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            score += 10;
            Destroy(other.gameObject);
            //Debug.Log("collided");
        }
    }

}
