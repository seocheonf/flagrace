using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Panel_Effect_ExtraMove : Effect
{
    private int moveCount;
    private Player target;

    public Panel_Effect_ExtraMove(int moveCount)
    {
        this.moveCount = moveCount;
        priority = 2;
        duration = -1;
        times = -1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"�ش� �г� ������ {moveCount}��ŭ �̵��մϴ�!";        
    }
    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        if (owner == null) return false;
        Panel panel = owner as Panel;
        if (panel == null) return false;
        if (panel.Existences.Count == 0) return false;

        for (int i = 0; i < panel.Existences.Count; i++)
        {
            target = panel.Existences[i] as Player;
            if (target == null) return false;
            if (target.GetCurrentState(State.MOVE) == true) break;
        }

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;

        return true;
    }
    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
        EffectMove(target, moveCount, owner, MovePath.RUN);


        return true;
    }   
}

//public class Panel_Effect_CheckPoint : Effect
//{
//    private Player target;
//    private bool isPassByCheckPoiot;

//    public Panel_Effect_CheckPoint()
//    {
//        priority = 0;
//        duration = -1;
//        times = -1;

//        timing.Add(Timing.MOVE_ONESTEP_AFTER);

//        description = $"�ش� �г��� �������� �¸��� ����� �ϳ� ȹ���մϴ�.";
//    }

//    protected override bool CheckUsingCondition()
//    {
//        if (owner == null) return false;

//        Panel panel = owner as Panel;
//        if (panel == null) return false;
//        if (panel.Existences.Count == 0) return false;

//        target = panel.Existences.Find
//            (target => target.ExistenceType == ExistenceType.TurnPlayer || target.ExistenceType == ExistenceType.OrtherPlayer) as Player;

//        if (target == null) return false;
//        if (target.GetCurrentState(State.MOVE) == false) return false;

//        return true;
//    }

//    protected override bool RunEffect(Timing timing)
//    {
//        if (owner == null)  return false;
//        if (target == null) return false;
//        if (target.GetCurrentState(State.MOVE) == false) return false;

//        switch (timing)
//        {
//            case Timing.MOVE_ONESTEP_AFTER:

//                break;

//            case Timing.CARD_MOVE_AFTER:
//            case Timing.DICE_MOVE_AFTER:
//            case Timing.EFFECT_MOVE_AFTER:

//                break;
//        }

//        return true;
//    }
//}

//public class Panel_Effect : Effect
//{
//    public Panel_Effect()
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

public class Panel_Effect_ExtraTeleport : Effect
{
    private Player target;
    public Panel_Effect_ExtraTeleport()
    {
        priority = 2;
        duration = -1;
        times = -1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"�ش� �г� ������ ������ ��ġ�� �ڷ���Ʈ�մϴ�!";
    }


    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        if (owner == null) return false;
        Panel panel = owner as Panel;
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

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        int randomIndex = Random.Range(0, board.Length);

        EffectMove(target, randomIndex - target.GetPanelNumber(), owner, MovePath.TELEPORT);

        return true;
    }
}
public class Panel_Effect_Stun : Effect
{
    private Player target;
    public Panel_Effect_Stun()
    {
        priority = 0;
        duration = -1;
        times = -1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"�ش� �г� ������ ���Ͽ� �ɸ��ϴ�!(�̵� 1ȸ �Ұ�)";
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        if (owner == null) return false;
        Panel panel = owner as Panel;
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
        if (target.GetCurrentState(State.STUN)) return false;

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        target.SetCurrentState(State.STUN, true);

        return true;
    }
}

public class Panel_Effect_ExchangePanel : Effect
{
    private Player target;
    public Panel_Effect_ExchangePanel()
    {
        priority = 0;
        duration = -1;
        times = -1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"�ش� �г� ������ �����ϰ� 2�� �г��� ��ġ�� �ٲߴϴ�!";
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        if (owner == null) return false;
        Panel panel = owner as Panel;
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

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        int firstIndex = Random.Range(0, board.Length);
        int secondIndex;
        do
        {
            secondIndex = Random.Range(0, board.Length);
        }
        while (firstIndex == secondIndex);

        GameManager.GameManagerInstance.BoardManagerInstance.ExchangePanel(firstIndex, secondIndex);

        return true;
    }
}
// �ӽ�(�������ѵ� �⺻������ �ٲ�ߵǼ�..)
public class Panel_Effect_BoardRandomize : Effect
{
    private Player target;
    public Panel_Effect_BoardRandomize()
    {
        priority = 0;
        duration = -1;
        times = -1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"�ش� �г� ������ ��� �г��� ��ġ�� �����ϴ�!";
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        if (owner == null) return false;
        Panel panel = owner as Panel;
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

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        int boardLength = GameManager.GameManagerInstance.BoardManagerInstance.Board.Length;

        Vector2Int[] indexs = new Vector2Int[20];
        for (int i = 0; i < indexs.Length; i++)
        {
            int firstIndex = Random.Range(0, boardLength);
            int secondIndex;
            do
            {
                secondIndex = Random.Range(0, boardLength);
            }
            while (firstIndex == secondIndex);

            indexs[i] = new Vector2Int(firstIndex, secondIndex);
        }

        GameManager.GameManagerInstance.BoardManagerInstance.ExchangePanels(indexs);

        return true;
    }
}
public class Panel_Effect_GenerateTrap : Effect
{
    private Player target;
    public Panel_Effect_GenerateTrap()
    {
        priority = 1;
        duration = -1;
        times = -1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"�ش� �г� ������ �ٷε� �гο� ������ ��ġ�մϴ�!";
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        if (owner == null) return false;
        Panel panel = owner as Panel;
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


        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        Panel panel = board[(owner.GetPanelNumber() - 1 + board.Length) % board.Length];
        if (panel == null) return false;

        Existence trap = panel.Existences.Find(target => target.ExistenceType == ExistenceType.TRAP);

        if (trap != null)
        {
            GameManager.GameManagerInstance.EffectManagerInstance.RemoveEffect(trap.GetEffect());
            panel.RemoveExistence(trap);
            UIManager.UIManagerInstance.SetAnimationRemoveTrap((Trap)trap);
        }

        Effect randomTrapEffect = null;
        float trapEffectRate = Random.value;

        if (trapEffectRate < 0.4f)
        {
            randomTrapEffect = new Trap_Effect_Debuff();
        }
        else if (trapEffectRate < 0.8f)
        {
            randomTrapEffect = new Trap_Effect_TeleportFrontOfCheckPoint();
        }
        else
        {
            randomTrapEffect = new Trap_Effect_Barricade();
        }

        Trap temp = new Trap(panel.GetPanelNumber(), randomTrapEffect);
        //Trap temp = new Trap(panel.GetPanelNumber(), new Trap_Effect_Debuff());
        panel.AddExistence(temp);
        UIManager.UIManagerInstance.SetAnimationGenerateTrap(temp);
        UIManager.UIManagerInstance.SetAnimationOpenPanel(panel.GetPanelNumber());
        Debug.Log($"[{panel.GetPanelNumber()}]�� �г� {panel.Existences.Find(target => target.ExistenceType == ExistenceType.TRAP).GetEffect()} ������ġ");

        return true;
    }
}


public class Panel_Effect_GenerateEventPanel : Effect
{
    private Player target;
    public Panel_Effect_GenerateEventPanel()
    {
        priority = 1;
        duration = -1;
        times = -1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"�ش� �г� ������ �� �г� �ϳ��� �̺�Ʈ �гη� �����մϴ�!(ȿ�� ����)";
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        if (owner == null) return false;
        Panel panel = owner as Panel;
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

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (target == null) return false;
        if (target.GetCurrentState(State.MOVE) == false) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        Panel[] emptyPanelList = System.Array.FindAll(board, target => target.GetPanelType() == PanelType.EMPTY);

        if (emptyPanelList == null || emptyPanelList.Length == 0)
            return false;

        int panelIndex = emptyPanelList[Random.Range(0, emptyPanelList.Length)].GetPanelNumber();
        int effectIndex = Random.Range(0, 10);
        switch (effectIndex)
        {
            case 0: board[panelIndex].SetPanelInfo(new Panel_Effect_ExtraMove(Random.Range(-6, 7)), PanelType.EVENT); break;
            case 1: board[panelIndex].SetPanelInfo(new Panel_Effect_ExtraTeleport(), PanelType.EVENT); break;
            case 2: board[panelIndex].SetPanelInfo(new Panel_Effect_Stun(), PanelType.EVENT); break;
            case 3: board[panelIndex].SetPanelInfo(new Panel_Effect_ExchangePanel(), PanelType.EVENT); break;
            case 4: board[panelIndex].SetPanelInfo(new Panel_Effect_BoardRandomize(), PanelType.EVENT); break;
            case 5: board[panelIndex].SetPanelInfo(new Panel_Effect_GenerateTrap(), PanelType.EVENT); break;
            //case 6: board[panelIndex].SetPanelInfo(new Panel_Effect_ExtraMove(Random.Range(-6, 7)), PanelType.EVENT); break;
            //case 7: board[panelIndex].SetPanelInfo(new Panel_Effect_ExtraMove(Random.Range(-6, 7)), PanelType.EVENT); break;
            //case 8: board[panelIndex].SetPanelInfo(new Panel_Effect_ExtraMove(Random.Range(-6, 7)), PanelType.EVENT); break;
            //case 9: board[panelIndex].SetPanelInfo() break;

            default: board[panelIndex].SetPanelInfo(new Panel_Effect_GenerateEventPanel(), PanelType.EVENT); break;
        }
        UIManager.UIManagerInstance.SetAnimationOpenPanel(panelIndex);
        Debug.Log($"{panelIndex}�� �г��� {board[panelIndex].GetEffect()}ȿ���� ����!");
        return true;
    }
}





















public class Panel_Effect : Effect
{
    public Panel_Effect()
    {
        priority = 0;
        duration = -1;
        times = -1;

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = $"";
    }

    protected override bool DoWhenDie()
    {
        return true;
    }

    protected override bool CheckUsingCondition()
    {
        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        return true;
    }
}


