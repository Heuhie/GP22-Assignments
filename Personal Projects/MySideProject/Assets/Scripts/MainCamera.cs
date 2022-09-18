using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Transform carToFollow;

    private float x, y;

    public PlayerMovement playerMovement;
    public float zMaxZoom = -11f;
    public float zMinZoom = -5f;
    public float stopScalingSpeed = 10f;
    public float zoomScaling;

    private float zZoom = 1;

    // Start is called before the first frame update
    void Start()
    {
        zoomScaling = (zMaxZoom - zMinZoom) / stopScalingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        x = carToFollow.position.x;
        y = carToFollow.position.y;

        zZoom = zMinZoom + (zoomScaling * playerMovement.currentspeed);

        zZoom = Mathf.Clamp(zZoom, zMaxZoom, zMinZoom);

        transform.position = new Vector3(x, y, -10f);
        Camera.main.orthographicSize = -zZoom;
        Debug.Log(zZoom);
    }
}
