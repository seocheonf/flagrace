using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Existence
{
    protected ExistenceType existenceType;
    public ExistenceType ExistenceType => existenceType;

    protected Effect effect;
    public Effect GetEffect()
    {
        return effect;
    }
    // ���� ������ �ִ� ���� ȿ��

    protected int panelNumber;
    // ���� ��ġ�� �г� ��ȣ

    /*
    public ExistenceUI GetInfo()
    {
        ExistenceUI.panelNUmber = this.panelNumber;
        return ExistenceUI;

    }
    */

    //
    public int GetPanelNumber()
    {
        return panelNumber;
    }
    public virtual void SetPanelNumber(int panelNumber)
    {
        this.panelNumber = panelNumber;
    }

    public string GetEffectDescription()
    {
        if (effect != null)
            return effect.description;
        else
            return "";
    }

}