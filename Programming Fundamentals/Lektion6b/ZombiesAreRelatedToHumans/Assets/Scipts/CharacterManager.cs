using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : ProcessingLite.GP21
{
    public Character[] characterList;
    public int r, g, b;
    public bool gameOver;
    public float extinctionTime;


    // Start is called before the first frame update
    void Start()
    {
        Background(r, g, b);
        characterList = new Character[100];
        InitCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Background(r, g, b);
            UpdateCharacters();
            CheckGameOver();
        }
        else
        {
            Background(255, 0, 0);
            TextSize(30);
            Fill(0, 255, 0);
            Text("GaMeOVer", Width/2, Height/2);
            Text("It took the Zombies " + extinctionTime + " seconds to rule the world", Width / 2, Height / 2 - 3);
        }
    }

    //Fills the array with characters
    void InitCharacters()
    {
        for(int i = 0; i < characterList.Length; i++)
        {
            if(i == 0)
            {
                characterList[i] = new Zombie();
            }
            else
            {
                characterList[i] = new Human();
            }
        }
    }

    //Updates position, checks collisions and boundries
    //also draws character
    void UpdateCharacters()
    {
        for(int i = 0; i < characterList.Length; i++)
        {
            characterList[i].UpdateCharacterMovement();
            characterList[i].DrawCharacter();
            characterList[i].CheckBounds();

            if(characterList[i].characterType == "Zombie")
            {
                characterList[i].CheckCharacterCollision(characterList[i], characterList);
            }
        }
    }

    //Checking how many zombies there are
    int ZombieCount()
    {
        int zombieCount = 0;

        for(int i = 0; i < characterList.Length; i++)
        {
            if(characterList[i].characterType == "Zombie")
            {
                zombieCount++;
            }
        }
        return zombieCount;
    }

    //Checking if there is as many zombies as
    //there is characters in the array, if so
    //sets gameOver to true
    void CheckGameOver()
    {
        if(ZombieCount() == characterList.Length)
        {
            extinctionTime = Time.realtimeSinceStartup;
            gameOver = true;
        }
    }
}
