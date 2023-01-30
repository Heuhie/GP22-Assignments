using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    public InGameMessageData message = new InGameMessageData();

    public GameObject textGameObject;

    GameObject myText;

    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = FindObjectOfType<Canvas>().transform;

        Vector3 UIPosition = Camera.main.WorldToScreenPoint(transform.position);
        UIPosition.y += 40;

        myText = Instantiate(textGameObject, UIPosition, transform.rotation, canvas);
        myText.SetActive(false);
        myText.GetComponent<TextMeshProUGUI>().text = message.messageBody;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            myText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            myText.SetActive(false);
        }
    }
}
