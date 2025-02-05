using UnityEngine;

public class Card : Action
{
    public int moveRange;
    public Effect cardEffect;
    //카드로부터 등록할 효과
    public CardType cardType;

    public override bool DoAction()
    {
        if (player == null) return false;

        Debug.Log("카드사용!");
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