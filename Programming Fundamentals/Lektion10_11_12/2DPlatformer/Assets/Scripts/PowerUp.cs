using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            playerStats.hasPowerUp = true;

            if (gameObject.name.Contains("Grow"))
            {
                playerStats.pickUpManager.ChangePowerUpState(PowerUpType.Grow);   
            }  
            
            if(gameObject.name.Contains("Speed"))
            {
                playerStats.pickUpManager.ChangePowerUpState(PowerUpType.Speed);
            }
        }
    }
}
