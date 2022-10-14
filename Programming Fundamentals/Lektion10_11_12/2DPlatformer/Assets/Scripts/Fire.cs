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

    InputHandler input;

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(fireButton))
        {
            var newFireBall = Instantiate(fireBall, aim.transform.position, aim.transform.rotation);

            newFireBall.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            newFireBall.GetComponent<Rigidbody2D>().AddForce((gameObject.transform.right + new Vector3(0, input.inputVector.y)) * 100);
        }
    }

    //public void PLayExplosion()
    //{
    //    explosion.Play();
    //}
}
