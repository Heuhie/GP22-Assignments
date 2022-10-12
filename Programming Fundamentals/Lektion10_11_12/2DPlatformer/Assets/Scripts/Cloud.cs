using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed;
    float positionY;
    float cloudScale;
    public SpriteRenderer zLayer;

    private void Start()
    {
        zLayer = GetComponent<SpriteRenderer>();
        SpawnSize();
        SetSpriteLayer();
        speed = Random.Range(0.5f, 2f);
    }

    private void Update()
    {
        UpdatePosition();
    }

    public void StartCloudPosition(float xPosition, float yPosition)
    {
        transform.position = new Vector3(xPosition, yPosition);
    }

    public void SpawnPosition()
    {
        positionY = Random.Range(-2f, 8f);
        transform.position = new Vector3(-25f, positionY);
    }

    void SetSpriteLayer()
    {
        int layer = Random.Range(-1, 2);
        if (layer == 0)
            layer = -1;
        if (layer == 1)
            layer = 2;

        zLayer.sortingOrder = layer;
    }

    void SpawnSize()
    {
        cloudScale = Random.Range(2f, 5f);
        transform.localScale = new Vector3(cloudScale, cloudScale, 1);
    }


    void UpdatePosition()
    {
        Vector3 movement = Vector3.right * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
