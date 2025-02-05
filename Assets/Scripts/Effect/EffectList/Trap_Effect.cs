using UnityEngine;

//public class Trap_Effect : Effect
//{
//    private Player target;
//    public Trap_Effect()
//    {
//        priority = 0;
//        duration = -1;
//        times = -1;

//        timing.Add(Timing.CARD_MOVE_AFTER);
//        timing.Add(Timing.DICE_MOVE_AFTER);
//        timing.Add(Timing.EFFECT_MOVE_AFTER);

//        description = $"";
//    }

//    protected override bool DoWhenDie()
//    {
//        return true;
//    }

//    protected override bool CheckUsingCondition()
//    {
//        return true;
//    }

//    protected override bool RunEffect(Timing timing)
//    {
//        return true;
//    }
//}

public class Trap_Effect_Barricade : Effect
{
    private Player target;
    private bool isBlockTarget = false;
    private bool isActive = false;
    public Trap_Effect_Barricade()
    {
        priority = 1;
        duration = -1;
        times = 1;

        //timing.Add(Timing.CARD_MOVE_BEFORE);
        //timing.Add(Timing.DICE_MOVE_BEFORE);
        //timing.Add(Timing.EFFECT_MOVE_BEFORE);
        //timing.Add(Timing.MOVE_ONESTEP_BEFORE);
        timing.Add(Timing.MOVE_JUST_BEFORE);

        description = $"바리게이트에 막혀 이동을 멈춥니다!";
    }

    protected override bool DoWhenDie()
    {
        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        Panel panel = board[(owner.GetPanelNumber())];
        panel.RemoveExistence(owner);


        return true;
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        if (currentTimes == 0)
            return false;

        if (isActive)
        {
            isActive = false;
            return false;
        }
        if (owner == null) return false;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;

        Panel panelBefore = board[(owner.GetPanelNumber() - 1 + board.Length) % board.Length];
        Panel panelAfter = board[(owner.GetPanelNumber() + 1 + board.Length) % board.Length];

        if (panelBefore == null && panelAfter == null) return false;
        if (panelBefore.Existences.Count == 0 && panelAfter.Existences.Count == 0) return false;

        //--수정 시도 본--//
        target = null;
        {
            for (int i = 0; i < panelBefore.Existences.Count; i++)
            {
                Player temptTarget = panelBefore.Existences[i] as Player;
                if (temptTarget == null) continue;
                if (temptTarget.GetCurrentState(State.JUST_MOVE) == true && temptTarget.playerMoveDirection == 1)
                {
                    target = temptTarget;
                    break;
                }
            }
            for (int i = 0; i < panelAfter.Existences.Count; i++)
            {
                Player temptTarget = panelAfter.Existences[i] as Player;
                if (temptTarget == null) continue;
                if (temptTarget.GetCurrentState(State.JUST_MOVE) == true && temptTarget.playerMoveDirection == -1)
                {
                    target = temptTarget;
                    break;
                }
            }

            if (target == null) return false;
            if (target.GetCurrentState(State.JUST_MOVE) == false) return false;
            if (target.GetCurrentState(State.TRAP_PROTECTED))
            {
                currentTimes--;
                //isUseOneCycle = true;
                if(currentTimes == 0)
                    UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)owner);
                return false;
            }
        }
        //--수정 시도 본--//

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        //isUseOneCycle = true;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        Panel panel = board[(owner.GetPanelNumber())];

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
        panel.SetPanelInfo(false);
        isBlockTarget = true;
        isActive = true;
        Debug.Log("바리케이드");
        currentTimes--;

        if (currentTimes == 0)
            UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)owner);

        return true;
    }
}

public class Trap_Effect_TeleportFrontOfCheckPoint : Effect
{
    private Player target;
    //private bool isActive;
    public Trap_Effect_TeleportFrontOfCheckPoint()
    {
        priority = 2;
        duration = -1;
        times = 1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"체크포인트 앞 패널로 텔레포트 됩니다!";
    }
    protected override bool DoWhenDie()
    {
        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        Panel panel = board[(owner.GetPanelNumber())];
                
        panel.RemoveExistence(owner);

        return true;
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        //if (isActive == false)
        //{
        //    isActive = true;
        //    return false;
        //}
        if (isUseOneCycle) return false;

        if (owner == null) return false;
        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        Panel panel = board[owner.GetPanelNumber()];
        if (panel == null) return false;
        if (panel.Existences.Count == 0) return false;

        for (int i = 0; i < panel.Existences.Count; i++)
        {
            target = panel.Existences[i] as Player;
            if (target == null) continue;
            if (target.GetCurrentState(State.MOVE) == true) break;
        }

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;
        if (target.GetCurrentState(State.TRAP_PROTECTED))
        {
            currentTimes--;
            if (currentTimes == 0)
                UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)owner);
            return false;
        }

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;
        currentTimes--;

        if (target == null) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        int checkPointPanelIndex = System.Array.FindIndex(board, target => target.GetPanelType() == PanelType.CHECKPOINT);

        if (checkPointPanelIndex == -1) return false;

        EffectMove(target, (checkPointPanelIndex - target.GetPanelNumber() + 1), owner, MovePath.TELEPORT);

        UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)owner);
        Panel panel = board[(owner.GetPanelNumber())];
        //panel.RemoveExistence(owner);

        return true;
    }
}

public class Trap_Effect_Debuff : Effect
{
    private Player target;
    //private bool isActive;
    public Trap_Effect_Debuff()
    {
        priority = 0;
        duration = -1;
        times = 1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        timing.Add(Timing.TURN_END);

        description = $"디버프에 걸립니다!(슬로우, 스턴)";
    }

    protected override bool DoWhenDie()
    {
        if (target == null) return false;

        if (target.GetCurrentState(State.SLOW))
        {
            target.SetCurrentState(State.SLOW, false);
            target.SetCurrentState(State.NORMAL, true);
            target.MoveRate = 1f;
        }

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        Panel panel = board[(owner.GetPanelNumber())];

        panel.RemoveExistence(owner);

        return true;
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        //if (isActive == false)
        //{
        //    isActive = true;
        //    return false;
        //}
        if (isUseOneCycle) return false;

        if (owner == null) return false;

        if (target == null)
        {
            Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
            Panel panel = board[owner.GetPanelNumber()];
            if (panel == null) return false;
            if (panel.Existences.Count == 0) return false;

            for (int i = 0; i < panel.Existences.Count; i++)
            {
                target = panel.Existences[i] as Player;
                if (target == null) continue;
                if (target.GetCurrentState(State.MOVE) == true) break;
            }

            if (target == null) return false;
            if (target.GetCurrentState(State.MOVE) == false) return false;
        }
        
        if (target.GetCurrentState(State.TRAP_PROTECTED))
        {
            currentTimes--;
            if (currentTimes == 0)
                UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)owner);
            return false;
        }

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (target == null) return false;

        switch (timing)
        {
            case Timing.CARD_MOVE_AFTER:
            case Timing.DICE_MOVE_AFTER:
            case Timing.EFFECT_MOVE_AFTER:
                Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
                Panel panel = board[owner.GetPanelNumber()];
                float debuffRate = Random.value;
                if (debuffRate < 0.5)
                {
                    description = $"함정발동! 스턴에 걸립니다!";
                    UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
                    UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)owner);
                    target.SetCurrentState(State.STUN, true);
                    currentTimes--;
                }
                else
                {
                    if (target.GetCurrentState(State.SLOW) == false)
                    {
                        description = $"함정발동! 슬로우에 걸립니다!";
                        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
                        UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)owner);
                        target.SetCurrentState(State.NORMAL, false);
                        target.SetCurrentState(State.SLOW, true);
                        target.MoveRate = 0.501f;
                    }
                }
                //panel.RemoveExistence(owner);
                //if (target.GetCurrentState(State.SLOW) == false)
                //{
                //    description = $"함정발동! 슬로우에 걸립니다!";
                //    UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
                    
                //    //UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)owner);
                //    target.SetCurrentState(State.NORMAL, false);
                //    target.SetCurrentState(State.SLOW, true);
                //    target.MoveRate = 0.501f;

                //    Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
                //    Panel panel = board[(owner.GetPanelNumber())];

                //    panel.RemoveExistence(owner);
                //}
                break;
            case Timing.TURN_END:
                currentTimes--;
                break;
        }
        return true;
    }
}
