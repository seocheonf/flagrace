using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_Card_1 : Card
{
    public Booster_Card_1()
    {
        moveRange = 1;
        cardEffect = new Booster_Card_Effect_1();
        cardType = CardType.CHARACTER;
    }
}
public class Booster_Card_2 : Card
{
    public Booster_Card_2()
    {
        moveRange = 2;
        cardEffect = new Booster_Card_Effect_2();
        cardType = CardType.CHARACTER;
    }
}
public class Booster_Card_3 : Card
{
    public Booster_Card_3()
    {
        moveRange = 3;
        cardEffect = new Booster_Card_Effect_3();
        cardType = CardType.CHARACTER;
    }
}
public class Booster_Card_4 : Card
{
    public Booster_Card_4()
    {
        moveRange = -4;
        cardEffect = new Booster_Card_Effect_4();
        cardType = CardType.CHARACTER;
    }
}
public class Booster_Card_5 : Card
{
    public Booster_Card_5()
    {
        moveRange = 5;
        cardEffect = new Booster_Card_Effect_5();
        cardType = CardType.CHARACTER;
    }
}
public class Booster_Card_6 : Card
{
    public Booster_Card_6()
    {
        moveRange = 6;
        cardEffect = new Booster_Card_Effect_6();
        cardType = CardType.CHARACTER;
    }
}
