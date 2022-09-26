using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ProcessingLite.GP21
{
    public Vector2 position;
    public Vector2 velocity;
    public float speed = 2;
    public float acceleration;
    public float size = 0.5f;
    public string characterType;

    [Range(0,255)]
    public int r, g, b;


    // Character constructor
    public Character()
    {
        r = 255;
        position.x = Random.Range(1, Width - 1);
        position.y = Random.Range(1, Height - 1);
        velocity.x = Random.Range(-10, 10);
        velocity.y = Random.Range(-10, 10);
        velocity = velocity.normalized;
    }

    //Updates character movement
    public void UpdateCharacterMovement()
    {
        position += velocity * speed * Time.deltaTime;
    }

    //Draws the character at given position
    public void DrawCharacter()
    {
        Fill(r, g, b);
        Circle(position.x, position.y, size);
    }

    //Checking if character within bounds or not
    public void CheckBounds()
    {
        if (position.x - size / 2 > Width)
        {
            position.x = Mathf.Clamp(position.x, 0 - size / 2, Width + size / 2);
            position.x = 0 - size / 2;
        }

        if (position.x + size / 2 < 0)
        {
            position.x = Mathf.Clamp(position.x, 0 - size / 2, Width + size / 2);
            position.x = Width + size / 2;
        }

        if (position.y - size / 2 > Height)
        {
            position.y = Mathf.Clamp(position.y, 0 - size / 2, Height + size / 2);
            position.y = 0 - size / 2;
        }

        if (position.y + size / 2 < 0)
        {
            position.y = Mathf.Clamp(position.y, 0 - size / 2, Height + size / 2);
            position.y = Height + size / 2;
        }
    }

    //Checking if zombies collides with humans
    //if so humans are transformed to zombies
    public bool CheckCharacterCollision(Character zombie, Character[] characterList, int index)
    {
        bool hasCollided = false;

        for (int i = index; i < characterList.Length; i++)
        {
            float maxDistance = zombie.size/2 + characterList[i].size/2;

            if (characterList[i].characterType == "Human")
            {

                if (Mathf.Abs(zombie.position.x - characterList[i].position.x) > maxDistance || Mathf.Abs(zombie.position.y - characterList[i].position.y) > maxDistance)
                {
                    hasCollided = false;
                }
                else if (Vector2.Distance(zombie.position, characterList[i].position) > maxDistance)
                {
                    hasCollided = false;
                }
                else
                {
                    Debug.Log("zombieTime");
                    Vector2 tmpPosition = characterList[i].position;
                    characterList[i] = new Zombie();
                    characterList[i].position = tmpPosition;
                    hasCollided = true;
                    break;
                }
            }  
        }
        return hasCollided;
    }
}
