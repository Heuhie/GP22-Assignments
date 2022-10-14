using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public string horizontalInput;
    public string verticalInput;
    public string jumpButton;
    public float horizontal;
    public float vertical;
    public Vector2 inputVector;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw(horizontalInput);
        vertical = Input.GetAxisRaw(verticalInput); 
        inputVector = new Vector2(horizontal, vertical);
    }
}
