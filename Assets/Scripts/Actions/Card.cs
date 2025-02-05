using UnityEngine;

public class Card : Action
{
    public int moveRange;
    public Effect cardEffect;
    //ī��κ��� ����� ȿ��
    public CardType cardType;

    public override bool DoAction()
    {
        if (player == null) return false;

        Debug.Log("ī����!");
        EffectManager effectManager = GameManager.GameManagerInstance.EffectManagerInstance;


        effectManager.UseEffect(Timing.CARD_USE_START);

        if (cardEffect != null)
        {
            effectManager.RegisterEffect(cardEffect, player);
        }

        if (moveRange != 0)
        {
            player.Move(moveRange, player, MovePath.RUN, MoveTool.CARD);
        }

        effectManager.UseEffect(Timing.CARD_USE_END);


        return true;
    }
}