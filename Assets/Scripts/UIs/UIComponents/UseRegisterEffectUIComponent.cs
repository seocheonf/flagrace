using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRegisterEffectUIComponent : UIComponent
{
    string effectOwner;
    string effectDescription;
    UseEffectUIDetail useEffectUIDetail;

    public UseRegisterEffectUIComponent(string effectOwner, string effectDescription, UseEffectUIDetail useEffectUIDetail, float duration) : base(null, duration)
    {
        this.effectOwner = effectOwner;
        this.effectDescription = effectDescription;
        this.useEffectUIDetail = useEffectUIDetail;
    }


    public override bool UpdateAnimation()
    {
        return false;
    }

    public override void EndAnimation()
    {
        useEffectUIDetail.StartUseEffectUI(effectOwner, effectDescription);
    }
}
