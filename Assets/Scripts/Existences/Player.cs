using System.Collections.Generic;
using UnityEngine;

public class Player : Existence
{
    public Player(CharacterInfo playerCharacter, string playerName)
    {
        //이건 그냥 고유한 캐릭터 정보를 등록하는 거라 참조 값 대입.
        this.playerCharacter = playerCharacter;
        //이건 캐릭터 정보와 별개로 덱이 구성되어야 하므로(순서가 바뀌거나 덱에서 카드가 빠질 수 있음)
        //객체 별도 생성 필요. (물론, 그 List안의 각각의 Card의 내용이 깊은 복사 되는 건 아님)
        this.playerDeck = new List<Card>();
        this.playerDeck.AddRange(playerCharacter.deckInfo);
        //--
        this.playerHand = new List<Card>();
        //--
        this.effect = playerCharacter.characterEffect;
        GameManager.GameManagerInstance.EffectManagerInstance.RegisterEffect(playerCharacter.characterEffect, this);
        //--
        this.playerDice = new Dice();
        this.playerDice.SetPlayer(this);
        //--
        this.currentState = new Dictionary<State, bool>();
        for (State i = 0; i < State.LENGTH; i++)
        {
            if (i == State.NORMAL) currentState[i] = true;
            else                   currentState[i] = false;
        }
        //--
        moveRate = initialMoveRate;
        //--
        waitingAction = new Queue<Action>();
        //--
        playerBehaviourEnd = new BehaviourEnd(this);
        //--
        existenceType = ExistenceType.OTHER_PLAYER;
        //--
        this.playerName = playerName;
        //--
        this.panelNumber = 0;
        //--
        this.flagCount = -1;
        //--
    }

    public int playerMoveDirection = 1;

    public Dice GetDice()
    {
        return playerDice;
    }
    public void DoTurnStart()
    {
        existenceType = ExistenceType.TURN_PLAYER;
        isMyTurn = true;
        isBehaviourEnd = false;
        playerDice.SetIsRollDice(false);
    }
    public void DoTurnEnd()
    {
        existenceType = ExistenceType.OTHER_PLAYER;
        isMyTurn = false;
        isBehaviourEnd = true;
        playerDice.SetIsRollDice(true);
        waitingAction.Clear();
    }


    //플레이어가 가지는 캐릭터
    protected CharacterInfo playerCharacter;

    //플레이어가 가지는 카드 뭉치
    //랜덤으로 넣고 처음것을 뽑아갈지, 랜덤으로 인덱스를 뽑고 그 위치에 걸 뽑아갈지
    //(단, 전체 크기를 항상 봐야함)
    protected List<Card> playerDeck;

    // 현제 플레이어의 패.
    protected List<Card> playerHand;

    // 가지고있는 주사위
    protected Dice playerDice;

    // 현재 상태
    protected Dictionary<State, bool> currentState = new Dictionary<State, bool>();

    // 내 턴인가?
    protected bool isMyTurn;

    // 나는 몇번째 플레이어 인가?
    protected int playerNumber;

    // 현재 가지고있는 승리재화(flag)
    protected int flagCount;
    
    protected int FlagCount
    {
        get
        {
            return flagCount;
        }
        set
        {
            flagCount = value;
            UIManager.UIManagerInstance.SetAnimationPlayerFlagUI(this, flagCount);
            if (flagCount >= GameManager.GameManagerInstance.needFlagCount)
            {
                GameManager.GameManagerInstance.GameOver(this);
            }
        }
    }
    

    // 움직일 때 가중치
    protected float moveRate;
    public float MoveRate
    {
        get => moveRate;
        set => moveRate = value;
    }

    // 초기 가중치 (1)
    protected float initialMoveRate = 1.0f;

    // 대기중인 Action
    protected Queue<Action> waitingAction;

    // BehaviourEnd에 대한 Action
    protected BehaviourEnd playerBehaviourEnd;

    // 행동이 종료되었는지에 대한 변수. 초기 값은 true고, 턴이 시작될 때 false가 됨
    protected bool isBehaviourEnd = true;

    public string playerName;


    public void SetWaitingAction(Action waitingAction)
    {
        this.waitingAction.Enqueue(waitingAction);
    }

    public bool GetIsBehaviourEnd()
    {
        return isBehaviourEnd;
    }

    public Action GetBehaviourEnd()
    {
        return playerBehaviourEnd;
    }

    public void DoBehaviourEnd()
    {
        if(!playerDice.GetIsRollDice())
        {
            playerDice.DoAction();
        }

        waitingAction.Clear();
        isBehaviourEnd = true;
    }






    //행동을 선택했는지 반환하는 함수
    public bool Select(out Action action)
    {
        //대기중인 액션을 등록
        if (waitingAction.TryDequeue(out action))       //큐에서 빼면서, 대기중인 액션이 있다면, action에 넣은 채로 true 반환. 대기중인 액션을 대기열에서 빼서 내보냈으니 선택 성공.
        {
            return true;
        }
        else                                           //대기중인 액션이 없다면 false를 반환. 대기중인 액션이 없으니 선택 실패.
        {
            return false;
        }
    }

    /*
    //행동을 선택했는지 반환하는 함수
    public bool Select(out Action action)
    {
        //대기중인 액션을 등록
        action = waitingAction;
        if (waitingAction != null) //대기중인 액션이 있다면
        {
            waitingAction = null;
            return true;
        }
        else                       //대기중인 액션이 없다면
            return false;
    }
    */

    //private bool SelectCard(out Action action)
    //{

    //}

    //이동 과정 전체 함수
    public bool Move(int steps, Existence order, MovePath movePath, MoveTool moveTool)
    {
        if (steps == 0) return false;

        currentState[State.MOVE] = true;

        switch (moveTool)
        {
            case MoveTool.CARD:
                GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.CARD_MOVE_BEFORE);
                break;
            case MoveTool.DICE:
                GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.DICE_MOVE_BEFORE);
                break;
            case MoveTool.EFFECT:
                GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.EFFECT_MOVE_BEFORE);
                break;
        }

        currentState[State.MOVE] = true;
        bool result = MoveProcess(steps, order, movePath);


        UIManager.UIManagerInstance.SetAnimationOpenPanel(panelNumber);

        if (result)
        {
            switch (moveTool)
            {
                case MoveTool.CARD:
                    GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.CARD_MOVE_AFTER);
                    break;
                case MoveTool.DICE:
                    GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.DICE_MOVE_AFTER);
                    break;
                case MoveTool.EFFECT:
                    GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.EFFECT_MOVE_AFTER);
                    break;
            }
        }

        currentState[State.MOVE] = false;

        return result;
    }

    private bool MoveProcess(int steps, Existence order, MovePath movePath)
    {
        if (steps == 0) return false;
        if (order == this && currentState[State.STUN])
        {
            currentState[State.STUN] = false;
            return false;
        }

        bool result = false;
        switch (movePath)
        {
            case MovePath.RUN:
                result = MoveRun(steps, order);
                break;

            case MovePath.TELEPORT:
                result = MoveTeleport(steps, order);
                break;
        }

        return result;
    }

    //이동:달리기 과정 전체 함수(이동 수, 명령 주인)
    public bool MoveRun(int steps, Existence order)
    {
        if (order == this) steps = Mathf.RoundToInt(steps * moveRate);

        bool direction = steps > 0;
        int moveCount = Mathf.Abs(steps);

        for (int i = 0; i < moveCount; i++)
        {
            if (MoveRunOneStep(order, direction) == false)
            {
                // 한칸 이동을 시도해서 실패했을때 그게 첫 이동이라면 움직이지 않은것으로 한다.
                if (i == 0) return false;
                else break;
            }
        }
        Debug.Log($"{moveCount}칸 이동");
        Debug.Log($"현재 위치 : [{panelNumber}]번째 칸");
        return true;
    }

    /// <summary>
    /// 한칸 씩 이동:달리기할 함수
    /// </summary>
    /// <param name="order"> 이동 주체 </param>
    /// <param name="direction"> 방향으로, true면 앞으로 이동, false면 뒤로 이동 </param>
    /// <returns></returns>
    public bool MoveRunOneStep(Existence order, bool direction)
    {
        BoardManager board = GameManager.GameManagerInstance.BoardManagerInstance;
        if (board == null) return false;

        playerMoveDirection = direction ? 1 : -1;
        GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.MOVE_ONESTEP_BEFORE);
        if (board.MovePlayerOnBoard(this, panelNumber, playerMoveDirection, out int destination))
        {
            if (panelNumber != destination)
            {
                panelNumber = destination;

                UIManager.UIManagerInstance.SetAnimationPlayerMoveRun(this);

                if (direction && board.Board[panelNumber].GetPanelType() == PanelType.CHECKPOINT)
                {
                    FlagCount += 1;
                    Debug.Log($"승리의 깃발 획득!, 보유한 승리의깃발 : {flagCount}");
                }
                GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.MOVE_ONESTEP_AFTER);
                return true;
            }
            else return false;
        }
        else
        {
            GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.MOVE_ONESTEP_FAIL);
            return false;
        }
    }



    //이동:텔레포트 과정 전체 함수
    public bool MoveTeleport(int steps, Existence order)
    {
        BoardManager board = GameManager.GameManagerInstance.BoardManagerInstance;
        if (board == null) return false;

        if (board.MovePlayerOnBoard(this, panelNumber, steps, out int destination))
        {
            panelNumber = destination;

            UIManager.UIManagerInstance.SetAnimationPlayerMoveTeleport(this);

            if (board.Board[panelNumber].GetPanelType() == PanelType.CHECKPOINT)
            {
                FlagCount += 1;
            }

            return true;
        }
        else return false;
    }


    //드로우 과정의 전체
    public bool DrawTask()
    {
        ThrowAllHands();
        return DrawCard();
    }

    /// <summary>
    /// 남은 CharacterCard를 전부 버린다.
    /// </summary>
    /// <returns></returns>
    private bool ThrowAllHands()
    {
        if (playerHand == null)
        {
            playerHand = new List<Card>();
            return false;
        }
        else if (playerHand.Count != 0)
        {
            playerHand.RemoveAll(target => target == null || target.cardType == CardType.CHARACTER);
        }

        return true;
    }

    /// <summary>
    /// 플레이어 패의 특정 위치 카드를 비운다.
    /// </summary>
    /// <param name="handIndex"></param>
    /// <returns></returns>
    public bool ThrowHand(int handIndex)
    {
        if (handIndex >= playerHand.Count)
            return false;
        else
        {
            playerHand[handIndex] = null;
            //임시
            UIManager.UIManagerInstance.SetAnimationSetCards(playerHand);
            return true;
        }
    }

    //Character카드를 뽑는다.
    private bool DrawCard()
    {
        if (playerDeck == null)
        {
            playerDeck = new List<Card>();
        }

        // 캐릭터 카드 보유수는 2장!
        for (int i = 0; i < 2; i++)
        {
            if (playerDeck.Count == 0)
            {
                if (playerCharacter.deckInfo == null) return false;

                playerDeck.AddRange(playerCharacter.deckInfo);
            }
            // 덱에서 어느 카드를 뽑을지 랜덤으로 결정한다.
            int randomIndex = Random.Range(0, playerDeck.Count);

            // 결정한 그 카드를 찾는다.
            Card currentCard = playerDeck[randomIndex];

            // 뽑은 카드의 주인을 지정해줌.
            currentCard.SetPlayer(this);

            // 뽑은 카드를 플레이어의 패에 더함.
            playerHand.Add(currentCard);

            // 뽑은 카드를 덱에서 뺀다.
            playerDeck.Remove(currentCard);

            //임시
            UIManager.UIManagerInstance.SetAnimationSetCards(playerHand);

        }

        //임시 테스트용

        return true;
    }

    /// <summary>
    /// 플레이어 핸드의 인덱스를 주고, 그곳에 해당하는 카드를 반환하는 함수.
    /// </summary>
    /// <param name="handIndex"> 플레이어 핸드의 인덱스 </param>
    /// <param name="card"> 플레이어 핸드의 인덱스에 위치해 있는 카드 </param>
    /// <returns> 핸드의 인덱스로 플레이어의 핸드에 접근할 수 없을 경우 false, 그 외에 카드를 가져올 수 있는 경우 true를 반환한다. </returns>
    public bool GetCardInHand(int handIndex, out Card card)
    {
        if (handIndex >= playerHand.Count)
        {
            card = null;
            return false;
        }
        else
        {
            card = playerHand[handIndex];
            return true;
        }
    }

    public bool GetCurrentState(State keyState)
    {
        return currentState[keyState];
    }

    public void SetCurrentState(State keyState, bool value)
    {
        currentState[keyState] = value;
    }


}
