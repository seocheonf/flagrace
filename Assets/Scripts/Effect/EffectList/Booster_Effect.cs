using System.Collections.Generic;
using UnityEngine;
public class Booster_Effect : Effect
{
    public Booster_Effect()
    {
        priority = 1;
        //�켱 ���� (������, ��ġ��, �̵��� : 0, 1, 2)
        duration = -1; // ����
        //���� �ð�
        times = -1; // ����
        //�ߵ� Ƚ��

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = "�̵� �� �ڽ��� �� �г��� üũ����Ʈ �г��� �ƴ϶�� �߰� �̵� �̺�Ʈ �гη� �����մϴ�!(�̵� ĭ�� ����)";
    }

    protected override bool CheckUsingCondition()
    {
        if (isUseOneCycle) return false;
        if (owner == null) return false;

        // �̵� �� üũ
        Player user = owner as Player;
        if (user == null) return false;
        if (user.GetCurrentState(State.MOVE) == false) return false;

        // ��ǥ������ CheckPoint���� Ȯ��
        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        if (board[(owner.GetPanelNumber() + 1) % board.Length].GetPanelType() == PanelType.CHECKPOINT) return false;

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        int targetIndex = (owner.GetPanelNumber() + 1) % board.Length;

        Debug.Log($"{targetIndex}�� �г� ȿ������(�нú�)");

        // ������ �߰� �̵� �г� ����
        Panel_Effect_ExtraMove panelEffect = new Panel_Effect_ExtraMove(Random.Range(1, 7));



        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
        // �г����� ����
        board[targetIndex].SetPanelInfo(panelEffect, PanelType.EVENT);


        return true;
    }
}

public class Booster_Card_Effect_1 : Effect
{
    public Booster_Card_Effect_1()
    {
        priority = 1;
        //�켱 ���� (������, ��ġ��, �̵��� : 0, 1, 2)
        duration = 1;
        //���� �ð�
        times = 1;
        //�ߵ� Ƚ��

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "ī���̵� �� �ڽ��� �� �г��� üũ����Ʈ �г��� �ƴ϶�� �߰� �̵� �̺�Ʈ �гη� �����մϴ�!(�̵� ĭ�� ����)";
    }


    protected override bool CheckUsingCondition()
    {
        if (isUseOneCycle) return false;
        if (owner == null) return false;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        if (board[(owner.GetPanelNumber() + 1) % board.Length].GetPanelType() == PanelType.CHECKPOINT) return false;

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        // ���Ƚ�� üũ(0������ �������� �������� �ߵ��ϴ� ����)
        if (currentTimes > 0) currentTimes--;
        else return false;

        isUseOneCycle = true;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        int targetIndex = (owner.GetPanelNumber() + 1) % board.Length;

        Debug.Log($"{targetIndex}�� �г� ȿ������(ī��)");

        // ������ �߰� �̵� �г� ����
        Panel_Effect_ExtraMove panelEffect = new Panel_Effect_ExtraMove(Random.Range(1, 7));

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        // �г����� ����
        board[targetIndex].SetPanelInfo(panelEffect, PanelType.EVENT);


        return true;
    }
}
public class Booster_Card_Effect_2 : Effect
{
    public Booster_Card_Effect_2()
    {
        priority = 0;
        //�켱 ���� (������, ������, �ɵ��� : 0, 1, 2)
        duration = 1;
        //���� �ð�
        times = -1;
        //�ߵ� Ƚ��

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "1�� ���� ������ ������ ���� �ʽ��ϴ�.";
    }

    protected override bool DoWhenDie()
    {
        if (owner == null) return false;

        Player user = owner as Player;
        if (user == null) return false;

        user.SetCurrentState(State.TRAP_PROTECTED, false);

        return true;
    }

    protected override bool CheckUsingCondition()
    {
        if (isUseOneCycle) return false;
        if (owner == null) return false;

        Player user = owner as Player;
        if (user == null) return false;

        if (user.GetCurrentState(State.TRAP_PROTECTED)) return false;

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        if (owner == null) return false;

        Player user = owner as Player;
        if (user == null) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        user.SetCurrentState(State.TRAP_PROTECTED, true);

        return true;
    }
}
public class Booster_Card_Effect_3 : Effect
{

    public Booster_Card_Effect_3()
    {
        priority = 2;
        //�켱 ���� (������, ������, �ɵ��� : 0, 1, 2)
        duration = 1;
        //���� �ð�
        times = 1;
        //�ߵ� Ƚ��

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "���� ���ο� �ִ� �÷��̾���� ������ �ǳ����� �̵���ŵ�ϴ�. (�ڽ� ����,������ �̵�)";
    }


    protected override bool CheckUsingCondition()
    {
        if (isUseOneCycle) return false;
        if (owner == null) return false;

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        if (currentTimes > 0) currentTimes--;
        else return false;

        isUseOneCycle = true;

        if (owner == null) return false;

        Player user = owner as Player;
        if (user == null) return false;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;

        int ownerLineIndex = owner.GetPanelNumber() / (board.Length / 4);

        int lineStartPanelIndex = (board.Length / 4) * ownerLineIndex;
        int lineEndPanelIndex = (board.Length / 4) * (ownerLineIndex + 1);

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        EffectMove(user, lineEndPanelIndex - owner.GetPanelNumber(), owner, MovePath.RUN);

        List<Existence> targets = new List<Existence>();
        for (int i = lineStartPanelIndex; i < lineEndPanelIndex; i++)
        {
            targets.AddRange(board[i].Existences.FindAll(target => target.ExistenceType == ExistenceType.OTHER_PLAYER));
        }

        for (int i = 0; i < targets.Count; i++)
        {
            Player target = targets[i] as Player;
            if (target != null)
            {
                EffectMove(target, lineEndPanelIndex - target.GetPanelNumber(), owner, MovePath.RUN);
            }
        }

        return true;
    }
}
public class Booster_Card_Effect_4 : Effect
{
    private Player user;
    public Booster_Card_Effect_4()
    {
        priority = 0;
        //�켱 ���� (������, ������, �ɵ��� : 0, 1, 2)
        duration = 1;
        //���� �ð�
        times = 1;
        //�ߵ� Ƚ��

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "�ڽ��� �ֻ��� �� �ϳ��� 4~6���� �����մϴ�.";
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;

        user = owner as Player;
        if (user == null) return false;

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        if (currentTimes > 0) currentTimes--;
        else return false;
        isUseOneCycle = true;

        if (user == null) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        user.GetDice().SetEyeValue(Random.Range(0, 7), Random.Range(4, 7));

        return true;
    }
}
public class Booster_Card_Effect_5 : Effect
{
    private Existence ortherPlayer;
    private Player user;

    public Booster_Card_Effect_5()
    {
        priority = 2;
        //�켱 ���� (������, ������, �ɵ��� : 0, 1, 2)
        duration = 1;
        //���� �ð�
        times = 1;
        //�ߵ� Ƚ��

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "��������(0��° ĭ)�� �������� �� �տ� �ִ� �÷��̾��� ��ġ�� �̵��մϴ�.";
    }

    protected override bool CheckUsingCondition()
    {
        if (isDead) return false;
        if (isUseOneCycle) return false;
        if (owner == null) return false;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;

        ortherPlayer = null;

        for (int i = owner.GetPanelNumber(); i < board.Length; i++)
        {
            if (ortherPlayer == null)
            {
                ortherPlayer = board[i].Existences.Find(target => target.ExistenceType == ExistenceType.OTHER_PLAYER);
            }
            else break;
        }

        if (ortherPlayer == null) return false;

        user = owner as Player;
        if (user == null) return false;

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        if (currentTimes > 0) currentTimes--;
        else return false;
        isUseOneCycle = true;

        if (ortherPlayer == null) return false;
        if (user == null) return false;

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        EffectMove(user, ortherPlayer.GetPanelNumber() - owner.GetPanelNumber(), owner, MovePath.RUN);

        return true;
    }
}
public class Booster_Card_Effect_6 : Effect
{
    bool isActive;
    private Player user;
    public Booster_Card_Effect_6()
    {
        priority = 0;
        //�켱 ���� (������, ������, �ɵ��� : 0, 1, 2)
        duration = 3;
        //���� �ð�
        times = -1;
        //�ߵ� Ƚ��

        timing.Add(Timing.CARD_MOVE_BEFORE);        
        timing.Add(Timing.TURN_END);

        description = "1�� ���� ��� �̵��� 2��� ����˴ϴ�. ���� �ڽ� �� �̵��� ���ݸ� ����˴ϴ�.";

    }

    protected override bool DoWhenDie()
    {
        if (user == null) return false;

        user.SetCurrentState(State.SLOW, false);
        user.SetCurrentState(State.NORMAL, true);
        user.MoveRate = 1f;
        Debug.Log("�����ӵ�");
        return true;
    }

    protected override bool CheckUsingCondition()
    {
        if (owner == null) return false;
        user = owner as Player;
        if (user == null) return false;
        if (isUseOneCycle) return false;

        if (currentDuration != duration) return false;        

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        if (user == null) return false;

        isUseOneCycle = true;

        if (!isActive)
        {
            isActive = true;
            UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
        }

        switch (timing)
        {
            case Timing.CARD_MOVE_BEFORE:
                user.SetCurrentState(State.NORMAL, false);
                user.SetCurrentState(State.FAST, true);
                user.MoveRate = 2f;
                Debug.Log("������");
                break;

            case Timing.TURN_END:
                user.SetCurrentState(State.FAST, false);
                user.SetCurrentState(State.SLOW, true);
                user.MoveRate = 0.501f;
                Debug.Log("������");
                break;
        }

        return true;
    }
}
