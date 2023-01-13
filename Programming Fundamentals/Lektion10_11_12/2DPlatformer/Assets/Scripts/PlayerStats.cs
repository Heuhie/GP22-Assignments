using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PickupManager pickUpManager;
    public Slider healthbar;
    public int playerHealth = 50;
    public bool isDead;
    public PowerUpType playerPowerUp;
    public float powerUpTime;
    public float timer;
    public bool hasPowerUp;
    public bool isPowerUpTriggered;

    public float maxHealth;

    // Start is called before the first frame update
    private void Start()
    {
        pickUpManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PickupManager>();
        pickUpManager.currentPowerUp = PowerUpType.Normal;
        healthbar.maxValue = playerHealth;
        healthbar.value = playerHealth;
        maxHealth = playerHealth;

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
        healthbar.value -= 10;
        if(playerHealth <= 0)
        {
            isDead = true;
            FindObjectOfType<GameManager>().GetComponent<GameManager>().GameOver();
        }
    }
}
