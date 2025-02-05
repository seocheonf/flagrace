using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagUIComponent : UIComponent
{
    PlayerFlagUIDetail playerFlagUIDetail;
    int flagCount;
    public PlayerFlagUIComponent(PlayerFlagUIDetail playerFlagUIDetail, int flagCount) : base(playerFlagUIDetail.gameObject, 0.2f)
    {
        this.playerFlagUIDetail = playerFlagUIDetail;
        this.flagCount = flagCount;
    }

    public override void StartAnimation()
    {
        playerFlagUIDetail.SetFlagCount(flagCount);
    }

    public override bool UpdateAnimation()
    {
        return true;
    }
}
