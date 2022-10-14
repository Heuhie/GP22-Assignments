using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip spawn;
    public AudioClip hit;
    public string ignorePlayerName;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawn);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != ignorePlayerName)
        {
            //Debug.Log(collision.gameObject.name);
            audioSource.PlayOneShot(hit);
            //Destroy(gameObject, hit.length);
        }
        
        if(collision.gameObject.tag == "Player" && collision.gameObject.name != ignorePlayerName)
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage();
            Destroy(gameObject, hit.length);

            if (collision.gameObject.GetComponent<PlayerStats>().isDead == true)
            {
                Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
                Debug.Log("Gets to add force");
                collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
                collision.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(gameObject.GetComponent<Rigidbody2D>().velocity * 10, gameObject.transform.position);
                Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity * (new Vector2(1, 1) * 100));
            }
        }

    }
}
