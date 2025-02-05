using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterEffectUseUIComponent : UIComponent
{
    string effectOwner;
    string effectDescription;
    UseEffectUIDetail useEffectUIDetail;

    public RegisterEffectUseUIComponent(string effectOwner, string effectDescription, UseEffectUIDetail useEffectUIDetail, float duration) : base(null, duration)
    {
        this.effectOwner = effectOwner;
        this.effectDescription = effectDescription;
        this.useEffectUIDetail = useEffectUIDetail;
    }

    public override void StartAnimation()
    {
        useEffectUIDetail.StartUseEffectUI(effectOwner, effectDescription);
    }

    public override bool UpdateAnimation()
    {
        return true;
    }

}
