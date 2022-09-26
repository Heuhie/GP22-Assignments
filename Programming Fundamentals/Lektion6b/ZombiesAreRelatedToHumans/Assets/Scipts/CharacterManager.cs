using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : ProcessingLite.GP21
{
    public Character[] characterList;
    public int r, g, b;


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
        Background(r, g, b);
        UpdateCharacters();
    }

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

    void UpdateCharacters()
    {
        for(int i = 0; i < characterList.Length; i++)
        {
            characterList[i].UpdateCharacterMovement();
            characterList[i].DrawCharacter();
            characterList[i].CheckBounds();

            if(characterList[i].characterType == "Zombie")
            {
                characterList[i].CheckCharacterCollision(characterList[i], characterList, i +1);
            }
        }
    }
}
