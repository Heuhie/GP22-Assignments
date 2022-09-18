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
    private float brakeforce = 10f;
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

    private bool braking = false;

    public TrailRenderer[] wheelTrailList;
    public float vertInput;
    public float currentspeed;

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
        if (vertInput > 0 && rb.velocity.magnitude < maxSpeed)
        {
            rb.drag = 0;
            Accelerate();
        }

        if(vertInput < 0 && rb.velocity.magnitude < maxSpeed/2)
        {
            rb.drag = 0;
            Accelerate();
        }

        if(vertInput == 0)
        {
            Decelerate();
        }

        if(rb.velocity.magnitude > 0 && vertInput < 0)
        {
            Braking();
            braking = true;
        }
        else
        {
            braking = false;
        }

        Steering();
        SideDrift();
        Tiremarks();

        currentspeed = rb.velocity.magnitude;

        //Debug.Log(rb.velocity.magnitude);

    }

    void Accelerate()
    {
        Vector2 speedVector = transform.right * vertInput * acceleration;
        rb.AddForce(speedVector, ForceMode2D.Force);
    }

    void Decelerate()
    {
        rb.drag = Mathf.Lerp(rb.drag, drag, Time.deltaTime * 3);
    }

    void Braking()
    {
        rb.drag = brakeforce;
    }

    void Steering()
    {
        float speedScale = rb.velocity.magnitude / steeringSpeedScale;
        speedScale = Mathf.Clamp01(speedScale);

        Vector3 transformDirection = rb.transform.InverseTransformDirection(rb.velocity);
        if (transformDirection.x <= 0)
            turnAngle += zAxisRot * turningspeed * speedScale;
        else
            turnAngle -= zAxisRot * turningspeed * speedScale;

        rb.MoveRotation(turnAngle);
    }

    //used by PlayerInput
    public void GetInput(Vector2 controllerInput)
    {
        vertInput = controllerInput.x;
        zAxisRot = controllerInput.y;
        //Debug.Log(vertInput);
    }

    void SideDrift()
    {
        Vector2 forwardVel = transform.right * Vector2.Dot(rb.velocity, transform.right);
        Vector2 sideVel = transform.up * Vector3.Dot(rb.velocity, transform.up);

        rb.velocity = forwardVel + (sideVel * drift);
        //Debug.Log(sideVel);
 
    }

    void Tiremarks()
    {
        grip = Mathf.Abs(Vector3.Dot(transform.up, rb.velocity));
        if(grip > griptreshhold || braking)
        {
            foreach (TrailRenderer wheel in wheelTrailList)
            {
                wheel.emitting = true;
            }
        }
        else
        {
            foreach (TrailRenderer wheel in wheelTrailList)
            {
                wheel.emitting = false;
            }
        }
    }
}
