using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHandler : MonoBehaviour
{
    public float timer = 0;
    public float spawnInterval = 5f;
    public List<GameObject> clouds;
    public int index;
    public GameObject cloud;
    // Start is called before the first frame update
    void Start()
    {
        clouds = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SpawnClouds();
        CheckBounds();
    }

    void SpawnClouds()
    {
        if (timer >= spawnInterval)
        {
            cloud = Instantiate(cloud);
            clouds.Add(cloud);
            index++;
            timer = 0;
        }
    }


    void CheckBounds()
    {
        foreach(GameObject cloud in clouds)
        {
            if (cloud.transform.position.x >= 25f)
            {
                Destroy(cloud);
                clouds.Remove(cloud);
                index--;
            }
        }
    }
}
