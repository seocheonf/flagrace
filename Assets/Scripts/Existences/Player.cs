using System.Collections.Generic;
using UnityEngine;

public class Player : Existence
{
    public Player(CharacterInfo playerCharacter, string playerName)
    {
        //�̰� �׳� ������ ĳ���� ������ ����ϴ� �Ŷ� ���� �� ����.
        this.playerCharacter = playerCharacter;
        //�̰� ĳ���� ������ ������ ���� �����Ǿ�� �ϹǷ�(������ �ٲ�ų� ������ ī�尡 ���� �� ����)
        //��ü ���� ���� �ʿ�. (����, �� List���� ������ Card�� ������ ���� ���� �Ǵ� �� �ƴ�)
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


    //�÷��̾ ������ ĳ����
    protected CharacterInfo playerCharacter;

    //�÷��̾ ������ ī�� ��ġ
    //�������� �ְ� ó������ �̾ư���, �������� �ε����� �̰� �� ��ġ�� �� �̾ư���
    //(��, ��ü ũ�⸦ �׻� ������)
    protected List<Card> playerDeck;

    // ���� �÷��̾��� ��.
    protected List<Card> playerHand;

    // �������ִ� �ֻ���
    protected Dice playerDice;

    // ���� ����
    protected Dictionary<State, bool> currentState = new Dictionary<State, bool>();

    // �� ���ΰ�?
    protected bool isMyTurn;

    // ���� ���° �÷��̾� �ΰ�?
    protected int playerNumber;

    // ���� �������ִ� �¸���ȭ(flag)
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
    

    // ������ �� ����ġ
    protected float moveRate;
    public float MoveRate
    {
        get => moveRate;
        set => moveRate = value;
    }

    // �ʱ� ����ġ (1)
    protected float initialMoveRate = 1.0f;

    // ������� Action
    protected Queue<Action> waitingAction;

    // BehaviourEnd�� ���� Action
    protected BehaviourEnd playerBehaviourEnd;

    // �ൿ�� ����Ǿ������� ���� ����. �ʱ� ���� true��, ���� ���۵� �� false�� ��
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






    //�ൿ�� �����ߴ��� ��ȯ�ϴ� �Լ�
    public bool Select(out Action action)
    {
        //������� �׼��� ���
        if (waitingAction.TryDequeue(out action))       //ť���� ���鼭, ������� �׼��� �ִٸ�, action�� ���� ä�� true ��ȯ. ������� �׼��� ��⿭���� ���� ���������� ���� ����.
        {
            return true;
        }
        else                                           //������� �׼��� ���ٸ� false�� ��ȯ. ������� �׼��� ������ ���� ����.
        {
            return false;
        }
    }

    /*
    //�ൿ�� �����ߴ��� ��ȯ�ϴ� �Լ�
    public bool Select(out Action action)
    {
        //������� �׼��� ���
        action = waitingAction;
        if (waitingAction != null) //������� �׼��� �ִٸ�
        {
            waitingAction = null;
            return true;
        }
        else                       //������� �׼��� ���ٸ�
            return false;
    }
    */

    //private bool SelectCard(out Action action)
    //{

    //}

    //�̵� ���� ��ü �Լ�
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

    //�̵�:�޸��� ���� ��ü �Լ�(�̵� ��, ��� ����)
    public bool MoveRun(int steps, Existence order)
    {
        if (order == this) steps = Mathf.RoundToInt(steps * moveRate);

        bool direction = steps > 0;
        int moveCount = Mathf.Abs(steps);

        for (int i = 0; i < moveCount; i++)
        {
            if (MoveRunOneStep(order, direction) == false)
            {
                // ��ĭ �̵��� �õ��ؼ� ���������� �װ� ù �̵��̶�� �������� ���������� �Ѵ�.
                if (i == 0) return false;
                else break;
            }
        }
        Debug.Log($"{moveCount}ĭ �̵�");
        Debug.Log($"���� ��ġ : [{panelNumber}]��° ĭ");
        return true;
    }

    /// <summary>
    /// ��ĭ �� �̵�:�޸����� �Լ�
    /// </summary>
    /// <param name="order"> �̵� ��ü </param>
    /// <param name="direction"> ��������, true�� ������ �̵�, false�� �ڷ� �̵� </param>
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
                    Debug.Log($"�¸��� ��� ȹ��!, ������ �¸��Ǳ�� : {flagCount}");
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



    //�̵�:�ڷ���Ʈ ���� ��ü �Լ�
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


    //��ο� ������ ��ü
    public bool DrawTask()
    {
        ThrowAllHands();
        return DrawCard();
    }

    /// <summary>
    /// ���� CharacterCard�� ���� ������.
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
    /// �÷��̾� ���� Ư�� ��ġ ī�带 ����.
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
            //�ӽ�
            UIManager.UIManagerInstance.SetAnimationSetCards(playerHand);
            return true;
        }
    }

    //Characterī�带 �̴´�.
    private bool DrawCard()
    {
        if (playerDeck == null)
        {
            playerDeck = new List<Card>();
        }

        // ĳ���� ī�� �������� 2��!
        for (int i = 0; i < 2; i++)
        {
            if (playerDeck.Count == 0)
            {
                if (playerCharacter.deckInfo == null) return false;

                playerDeck.AddRange(playerCharacter.deckInfo);
            }
            // ������ ��� ī�带 ������ �������� �����Ѵ�.
            int randomIndex = Random.Range(0, playerDeck.Count);

            // ������ �� ī�带 ã�´�.
            Card currentCard = playerDeck[randomIndex];

            // ���� ī���� ������ ��������.
            currentCard.SetPlayer(this);

            // ���� ī�带 �÷��̾��� �п� ����.
            playerHand.Add(currentCard);

            // ���� ī�带 ������ ����.
            playerDeck.Remove(currentCard);

            //�ӽ�
            UIManager.UIManagerInstance.SetAnimationSetCards(playerHand);

        }

        //�ӽ� �׽�Ʈ��

        return true;
    }

    /// <summary>
    /// �÷��̾� �ڵ��� �ε����� �ְ�, �װ��� �ش��ϴ� ī�带 ��ȯ�ϴ� �Լ�.
    /// </summary>
    /// <param name="handIndex"> �÷��̾� �ڵ��� �ε��� </param>
    /// <param name="card"> �÷��̾� �ڵ��� �ε����� ��ġ�� �ִ� ī�� </param>
    /// <returns> �ڵ��� �ε����� �÷��̾��� �ڵ忡 ������ �� ���� ��� false, �� �ܿ� ī�带 ������ �� �ִ� ��� true�� ��ȯ�Ѵ�. </returns>
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
