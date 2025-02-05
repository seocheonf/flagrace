using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    //timing 객체 생성
    public Effect()
    {
        timing = new List<Timing>();
    }

    //-----------------변하지 않는 고유한 값---------------------//
    protected int priority;
    public int Priority => priority;
    //우선 순위 (버프형, 함정형, 능동형 : 1, 2, 3)
    protected int duration;
    //지속 시간
    protected int times;
    //발동 횟수
    protected List<Timing> timing;
    //조건 확인 ‘때’
    public string description = "효과 사용!";
    //-----------------변하지 않는 고유한 값---------------------//


    //------------------사용되면 변하는 값----------------------//
    protected bool isUseOneCycle;
    //times가 Inf일 경우, 행동사이클 당 한번 (times가 Inf일 경우, 행동 사이클의 시작 시 순회하며 풀어주는 것이 필요할 듯)
    protected int currentDuration;
    //현재 남은 지속시간
    protected int currentTimes;
    //현재 남은 발동 횟수
    protected Existence owner;
    public Existence Owner => owner;
    //효과의 주인. 누가 효과를 사용했는가? (플레이어, 함정, 패널)
    protected bool isDead = false;
    public bool IsDead => isDead;
    //효과가 죽었는지 확인

    protected bool isUpdateComplete;
    public bool IsUpdateComplete
    {
        get => isUpdateComplete;
        set => isUpdateComplete = value;
    }

    //------------------사용되면 변하는 값----------------------//


    protected virtual bool DoWhenDie() { return default; }
    //효과가 사망할 때 해야할 일
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
    //효과의 사용 : 0일땐 사망, 1일땐 조건 미달, 2일땐 효과내용 성공
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
    //효과의 사망 조건 확인 및 사망 설정(사망 설정 시점 = 사망을 완료시킨 것(없는 것 취급. 데이터는 남아도, 로직상 죽은놈임. 이 것에 대한 처리는 없는 것마냥 처리해야함)). 사망할 때 해야할 일을 수행한다.
    protected virtual bool CheckUsingCondition() { return default; }
    //효과가 발동되기 위한 조건 확인
    protected virtual bool RunEffect(Timing timing)
    {
        Debug.Log($"효과발동 : {description}");
        return default;
    }
    //효과의 내용

    //------------------사용되면 변하는 값을, 변하지 않는 고유한 값으로 초기화----------------------//
    public void Initialize()
    {
        isUseOneCycle = false;
        currentDuration = duration;
        currentTimes = times;
        owner = null;
        isDead = false;
    }
    // 우리는 효과에 사용되면 변하지 않는 값과, 함수만 재 정의한 후에, 효과를 등록할 시기에 초기화 시켜주면 됨.
    // 모든 효과는 등록되기전에 초기화해야한다.

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
