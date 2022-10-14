using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int playerHealth = 50;
    public bool isDead;

    // Start is called before the first frame update

    public void TakeDamage()
    {
        playerHealth -= 10;
        if(playerHealth <= 0)
        {
            isDead = true;
        }
    }
}
