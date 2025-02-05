using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceUIComponent : UIComponent
{
    DiceUIDetail diceUIDetail;

    //�ϴ�, �ܺ��� UI�� �״�� ������ ���� ����.
    //UI�� �� DiceUI�� ���� �����Ͽ� �� �� ����.
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
