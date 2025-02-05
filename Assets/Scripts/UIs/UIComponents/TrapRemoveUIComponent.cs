using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapRemoveUIComponent : UIComponent
{
    TrapUIDetail trapUIDetail;
    Color trapColor;
    public TrapRemoveUIComponent(TrapUIDetail trapUIDetail) : base(trapUIDetail.gameObject, 3f)
    {
        this.trapUIDetail = trapUIDetail;
        trapColor = trapUIDetail.trapSpriteRenderer.color;
    }

    public override void StartAnimation()
    {
        //trapUIDetail.gameObject.SetActive(true);
        trapUIDetail.StartBombAnimation();
    }

    public override bool UpdateAnimation()
    {
        return trapUIDetail.isAnimation;
    }

    public override void EndAnimation()
    {
        UIManager.UIManagerInstance.DisuseTrapUIDetail(trapUIDetail.GetOwnerTrap());
    }
}
