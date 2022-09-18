using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOutput : ProcessingLite.GP21
{
    public string myText;
    // Start is called before the first frame update
    void Start()
    {

        myText = "Tryck enter för att generara en ny vektor";
        Background(0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        Background(0, 0, 255);
        Fill(0);
        Text(myText, Width / 2, Height / 2);


    }
}
