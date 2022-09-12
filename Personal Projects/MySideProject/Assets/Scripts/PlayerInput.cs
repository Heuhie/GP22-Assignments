using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 carInput = Vector2.zero;
        carInput.x = Input.GetAxisRaw("Vertical");
        carInput.y = Input.GetAxisRaw("Horizontal");

        playerMovement.GetInput(carInput);
    }
}
