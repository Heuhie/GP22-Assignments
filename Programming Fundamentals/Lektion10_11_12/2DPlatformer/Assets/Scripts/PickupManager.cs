using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Normal, Grow, Speed
}

public class PickupManager : MonoBehaviour
{
    public PowerUpType currentPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        currentPowerUp = PowerUpType.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePowerUpState(PowerUpType powerUpState)
    {
        currentPowerUp = powerUpState;
    }

    public void Grow(Transform character)
    {
        character.localScale = new Vector3(transform.localScale.x * 2f, transform.localScale.y * 2f, 1);
    }

    public void Shrink(Transform character)
    {
        character.localScale = new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2f, 1);
    }

    public void SpeedUp(GameObject player)
    {
        player.GetComponent<PlayerMovement>().speed *= 2;
    }

    public void NormalSpeed(GameObject player)
    {
        player.GetComponent<PlayerMovement>().speed /= 2;
    }
}
