using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMessage : MonoBehaviour
{
    public GameObject messageInputObject;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            messageInputObject.SetActive(true);
        }
    }
}
