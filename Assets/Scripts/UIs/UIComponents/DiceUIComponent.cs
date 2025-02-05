using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceUIComponent : UIComponent
{
    DiceUIDetail diceUIDetail;

    //일단, 외부의 UI를 그대로 가져다 쓰게 만듬.
    //UI를 이 DiceUI가 직접 생성하여 할 수 있음.
    public DiceUIComponent(DiceUIDetail diceUIDetail, GameObject targetGameObject) : base(targetGameObject, 100f)
    {
        this.diceUIDetail = diceUIDetail;
    }

    public void SetDiceUINumber(int number)
    {
        diceUIDetail.SetOnDiceAnimation(number);
        AnnounceActive();
    }

    public override bool UpdateAnimation()
    {
        return diceUIDetail.GetIsAnimation();
    }

    public override void EndAnimation()
    {

    }

}
