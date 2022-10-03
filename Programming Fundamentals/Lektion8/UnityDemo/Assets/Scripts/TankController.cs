using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float turningSpeed = 90;
    public float speed = 10;

    private float angle;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        angle += Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;

        rb.MoveRotation(angle);

        float y = Input.GetAxis("Vertical");
        rb.velocity = rb.transform.up * y * speed;

       
    }
}
