using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float acceleration = 5f;
    [SerializeField]
    private float drift = 1f;
    [SerializeField]
    private float turningspeed = 5f;
    [SerializeField]
    private float zAxisRot;
    [SerializeField]
    private float turnAngle;


    public TrailRenderer wheelTrail;
    public float vertInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wheelTrail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //vertInput = Input.GetAxisRaw("Vertical") * acceleration;
        //transform.localPosition += transform.right * vertInput;
        //zAxisRot = Input.GetAxisRaw("Horizontal") * turningspeed;
        //transform.eulerAngles += new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zAxisRot);

    }

    private void FixedUpdate()
    {
        Speed();
        Steering();
        SideDrift();
    }

    void Speed()
    {
        Vector2 speedVector = transform.right * vertInput * acceleration;

        rb.AddForce(speedVector, ForceMode2D.Force);
    }

    void Steering()
    {
        turnAngle -= zAxisRot * turningspeed;
        rb.MoveRotation(turnAngle);
    }

    //used by PlayerInput
    public void GetInput(Vector2 controllerInput)
    {
        vertInput = controllerInput.x;
        zAxisRot = controllerInput.y;
    }

    void SideDrift()
    {
        Vector2 forwardVel = transform.right * Vector2.Dot(rb.velocity, transform.right);
        Vector2 sideVel = transform.up * Vector3.Dot(rb.velocity, transform.up);

        rb.velocity = forwardVel + (sideVel * drift);
    }
}
