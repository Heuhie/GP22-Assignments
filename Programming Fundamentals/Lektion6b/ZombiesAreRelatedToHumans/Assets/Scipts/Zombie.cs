using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Character
{
    //Constructs a badass evil zombie
    public Zombie()
    {
        r = 51;
        g = 102;
        b = 0;
        speed = 1;

        characterType = "Zombie";
    }
}
