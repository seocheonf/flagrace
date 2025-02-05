using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager
{
    protected Dictionary<Timing, List<Effect>> effectsByTiming;

    // 무한루프 안전장치
    private int repeatCount;
    private const int repeatCountMax = 30;


    public EffectManager()
    {
        // Dictionary 초기화
        effectsByTiming = new Dictionary<Timing, List<Effect>>();

        // Dictionary Key,Value값 초기화
        for (Timing i = 0; i <= Timing.TURN_END; i++)
        {
            effectsByTiming.Add(i, new List<Effect>());
        }
    }

    public bool RegisterEffect(Effect effect, Existence owner)
    {
        if (effect == null) return false; // effect가 없다면 등록할 필요없음
        if (owner == null) return false; // 사용자가 없으면 잘못된 effect등록

        // effect 등록전 초기화
        effect.Initialize();
        // effect의 사용자 등록
        effect.SetOwner(owner);


        // effect의 List<Timing> 받아와서
        List<Timing> effectTiming = effect.GetEffectTiming();
        for (int i = 0; i < effectTiming.Count; i++)
        {
            // Timing에맞는 List<Effect>에 effect 추가
            effectsByTiming[effectTiming[i]].Add(effect);
        }

        return true;

    }
    public bool UseEffect(Timing timing)
    {
        //Debug.Log($"{timing}.Count : {effectsByTiming[timing].Count}");
        //for (int i = 0; i < effectsByTiming[timing].Count; i++)
        //{
        //    Debug.Log($"{timing} Result : {effectsByTiming[timing][i].UseEffect(timing)}");
        //}

        if (effectsByTiming[timing].Count == 0) return false;

        // 무한루프 안전장치.
        if (repeatCount > repeatCountMax)
        {
            Debug.Log("out");
            repeatCount = 0;
            return false;
        }

        // IEnumerable<Effect>
        // IOrderedEnumerable<Effect>
        // var




        //수정 시도 본
        bool isEffectCheck = false;
        while (true)
        {
            //리스트안에 실행된게 있는지 확인하는 변수 초기화.
            isEffectCheck = false;

            List<Effect> checkList =
               (from effect in effectsByTiming[timing]
                orderby effect.Priority, effect.Owner.ExistenceType
                select effect).ToList();

            foreach (Effect effect in checkList)
            {
                if (effect.UseEffect(timing) == 2)
                {
                    isEffectCheck = true;
                    break;
                }
            }

            //만약 리스트 안에 실행된게 없다고 한다면 끝내고
            if(!isEffectCheck)
                return true;
            //만약 리스트 안에 실행된게 있다면 다시 조건들을 체크하게 한다.
        }



        //기존
        /*
        List<Effect> checkList =
          (from effect in effectsByTiming[timing]
           orderby effect.Priority, effect.Owner.ExistenceType
           select effect).ToList();



        foreach (Effect effect in checkList)
        {
            if (effect.UseEffect(timing) == 2)
            {
                // 무한루프 안전장치.
                repeatCount++;
                UseEffect(timing);
                return true;
            }
        }
        */



        repeatCount = 0;
        return false;
    }

    //public bool UpdateEffect(int reducedDuration = 0, bool isUseOneCycle = false)
    //{
    //    return default;
    //}

    private bool UpdateEffect(Effect effect, int reducedDuration = 0, bool isUseOneCycle = false)
    {
        if (effect == null) return false;
        if (effect.IsDead) return true;
        if (effect.IsUpdateComplete) return true;

        effect.ReduceCurrentDuration(reducedDuration);
        effect.SetIsUseOneCycle(isUseOneCycle);
        effect.CheckDeadCondition();

        effect.IsUpdateComplete = true;

        return true;
    }

    public bool UpdateEntireEffectList(Phase currentPhase)
    {
        int reduceDuration = currentPhase == Phase.TURN_END ? 1 : 0;

        for (Timing i = 0; i <= Timing.TURN_END; i++)
        {
            for (int j = 0; j < effectsByTiming[i].Count; j++)
            {
                UpdateEffect(effectsByTiming[i][j], reduceDuration);
            }

            effectsByTiming[i].RemoveAll(effect => effect.IsDead);

            //for (int j = 0; j < effectsByTiming[i].Count; j++)
            //{
            //    effectsByTiming[i][j].IsUpdateComplete = false;
            //}
        }

        for (Timing i = 0; i <= Timing.TURN_END; i++)
        {
            for (int j = 0; j < effectsByTiming[i].Count; j++)
            {
                effectsByTiming[i][j].IsUpdateComplete = false;
            }
        }
        return true;
    }

    public bool RemoveEffect(Effect effect)
    {
        if (effect == null) return false;
        // if (!effect.IsDead) return false;

        List<Timing> effectTiming = effect.GetEffectTiming();
        for (int i = 0; i < effectTiming.Count; i++)
        {
            effectsByTiming[effectTiming[i]].Remove(effect);
        }

        return true;
    }

    private bool RemoveEffect(List<Effect> effect)
    {
        if (effect.Count == 0) return false;

        for (int i = 0; i < effect.Count; i++)
        {
            RemoveEffect(effect[i]);
        }

        return true;
    }
}
