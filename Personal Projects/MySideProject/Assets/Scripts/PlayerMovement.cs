using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    public float maxSpeed;
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
    [SerializeField]
    private float griptreshhold = 4f;
    [SerializeField]
    private float grip;
    [SerializeField]
    private float steeringSpeedScale;
    [SerializeField]
    private float drag = 4f;


    public TrailRenderer[] wheelTrailList;
    public float vertInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //wheelTrail = GetComponent<TrailRenderer>();
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
        if (rb.velocity.magnitude < maxSpeed && vertInput != 0)
        {
            rb.drag = 0;
            Speed();
        }

        if(vertInput == 0)
        {
            Decelerate();
        }
        Steering();
        SideDrift();
        Tiremarks();
        Debug.Log(rb.velocity.magnitude);
    }

    void Speed()
    {
        Vector2 speedVector = transform.right * vertInput * acceleration;
        rb.AddForce(speedVector, ForceMode2D.Force);
    }

    void Decelerate()
    {
        rb.drag = Mathf.Lerp(rb.drag, drag, Time.deltaTime * 3);
    }

    void Steering()
    {
        float speedScale = rb.velocity.magnitude / steeringSpeedScale;
        speedScale = Mathf.Clamp01(speedScale);
        turnAngle -= zAxisRot * turningspeed * speedScale;
       
        Vector3 transformDirection = rb.transform.InverseTransformDirection(rb.velocity);
        


        if (transformDirection.x >= 0)
        {
            rb.MoveRotation(turnAngle);
        }
        else
            rb.MoveRotation(-turnAngle);
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
        Debug.Log(sideVel);
 
    }

    void Tiremarks()
    {
        grip = Mathf.Abs(Vector3.Dot(transform.up, rb.velocity));
        if(grip < griptreshhold)
        {
            foreach(TrailRenderer wheel in wheelTrailList)
            {
                wheel.emitting = false;
            }
               
        }
        else
        {
            foreach(TrailRenderer wheel in wheelTrailList)
            {
                wheel.emitting = true;
            }
        }
    }
}
