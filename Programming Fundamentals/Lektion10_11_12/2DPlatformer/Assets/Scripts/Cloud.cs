using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed;
    float positionY;
    float cloudScale;

    private void Start()
    {
        SpawnPosition();
        SpawnSize();
        speed = Random.Range(1f, 3f);
    }

    private void Update()
    {
        UpdatePosition();
    }

    void SpawnPosition()
    {
        positionY = Random.Range(0f, 8f);
        transform.position = new Vector3(-25f, positionY);
    }

    void SpawnSize()
    {
        cloudScale = Random.Range(1f, 3f);
        transform.localScale = new Vector3(cloudScale, cloudScale, 1);
    }


    void UpdatePosition()
    {
        Vector3 movement = Vector3.right * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
