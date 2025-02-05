using System.Collections.Generic;
using UnityEngine;
public class Booster_Effect : Effect
{
    public Booster_Effect()
    {
        priority = 1;
        //우선 순위 (버프형, 설치형, 이동형 : 0, 1, 2)
        duration = -1; // 무한
        //지속 시간
        times = -1; // 무한
        //발동 횟수

        timing.Add(Timing.CARD_MOVE_AFTER);
        timing.Add(Timing.DICE_MOVE_AFTER);
        timing.Add(Timing.EFFECT_MOVE_AFTER);

        description = "이동 후 자신의 앞 패널이 체크포인트 패널이 아니라면 추가 이동 이벤트 패널로 변경합니다!(이동 칸수 랜덤)";
    }

    protected override bool CheckUsingCondition()
    {
        if (isUseOneCycle) return false;
        if (owner == null) return false;

        // 이동 후 체크
        Player user = owner as Player;
        if (user == null) return false;
        if (user.GetCurrentState(State.MOVE) == false) return false;

        // 목표지점이 CheckPoint인지 확인
        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        if (board[(owner.GetPanelNumber() + 1) % board.Length].GetPanelType() == PanelType.CHECKPOINT) return false;

        return true;
    }

    protected override bool RunEffect(Timing timing)
    {
        isUseOneCycle = true;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        int targetIndex = (owner.GetPanelNumber() + 1) % board.Length;

        Debug.Log($"{targetIndex}번 패널 효과갱신(패시브)");

        // 랜덤한 추가 이동 패널 생성
        Panel_Effect_ExtraMove panelEffect = new Panel_Effect_ExtraMove(Random.Range(1, 7));



        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);
        // 패널정보 수정
        board[targetIndex].SetPanelInfo(panelEffect, PanelType.EVENT);


        return true;
    }
}

public class Booster_Card_Effect_1 : Effect
{
    public Booster_Card_Effect_1()
    {
        priority = 1;
        //우선 순위 (버프형, 설치형, 이동형 : 0, 1, 2)
        duration = 1;
        //지속 시간
        times = 1;
        //발동 횟수

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "카드이동 전 자신의 앞 패널이 체크포인트 패널이 아니라면 추가 이동 이벤트 패널로 변경합니다!(이동 칸수 랜덤)";
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
        // 사용횟수 체크(0밑으로 내려가면 무한으로 발동하니 주의)
        if (currentTimes > 0) currentTimes--;
        else return false;

        isUseOneCycle = true;

        Panel[] board = GameManager.GameManagerInstance.BoardManagerInstance.Board;
        int targetIndex = (owner.GetPanelNumber() + 1) % board.Length;

        Debug.Log($"{targetIndex}번 패널 효과갱신(카드)");

        // 랜덤한 추가 이동 패널 생성
        Panel_Effect_ExtraMove panelEffect = new Panel_Effect_ExtraMove(Random.Range(1, 7));

        UIManager.UIManagerInstance.SetAnimationUseEffectUI(this);

        // 패널정보 수정
        board[targetIndex].SetPanelInfo(panelEffect, PanelType.EVENT);


        return true;
    }
}
public class Booster_Card_Effect_2 : Effect
{
    public Booster_Card_Effect_2()
    {
        priority = 0;
        //우선 순위 (버프형, 함정형, 능동형 : 0, 1, 2)
        duration = 1;
        //지속 시간
        times = -1;
        //발동 횟수

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "1턴 동안 함정의 영향을 받지 않습니다.";
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
        //우선 순위 (버프형, 함정형, 능동형 : 0, 1, 2)
        duration = 1;
        //지속 시간
        times = 1;
        //발동 횟수

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "같은 라인에 있는 플레이어들을 라인의 맨끝으로 이동시킵니다. (자신 포함,연속적 이동)";
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
        //우선 순위 (버프형, 함정형, 능동형 : 0, 1, 2)
        duration = 1;
        //지속 시간
        times = 1;
        //발동 횟수

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "자신의 주사위 눈 하나를 4~6으로 변경합니다.";
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
        //우선 순위 (버프형, 함정형, 능동형 : 0, 1, 2)
        duration = 1;
        //지속 시간
        times = 1;
        //발동 횟수

        timing.Add(Timing.CARD_MOVE_BEFORE);

        description = "시작지점(0번째 칸)을 기준으로 내 앞에 있는 플레이어의 위치로 이동합니다.";
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
        //우선 순위 (버프형, 함정형, 능동형 : 0, 1, 2)
        duration = 3;
        //지속 시간
        times = -1;
        //발동 횟수

        timing.Add(Timing.CARD_MOVE_BEFORE);        
        timing.Add(Timing.TURN_END);

        description = "1턴 동안 모든 이동이 2배로 적용됩니다. 다음 자신 턴 이동은 절반만 적용됩니다.";

    }

    protected override bool DoWhenDie()
    {
        if (user == null) return false;

        user.SetCurrentState(State.SLOW, false);
        user.SetCurrentState(State.NORMAL, true);
        user.MoveRate = 1f;
        Debug.Log("원래속도");
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
                Debug.Log("빨라짐");
                break;

            case Timing.TURN_END:
                user.SetCurrentState(State.FAST, false);
                user.SetCurrentState(State.SLOW, true);
                user.MoveRate = 0.501f;
                Debug.Log("느려짐");
                break;
        }

        return true;
    }
}
