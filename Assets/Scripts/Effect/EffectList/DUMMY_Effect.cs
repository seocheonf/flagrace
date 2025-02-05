using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : Effect
{
    public BoostEffect()
    {
        //-----------------변하지 않는 고유한 값---------------------//
        priority = 2;
        //우선 순위 (버프형, 함정형, 능동형 : 1, 2, 3)
        duration = 4;
        //지속 시간
        times = 5;
        //발동 횟수
        timing.Add(Timing.EFFECT_USE_END);
        timing.Add(Timing.CARD_MOVE_AFTER);
        //조건 확인 ‘때’
        description = "Boost 효과 발동!";
        //-----------------변하지 않는 고유한 값---------------------//
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
        //-----------------변하지 않는 고유한 값---------------------//
        priority = 2;
        //우선 순위 (버프형, 함정형, 능동형 : 1, 2, 3)
        duration = 4;
        //지속 시간
        times = 5;
        //발동 횟수
        timing.Add(Timing.EFFECT_USE_END);
        timing.Add(Timing.CARD_MOVE_AFTER);
        //조건 확인 ‘때’
        description = "Semi 효과 발동!";
        //-----------------변하지 않는 고유한 값---------------------//
    }

    protected override bool RunEffect(Timing timing)
    {
        base.RunEffect(timing);
        return true;
    }

}