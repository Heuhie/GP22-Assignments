using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PickupManager pickUpManager;
    public int playerHealth = 50;
    public bool isDead;
    public PowerUpType playerPowerUp;
    public float powerUpTime;
    public float timer;
    public bool hasPowerUp;
    public bool isPowerUpTriggered;

    // Start is called before the first frame update
    private void Start()
    {
        pickUpManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PickupManager>();
        pickUpManager.currentPowerUp = PowerUpType.Normal;
    }

    private void Update()
    {
        if(hasPowerUp)
        {
            timer += Time.deltaTime;

            if(pickUpManager.currentPowerUp == PowerUpType.Grow && !isPowerUpTriggered)
            {
                pickUpManager.Grow(transform);
                isPowerUpTriggered = true;
            }

            if(pickUpManager.currentPowerUp == PowerUpType.Speed && !isPowerUpTriggered)
            {
                pickUpManager.SpeedUp(gameObject);
                isPowerUpTriggered = true;
            }

            if(isPowerUpTriggered && timer >= powerUpTime)
            {
                if (pickUpManager.currentPowerUp == PowerUpType.Grow)
                {
                    pickUpManager.Shrink(transform);
                }

                if(pickUpManager.currentPowerUp == PowerUpType.Speed)
                {
                    pickUpManager.NormalSpeed(gameObject);
                }

                isPowerUpTriggered = false;
                timer = 0;
                hasPowerUp = false;
                pickUpManager.currentPowerUp = PowerUpType.Normal;
            }
        }
    }

    
    public void TakeDamage()
    {
        playerHealth -= 10;
        if(playerHealth <= 0)
        {
            isDead = true;
        }
    }
}
