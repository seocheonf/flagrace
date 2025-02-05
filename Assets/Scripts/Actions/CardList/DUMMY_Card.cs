using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No1Card : Card
{
    public No1Card()
    {
        moveRange = 1;
        cardEffect = new SemiEffect();
        cardType = CardType.CHARACTER;
    }

    public override bool DoAction()
    {
        return base.DoAction();
    }

}

public class No2Card : Card
{
    public No2Card()
    {
        moveRange = 3;
        cardEffect = new SemiEffect();
        cardType = CardType.CHARACTER;
    }

    public override bool DoAction()
    {
        return base.DoAction();
    }

}

public class No3Card : Card
{
    public No3Card()
    {
        moveRange = 2;
        cardEffect = new BoostEffect();
        cardType = CardType.CHARACTER;
    }

    public override bool DoAction()
    {
        return base.DoAction();
    }

}

public class No4Card : Card
{
    public No4Card()
    {
        moveRange = 5;
        cardEffect = new BoostEffect();
        cardType = CardType.NEUTRAL;
    }

    public override bool DoAction()
    {
        return base.DoAction();
    }

}