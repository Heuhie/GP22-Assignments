using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlane : MonoBehaviour
{
    public Gyroscope gyro;
    public float rotation = 1;
    public Transform outerPlaneTransform;
    public Transform mazeTransform;
    public BoxCollider groundBoxCollider;
    public MeshCollider groundMeshCollider;

    private Vector3 gyroRot;
    private float limitRotation = 7f;

    // Start is called before the first frame update
    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        gyroRot = Input.gyro.rotationRateUnbiased;

        outerPlaneTransform.transform.Rotate(new Vector3(-gyroRot.x * Time.deltaTime * rotation, 0, 0));
        mazeTransform.transform.Rotate(new Vector3(0, 0, -gyroRot.y * Time.deltaTime * rotation));

        LimitouterPlaneRotation();
        LimitMazeRotation();
        //Debug.Log(mazeTransform.transform.eulerAngles);
        
    }
    public void LimitouterPlaneRotation()
    {
        Vector3 currentRotation = outerPlaneTransform.rotation.eulerAngles;
        if (currentRotation.x >= limitRotation && currentRotation.x <= 180)
        {
            currentRotation.x = limitRotation;
            outerPlaneTransform.eulerAngles = currentRotation;
        }
        if (currentRotation.x <= 360 - limitRotation && currentRotation.x > 180)
        {
            currentRotation.x = 360 - limitRotation;
            outerPlaneTransform.eulerAngles = currentRotation;
        }
    }

    public void LimitMazeRotation()
    {
        Vector3 currentRotation = mazeTransform.rotation.eulerAngles;
        if (currentRotation.z >= limitRotation && currentRotation.z <= 180)
        {
            //Debug.Log("Runs first");
            currentRotation.z = limitRotation;
            mazeTransform.eulerAngles = currentRotation;
        }

        if (currentRotation.z <= 360 - limitRotation && currentRotation.z > 180)
        {
            //Debug.Log("runs second");
            currentRotation.z = 360 - limitRotation;
            mazeTransform.eulerAngles = currentRotation;
        }
    }

    public void EnableFalling()
    {
        groundBoxCollider.enabled = false;
        groundMeshCollider.GetComponent<MeshCollider>().enabled = true;
        Debug.Log("Falling enabled");
    }

    public void DisableFalling()
    {
        groundBoxCollider.GetComponent<BoxCollider>().enabled = true;
        groundMeshCollider.GetComponent<MeshCollider>().enabled = false;
        Debug.Log("Falling disabled");
    }
   
}
