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

        //���ʿ� �г��� ȿ���� ������ �ִ� ��츦 ���� �Ű����� �߰��� �۾� �߰�
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
    // �г��� Ÿ��

    //�ӽ÷� ��ġ Ȯ�� ���� public���� �վ��. ������ protected
    protected List<Existence> existences;
    public List<Existence> Existences => existences;
    // ���� �гο� ��ġ���ִ� ����(����)�� ĳ����(����)

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
    // ���� �г��� ������ �� �ִ���?

    protected bool isOpen;
    public bool IsOpen => isOpen;
    // ���� �г����� ����� �г�����?


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
            // ���� �г�ȿ�� �����
            GameManager.GameManagerInstance.EffectManagerInstance.RemoveEffect(this.effect);
        }

        this.effect = effect;

        //ȿ���� ��� ���, ȿ���� null�� �ؾ��ϱ⿡(�� �г� ���� ��) ���� ó���� ���� ���̰�, ȿ���� ���� ��� ȿ���� ����ؾ� ��.
        if (effect != null)
        {
            this.effect.SetOwner(this);
            GameManager.GameManagerInstance.EffectManagerInstance.RegisterEffect(effect, effect.Owner);
        }


        this.panelType = panelType;
        UIManager.UIManagerInstance.SetAnimationSetPanelType(panelNumber);

        //�׽�Ʈ
        //isOpen = true;

        return true;
    }

    public bool SetPanelInfo(Effect effect, PanelType panelType, bool isPassable, bool isOpen)
    {
        if (this.panelType == PanelType.CHECKPOINT) return false;
        //if (effect == null)                         return false;

        if (this.effect != null)
        {
            // ���� �г�ȿ�� �����
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

    // �г��� ������ �ٲٴ� �Լ�. �г��� Ÿ���� üũ����Ʈ���, ���ο�Ҹ� �Ժη� �ٲ� �� ����
    // �г��� ��ġ�� �ٲ�� �Ŵ� �г��� ������ �ƴ�. �г��� ��ü�� �ƴ� ���尡 �ؾ��� ��
    // �Ϻθ� �ٲٰ� ���� �� �����ε带 ����غ���. Ȥ�� �ٸ� ���̽��� ��������, �� ������ ������ ��

    public void AddExistence(Existence existence)
    {
        //�׽�Ʈ
        //isOpen = true;

        existences.Add(existence);
    }

    public bool RemoveExistence(Existence existence)
    { 
        return existences.Remove(existence); 
    }
    
    //existence���� setpanelNumber�� �ؾ��ϴ� �۾��� �ٸ��� ����
    public override void SetPanelNumber(int panelNumber)
    {
        this.panelNumber = panelNumber;

        foreach(Existence existence in existences)
        {
            //���� ���� �����̸�?
            //existence�� �гο��� �������� �� �ִ���..
            //�����
            existence.SetPanelNumber(panelNumber);
        }
    }
}
