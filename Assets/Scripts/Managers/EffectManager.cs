using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager
{
    protected Dictionary<Timing, List<Effect>> effectsByTiming;

    // ���ѷ��� ������ġ
    private int repeatCount;
    private const int repeatCountMax = 30;


    public EffectManager()
    {
        // Dictionary �ʱ�ȭ
        effectsByTiming = new Dictionary<Timing, List<Effect>>();

        // Dictionary Key,Value�� �ʱ�ȭ
        for (Timing i = 0; i <= Timing.TURN_END; i++)
        {
            effectsByTiming.Add(i, new List<Effect>());
        }
    }

    public bool RegisterEffect(Effect effect, Existence owner)
    {
        if (effect == null) return false; // effect�� ���ٸ� ����� �ʿ����
        if (owner == null) return false; // ����ڰ� ������ �߸��� effect���

        // effect ����� �ʱ�ȭ
        effect.Initialize();
        // effect�� ����� ���
        effect.SetOwner(owner);


        // effect�� List<Timing> �޾ƿͼ�
        List<Timing> effectTiming = effect.GetEffectTiming();
        for (int i = 0; i < effectTiming.Count; i++)
        {
            // Timing���´� List<Effect>�� effect �߰�
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

        // ���ѷ��� ������ġ.
        if (repeatCount > repeatCountMax)
        {
            Debug.Log("out");
            repeatCount = 0;
            return false;
        }

        // IEnumerable<Effect>
        // IOrderedEnumerable<Effect>
        // var




        //���� �õ� ��
        bool isEffectCheck = false;
        while (true)
        {
            //����Ʈ�ȿ� ����Ȱ� �ִ��� Ȯ���ϴ� ���� �ʱ�ȭ.
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

            //���� ����Ʈ �ȿ� ����Ȱ� ���ٰ� �Ѵٸ� ������
            if(!isEffectCheck)
                return true;
            //���� ����Ʈ �ȿ� ����Ȱ� �ִٸ� �ٽ� ���ǵ��� üũ�ϰ� �Ѵ�.
        }



        //����
        /*
        List<Effect> checkList =
          (from effect in effectsByTiming[timing]
           orderby effect.Priority, effect.Owner.ExistenceType
           select effect).ToList();



        foreach (Effect effect in checkList)
        {
            if (effect.UseEffect(timing) == 2)
            {
                // ���ѷ��� ������ġ.
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
