using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHandler : MonoBehaviour
{
    public float timer = 0;
    public float spawnInterval = 5f;
    public List<GameObject> clouds;
    public int index = 0;
    public GameObject cloud;
    // Start is called before the first frame update
    void Start()
    {
        clouds = new List<GameObject>();
        cloud = Instantiate(cloud);
        clouds.Add(cloud);
        cloud = Instantiate(cloud);
        clouds.Add(cloud);
        cloud = Instantiate(cloud);
        clouds.Add(cloud);
        cloud = Instantiate(cloud);
        clouds.Add(cloud);
        cloud = Instantiate(cloud);
        clouds.Add(cloud);
        cloud = Instantiate(cloud);
        clouds.Add(cloud);
        cloud = Instantiate(cloud);
        clouds.Add(cloud);
        clouds[0].GetComponent<Cloud>().StartCloudPosition(-13.5f, -1f);
        clouds[1].GetComponent<Cloud>().StartCloudPosition(-10f, 3.5f);
        clouds[2].GetComponent<Cloud>().StartCloudPosition(-5f, 5f);
        clouds[3].GetComponent<Cloud>().StartCloudPosition(-1f, 3.3f);
        clouds[4].GetComponent<Cloud>().StartCloudPosition(1.5f, 0);
        clouds[5].GetComponent<Cloud>().StartCloudPosition(4f, 2f);
        clouds[6].GetComponent<Cloud>().StartCloudPosition(5.6f, 3f);
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
            cloud.GetComponent<Cloud>().SpawnPosition();
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
                return;
            }
        }
    }
}
