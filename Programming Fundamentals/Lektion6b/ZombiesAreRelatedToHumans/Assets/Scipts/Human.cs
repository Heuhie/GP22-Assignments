using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Character
{
    //Constructs zombiefood
    public Human()
    {
        base.r = 255;
        base.g = 204;
        base.b = 204;

        characterType = "Human";
    }
}
