using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Panel : Existence
{
    public Panel(int panelNumber, PanelType panelType, Effect panelEffect = null)
    {
        this.existenceType = ExistenceType.PANEL;
        this.panelNumber = panelNumber;
        this.panelType = panelType;
        this.isOpen = false;
        this.existences = new List<Existence>();

        //애초에 패널이 효과를 가지고 있는 경우를 위한 매개변수 추가와 작업 추가
        this.effect = panelEffect;
        if(this.effect != null)
        {
            GameManager.GameManagerInstance.EffectManagerInstance.RegisterEffect(effect, this);
        }

    }

    protected PanelType panelType;
    public PanelType GetPanelType()
    {
        return panelType;
    }
    // 패널의 타입

    //임시로 위치 확인 위해 public으로 뚫어둠. 원래는 protected
    protected List<Existence> existences;
    public List<Existence> Existences => existences;
    // 현재 패널에 위치해있는 함정(단일)과 캐릭터(복수)

    protected bool isPassable = true;
    public bool IsPassable
    {
        get
        {
            if (isPassable)
            {
                return true;
            }
            else
            {
                isPassable = true;
                return false;
            }
            
        }
        set
        {
            isPassable = value;
        }
    }
    // 현재 패널을 지나갈 수 있는지?

    protected bool isOpen;
    public bool IsOpen => isOpen;
    // 공개 패널인지 비공개 패널인지?


    public bool SetPanelInfo(bool isPassable)
    {
        this.isPassable = isPassable;
        return true;
    }

    public bool SetPanelInfo(bool isPassable, bool isOpen)
    {
        this.isPassable = isPassable;
        this.isOpen = isOpen;
        return true;
    }    

    public bool SetPanelInfo(Effect effect, PanelType panelType)
    {
        if(this.panelType == PanelType.CHECKPOINT) return false;
        //if(effect == null)                         return false;

        if(this.effect != null)
        {
            // 기존 패널효과 지우기
            GameManager.GameManagerInstance.EffectManagerInstance.RemoveEffect(this.effect);
        }

        this.effect = effect;

        //효과를 비울 경우, 효과를 null로 해야하기에(빈 패널 만들 때) 밑의 처리가 없을 것이고, 효과가 있을 경우 효과를 등록해야 함.
        if (effect != null)
        {
            this.effect.SetOwner(this);
            GameManager.GameManagerInstance.EffectManagerInstance.RegisterEffect(effect, effect.Owner);
        }


        this.panelType = panelType;
        UIManager.UIManagerInstance.SetAnimationSetPanelType(panelNumber);

        //테스트
        //isOpen = true;

        return true;
    }

    public bool SetPanelInfo(Effect effect, PanelType panelType, bool isPassable, bool isOpen)
    {
        if (this.panelType == PanelType.CHECKPOINT) return false;
        //if (effect == null)                         return false;

        if (this.effect != null)
        {
            // 기존 패널효과 지우기
            GameManager.GameManagerInstance.EffectManagerInstance.RemoveEffect(this.effect);
        }

        this.effect = effect;

        if (effect != null)
        {
            this.effect.SetOwner(this);
            GameManager.GameManagerInstance.EffectManagerInstance.RegisterEffect(effect, effect.Owner);
        }

        this.panelType = panelType;
        this.isPassable = isPassable;
        this.isOpen = isOpen;
        UIManager.UIManagerInstance.SetAnimationSetPanelType(panelNumber);
        return true;
    }

    // 패널의 정보를 바꾸는 함수. 패널의 타입이 체크포인트라면, 내부요소를 함부로 바꿀 수 없음
    // 패널의 위치가 바뀌는 거는 패널이 할일이 아님. 패널의 전체를 아는 보드가 해야할 일
    // 일부만 바꾸고 싶을 때 오버로드를 사용해보자. 혹시 다른 케이스가 나오면은, 그 때가서 논의할 것

    public void AddExistence(Existence existence)
    {
        //테스트
        //isOpen = true;

        existences.Add(existence);
    }

    public bool RemoveExistence(Existence existence)
    { 
        return existences.Remove(existence); 
    }
    
    //existence마다 setpanelNumber에 해야하는 작업이 다르기 때문
    public override void SetPanelNumber(int panelNumber)
    {
        this.panelNumber = panelNumber;

        foreach(Existence existence in existences)
        {
            //만약 공중 함정이면?
            //existence를 패널에서 빼버리고 다 넣던가..
            //고민중
            existence.SetPanelNumber(panelNumber);
        }
    }
}
