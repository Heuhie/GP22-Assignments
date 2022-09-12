using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Transform carToFollow;
    private float x, y;

    public float zZoom = -5f;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, zZoom);
    }

    // Update is called once per frame
    void Update()
    {
        x = carToFollow.position.x;
        y = carToFollow.position.y;

        transform.position = new Vector3(x, y, zZoom);
    }
}
