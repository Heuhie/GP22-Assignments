using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHandler : MonoBehaviour
{
    public float timer = 0;
    public float spawnInterval = 5f;
    public List<GameObject> clouds;
    public int index = 0;
    public GameObject cloudPrefab;


    // Start is called before the first frame update
    void Start()
    {
        clouds = new List<GameObject>();
        StartupClouds();
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
            
            GameObject cloud = Instantiate(cloudPrefab);
            cloud.GetComponent<Cloud>().SpawnPosition();
            clouds.Add(cloud);
            index++;
            timer = 0;
        }
    }

    void StartupClouds()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject cloud = Instantiate(cloudPrefab);
            clouds.Add(cloud);
            clouds[i].GetComponent<Cloud>().StartCloudPosition(Random.Range(-14f, 5f), Random.Range(-3f, 5f));
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
