using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : Effect
{
    public BoostEffect()
    {
        //-----------------������ �ʴ� ������ ��---------------------//
        priority = 2;
        //�켱 ���� (������, ������, �ɵ��� : 1, 2, 3)
        duration = 4;
        //���� �ð�
        times = 5;
        //�ߵ� Ƚ��
        timing.Add(Timing.EFFECT_USE_END);
        timing.Add(Timing.CARD_MOVE_AFTER);
        //���� Ȯ�� ������
        description = "Boost ȿ�� �ߵ�!";
        //-----------------������ �ʴ� ������ ��---------------------//
    }

    protected override bool RunEffect(Timing timing)
    {
        base.RunEffect(timing);
        return true;
    }
}

public class SemiEffect : Effect
{
    public SemiEffect()
    {
        //-----------------������ �ʴ� ������ ��---------------------//
        priority = 2;
        //�켱 ���� (������, ������, �ɵ��� : 1, 2, 3)
        duration = 4;
        //���� �ð�
        times = 5;
        //�ߵ� Ƚ��
        timing.Add(Timing.EFFECT_USE_END);
        timing.Add(Timing.CARD_MOVE_AFTER);
        //���� Ȯ�� ������
        description = "Semi ȿ�� �ߵ�!";
        //-----------------������ �ʴ� ������ ��---------------------//
    }

    protected override bool RunEffect(Timing timing)
    {
        base.RunEffect(timing);
        return true;
    }

}