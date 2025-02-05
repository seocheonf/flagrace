using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoBoost : CharacterInfo
{
    public CharacterInfoBoost()
    {

        deckInfo = new Card[5];
        deckInfo[0] = new No1Card();
        deckInfo[1] = new No1Card();
        deckInfo[2] = new No2Card();
        deckInfo[3] = new No2Card();
        deckInfo[4] = new No3Card();

        characterEffect = new BoostEffect();

        characterName = "Boost";

    }
}