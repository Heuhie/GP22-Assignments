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
            Destroy(gameObject, hit.length);
        }
    }
}
