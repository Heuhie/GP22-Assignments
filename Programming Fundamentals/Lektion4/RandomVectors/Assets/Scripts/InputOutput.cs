using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOutput : ProcessingLite.GP21
{
    public string myText;
    // Start is called before the first frame update
    void Start()
    {


        Background(0, 0, 255);
        Line(5, 2, 6, 4);
        Text("Hello", 5, 5);
        Debug.Log(Height);
    }

    // Update is called once per frame
    void Update()
    {
        Background(0, 0, 255);
        myText = "Tryck enter för att generara en ny vektor";


    }
}
