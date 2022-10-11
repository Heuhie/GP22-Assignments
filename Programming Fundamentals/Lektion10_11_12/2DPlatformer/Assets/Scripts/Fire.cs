using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject aim;
    public AudioSource explosion;
    public string fireButton;

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(fireButton))
        {
            var newFireBall = Instantiate(fireBall, aim.transform.position, aim.transform.rotation);

            newFireBall.GetComponent<Rigidbody2D>().gravityScale = 0;
            newFireBall.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        }
    }

    //public void PLayExplosion()
    //{
    //    explosion.Play();
    //}
}
