using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireBallSpeed = 100;
    public GameObject fireBall;
    public GameObject aim;
    public AudioSource explosion;
    public string fireButton;
    public float timer;
    public float fireRate = 1;

    InputHandler input;

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputHandler>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButtonDown(fireButton) && timer >= fireRate)
        {
            var newFireBall = Instantiate(fireBall, aim.transform.position, aim.transform.rotation);

            newFireBall.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            newFireBall.GetComponent<Rigidbody2D>().AddForce((gameObject.transform.right + new Vector3(0, input.inputVector.y)) * 150);
            timer = 0;
        }
    }

    //public void PLayExplosion()
    //{
    //    explosion.Play();
    //}
}
