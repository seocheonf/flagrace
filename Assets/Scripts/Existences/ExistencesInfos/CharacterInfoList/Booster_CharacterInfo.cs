using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_CharacterInfo : CharacterInfo
{
    
    public Booster_CharacterInfo()
    {
        int card_1_Amount = 6;
        int card_2_Amount = card_1_Amount + 3;
        int card_3_Amount = card_2_Amount + 4;
        int card_4_Amount = card_3_Amount + 4;
        int card_5_Amount = card_4_Amount + 2;
        int card_6_Amount = card_5_Amount + 1;

        deckInfo = new Card[20];
        for (int i = 0; i < deckInfo.Length; i++)
        {
            if (i < card_1_Amount) deckInfo[i] = new Booster_Card_1();
            else if (i < card_2_Amount) deckInfo[i] = new Booster_Card_2();
            else if (i < card_3_Amount) deckInfo[i] = new Booster_Card_3();
            else if (i < card_4_Amount) deckInfo[i] = new Booster_Card_4();
            else if (i < card_5_Amount) deckInfo[i] = new Booster_Card_5();
            else deckInfo[i] = new Booster_Card_6();
            //deckInfo[i] = new Booster_Card_4();
        }

        characterEffect = new Booster_Effect();

        characterName = "Booster";
    }
}
