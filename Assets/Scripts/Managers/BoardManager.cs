using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    //임시로 public 뚫어둠. protected였음.
    protected Panel[] board;
    public Panel[] Board => board;

    protected PanelInfo panelInfo;



    protected int checkPointPanelCount = 1;
    protected int eventPanelCount = 9;
    protected int totalPanelCount = 20;

    //PanelInfo를 기반으로 만들어야 하지만,
    //일단은 깡으로 만듬.
    public BoardManager()
    {
        GenerateBoard();
        InitialRandomize();
        SetInitialPanelToCheckPointPanel();
    }

    /// <summary>
    /// 처음 패널을 체크포인트 패널로 교체하는 함수. 이 함수는 게임 시작시 보드매니저 생성자의 마지막에 한번만 불러오면 된다.
    /// </summary>
    private void SetInitialPanelToCheckPointPanel()
    {
        int checkpointPanelIndex = -1;
        for(int i = 0; i<board.Length; i++)
        {
            if (board[i].GetPanelType() == PanelType.CHECKPOINT)
            {
                checkpointPanelIndex = i;
                break;
            }
        }

        if(checkpointPanelIndex != -1)
        {
            //만약에 플레이어가 이상한데에서 로직상 시작한다면
            //이거 위에 올라가있는 플레이어도 같이 간거임.
            //지금은 캐릭터가 보드위에 올라가기전이라 괜찮지만
            //혹시 문제가 생길 때를 대비해서 가능성을 적어둔다.
            ExchangePanelInner(0, checkpointPanelIndex);
        }
    }

    void GenerateBoard()
    {

        //6 by 6 = 20
        board = new Panel[totalPanelCount];

        for (int i = 0; i < totalPanelCount; i++)
        {
            if (i < checkPointPanelCount)
            {
                board[i] = new Panel(i, PanelType.CHECKPOINT);
            }
            else if (i < checkPointPanelCount + eventPanelCount)
            {
                Effect randomPanelEffect = null;
                int effectIndex = Random.Range(0, 7);
                switch (effectIndex)
                {
                    case 0: randomPanelEffect = new Panel_Effect_ExtraMove(Random.Range(-6, 7)); break;
                    case 1: randomPanelEffect = new Panel_Effect_ExtraTeleport(); break;
                    case 2: randomPanelEffect = new Panel_Effect_Stun(); break;
                    case 3: randomPanelEffect = new Panel_Effect_ExchangePanel(); break;
                    case 4: randomPanelEffect = new Panel_Effect_BoardRandomize(); break;
                    case 5: randomPanelEffect = new Panel_Effect_GenerateTrap(); break;
                    case 6: randomPanelEffect = new Panel_Effect_GenerateEventPanel(); break;

                    default: randomPanelEffect = new Panel_Effect_GenerateEventPanel(); break;
                }
                board[i] = new Panel(i, PanelType.EVENT, randomPanelEffect);
            }
            else
            {
                board[i] = new Panel(i, PanelType.EMPTY);
            }

            /*
            if (i < checkPointPanelCount)
            {
                board[i] = new Panel(i, PanelType.CHECKPOINT);
            }
            else if (i == 5)
            {
                board[i] = new Panel(i, PanelType.EVENT, new Panel_Effect_GenerateTrap());
            }
            else
            {
                board[i] = new Panel(i, PanelType.EMPTY);
            }
            */
        }

        /*
        PanelType panelTypeTempt;
        for (int i = 0; i < totalPanelCount; i++)
        {
            if (i < checkPointPanelCount)
                panelTypeTempt = PanelType.CHECKPOINT;
            else if (i < checkPointPanelCount + eventPanelCount)
                panelTypeTempt = PanelType.EVENT;
            else
                panelTypeTempt = PanelType.EMPTY;

            board[i] = new Panel(i, panelTypeTempt);

        }
        */

        //알려준다.
        UIManager.UIManagerInstance.InstantiatePanelUIVisual(board);
    }

    public void InitialRandomize()
    {
        
        for(int i = 0; i<1000; i++)
        {
            int choice = Random.Range(0, board.Length);
            Panel tempt = board[0];
            board[0] = board[choice];
            board[choice] = tempt;

            //색깔이 이상한 곳에서 바뀌길래 봤더니 여기서 제대로 번호를 안바꿔 놨었음..
            board[0].SetPanelNumber(0);
            board[choice].SetPanelNumber(choice);
        }
    }

    //ExchangePanelNumber였는데, 패널 옮기는 함수라 이름을 아래로 바꿈.
    public bool ExchangePanel(int firstIndex, int secondIndex)
    {
        if (ExchangePanelInner(firstIndex, secondIndex))
        {
            UIManager.UIManagerInstance.SetAnimationExchangePanel(board[firstIndex], board[secondIndex]);
            return true;
        }
        else
            return false;
    }

    bool ExchangePanelInner(int firstIndex, int secondIndex)
    {
        if (firstIndex >= board.Length || secondIndex >= board.Length)
            return false;

        Panel tempt = board[firstIndex];
        board[firstIndex] = board[secondIndex];
        board[secondIndex] = tempt;

        //위의 효과를 완수한 후, 각 패널들에게 자신의 변경 위치를 갱신해야 함을 알려줌.
        //보드의 인덱스가 결국 패널의 번호이므로, 보드 인덱스에 있는 패널의 번호가 곧 보드의 인덱스
        //갱신하는 느낌으로 접근하면 좋음. 이 위치의 패널넘버를 맞게 갱신한다.
        board[firstIndex].SetPanelNumber(firstIndex);
        board[secondIndex].SetPanelNumber(secondIndex);

        return true;
    }

    

    /// <summary>
    /// 여러개의 패널 쌍의 위치를 교체하는 함수이다.
    /// </summary>
    /// <param name="indexPairList"> Vector2.x 는 교환하고자 하는 firstIndex, Vector2.y는 교환하고자 하는 secondIndex이다. </param>
    /// <returns></returns>
    public bool ExchangePanels(params Vector2Int[] indexPairList)
    {
        int firstIndex, secondIndex;
        int successCount = 0;
        for (int i = 0; i < indexPairList.Length; i++)
        {
            firstIndex = indexPairList[i].x;
            secondIndex = indexPairList[i].y;

            if (ExchangePanelInner(firstIndex, secondIndex))
            {
                UIManager.UIManagerInstance.SetAnimationExchangePanel(board[firstIndex], board[secondIndex], 1f / indexPairList.Length);
                successCount++;
            }
        }

        if (successCount > 0)
            return true;
        else
            return false;
    }


    public List<PanelLine> CalculatePanelLine(int panelNumber)
    {
        //보드의 길이
        int boardLength = board.Length;
        //보드의 한 변의 길이 - 1
        int boardLengthQuater = boardLength / 4;
        if (panelNumber >= boardLength)
            return null;

        List<PanelLine> panelLineList= new List<PanelLine>();

        PanelLine? panelLine = CheckPanelLine(panelNumber / (boardLengthQuater));
        if (panelLine == null)
            return panelLineList;

        panelLineList.Add((PanelLine)panelLine);

        if ((panelNumber % (boardLengthQuater)) == 0)
        {
            panelLine = CheckPanelLine((panelNumber + (boardLengthQuater)) / (boardLengthQuater));
            //3을 더함으로서(1을 뺌으로써) 모서리친구가 자신이 바라보는 반대방향 라인에도 속해있음을 추가한다.
            if (panelLine == null)
                return panelLineList;
        }

        return panelLineList;

    }

    private PanelLine? CheckPanelLine(int quater)
    {
        switch (quater)
        {
            case 0:
                return (PanelLine.DOWN);
            case 1:
                return (PanelLine.LEFT);
            case 2:
                return (PanelLine.UP);
            case 3:
                return (PanelLine.RIGHT);
        }
        return null;
    }

    public bool RemoveExistence(Existence existence)
    {
        if (existence == null)
            return false;

        foreach(Panel eachPanel in board)
        {
            if (eachPanel.RemoveExistence(existence))
                return true;
        }

        return false;
    }



    // 나중에 passable로 문제가 생기면, 이 함수 안에 passable 구분 문구와 실제 이동시키는 문구를 두 함수로 분리시키고,
    // passable 구분 문구 함수 안에 실제이동 함수를 넣고, 그냥 이동만 필요할 경우 그 안의 함수를 실행시키도록 한다.
    // 밑에 처럼 분리는 시킬 수 있으나, 패널 이동 시 함께이동하는 것에 대해서는 처리할 게 아니다.
    public bool MovePlayerOnBoard(Player player, int origin, int steps, out int destination)
    {
        bool result = false;

        player.SetCurrentState(State.JUST_MOVE, true);

        GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.MOVE_JUST_BEFORE);

        destination = (origin + steps) % board.Length;
        destination += destination < 0 ? board.Length : 0;
        if (board[destination].IsPassable)
        {
            board[origin].RemoveExistence(player);
            board[destination].AddExistence(player);


            result = true;
        }
        else
        {
            destination = origin;
        }
        player.SetCurrentState(State.JUST_MOVE, false);
        return result;
    }
    //---------

    public bool MoveExistenceOnBoard(Existence existence, int origin, int steps, out int destination)
    {
        destination = (origin + steps) % board.Length;
        destination += destination < 0 ? board.Length : 0;
        if (board[destination].IsPassable)
        {
            MoveExistenceOnBoardStatic(existence, origin, destination);
            return true;
        }
        else
        {
            destination = origin;
            return false;
        }
    }

    public void MoveExistenceOnBoardStatic(Existence existence, int origin, int destination)
    {
        board[origin].RemoveExistence(existence);
        board[destination].AddExistence(existence);
    }

    //---------

    //우리는 목표가 이동시키는게 아니다.
    //플레이어는 어짜피 패널을 따라간다.
    //패널 번호만 갱신해야 한다.
    //따라서, 이동을 시키는 위의 함수는 우리가 하고자하는 곳에 써서는 안된다.
}
