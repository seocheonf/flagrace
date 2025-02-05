using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFlagUIComponent : UIComponent
{
    GoalFlagUIDetail goalFlagUIDetail;
    int flagCount;
    public GoalFlagUIComponent(GoalFlagUIDetail goalFlagUIDetail, int flagCount) : base(goalFlagUIDetail.gameObject, 0.2f)
    {
        this.goalFlagUIDetail = goalFlagUIDetail;
        this.flagCount = flagCount;
    }

    public override void StartAnimation()
    {
        goalFlagUIDetail.Initialize(flagCount);
    }

    public override bool UpdateAnimation()
    {
        return true;
    }
}
