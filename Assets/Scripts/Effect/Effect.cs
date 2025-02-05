using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    //timing ��ü ����
    public Effect()
    {
        timing = new List<Timing>();
    }

    //-----------------������ �ʴ� ������ ��---------------------//
    protected int priority;
    public int Priority => priority;
    //�켱 ���� (������, ������, �ɵ��� : 1, 2, 3)
    protected int duration;
    //���� �ð�
    protected int times;
    //�ߵ� Ƚ��
    protected List<Timing> timing;
    //���� Ȯ�� ������
    public string description = "ȿ�� ���!";
    //-----------------������ �ʴ� ������ ��---------------------//


    //------------------���Ǹ� ���ϴ� ��----------------------//
    protected bool isUseOneCycle;
    //times�� Inf�� ���, �ൿ����Ŭ �� �ѹ� (times�� Inf�� ���, �ൿ ����Ŭ�� ���� �� ��ȸ�ϸ� Ǯ���ִ� ���� �ʿ��� ��)
    protected int currentDuration;
    //���� ���� ���ӽð�
    protected int currentTimes;
    //���� ���� �ߵ� Ƚ��
    protected Existence owner;
    public Existence Owner => owner;
    //ȿ���� ����. ���� ȿ���� ����ߴ°�? (�÷��̾�, ����, �г�)
    protected bool isDead = false;
    public bool IsDead => isDead;
    //ȿ���� �׾����� Ȯ��

    protected bool isUpdateComplete;
    public bool IsUpdateComplete
    {
        get => isUpdateComplete;
        set => isUpdateComplete = value;
    }

    //------------------���Ǹ� ���ϴ� ��----------------------//


    protected virtual bool DoWhenDie() { return default; }
    //ȿ���� ����� �� �ؾ��� ��
    public int UseEffect(Timing timing)
    {
        if (isDead) return 0;

        if (CheckUsingCondition())
        {
            if (!RunEffect(timing))
            {
                CheckDeadCondition();

                return 1;
            }
            CheckDeadCondition();
            GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.EFFECT_USE_END);
            return 2;
        }
        else
        {
            CheckDeadCondition();
            return 1;
        }
    }
    //ȿ���� ��� : 0�϶� ���, 1�϶� ���� �̴�, 2�϶� ȿ������ ����
    public bool CheckDeadCondition() 
    {
        if (isDead) return true;
        else if (currentDuration == 0 || currentTimes == 0)
        {
            DoWhenDie();
            isDead = true;
        }
        else isDead = false;

        return isDead;
    }
    //ȿ���� ��� ���� Ȯ�� �� ��� ����(��� ���� ���� = ����� �Ϸ��Ų ��(���� �� ���. �����ʹ� ���Ƶ�, ������ ��������. �� �Ϳ� ���� ó���� ���� �͸��� ó���ؾ���)). ����� �� �ؾ��� ���� �����Ѵ�.
    protected virtual bool CheckUsingCondition() { return default; }
    //ȿ���� �ߵ��Ǳ� ���� ���� Ȯ��
    protected virtual bool RunEffect(Timing timing)
    {
        Debug.Log($"ȿ���ߵ� : {description}");
        return default;
    }
    //ȿ���� ����

    //------------------���Ǹ� ���ϴ� ����, ������ �ʴ� ������ ������ �ʱ�ȭ----------------------//
    public void Initialize()
    {
        isUseOneCycle = false;
        currentDuration = duration;
        currentTimes = times;
        owner = null;
        isDead = false;
    }
    // �츮�� ȿ���� ���Ǹ� ������ �ʴ� ����, �Լ��� �� ������ �Ŀ�, ȿ���� ����� �ñ⿡ �ʱ�ȭ �����ָ� ��.
    // ��� ȿ���� ��ϵǱ����� �ʱ�ȭ�ؾ��Ѵ�.

    public void ReduceCurrentDuration(int reduceDuration)
    {
        currentDuration -= reduceDuration;
    }

    public List<Timing> GetEffectTiming()
    {
        return timing;
    }

    public void SetIsUseOneCycle(bool isUseOneCycle)
    {
        this.isUseOneCycle = isUseOneCycle;
    }

    public void SetOwner(Existence owner)
    {
        this.owner = owner;
    }

    protected void EffectMove(Player player, int steps, Existence order, MovePath movePath)
    {
        if (steps == 0) return;

        player.Move(steps, order, movePath, MoveTool.EFFECT);

    }


}
