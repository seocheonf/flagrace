using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTeleportAfterUIComponent : UIComponent
{
    PlayerUIDetail playerUIDetail;

    public PlayerMoveTeleportAfterUIComponent(PlayerUIDetail playerUIDetail) : base(null, 100f)
    {
        this.playerUIDetail = playerUIDetail;
    }

    public override void StartAnimation()
    {
        playerUIDetail.StartDeadAnimation();
    }

    public override bool UpdateAnimation()
    {
        return playerUIDetail.isAnimation;
    }
}
