using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector2(-2, 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector2(-2, 2);

        }

        if(other.gameObject.CompareTag("NotPlayer"))
        {
            other.gameObject.transform.position = new Vector2(-2, 3);
        }
    }
}
