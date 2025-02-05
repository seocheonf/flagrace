using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    //�ӽ÷� public �վ��. protected����.
    protected Panel[] board;
    public Panel[] Board => board;

    protected PanelInfo panelInfo;



    protected int checkPointPanelCount = 1;
    protected int eventPanelCount = 9;
    protected int totalPanelCount = 20;

    //PanelInfo�� ������� ������ ������,
    //�ϴ��� ������ ����.
    public BoardManager()
    {
        GenerateBoard();
        InitialRandomize();
        SetInitialPanelToCheckPointPanel();
    }

    /// <summary>
    /// ó�� �г��� üũ����Ʈ �гη� ��ü�ϴ� �Լ�. �� �Լ��� ���� ���۽� ����Ŵ��� �������� �������� �ѹ��� �ҷ����� �ȴ�.
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
            //���࿡ �÷��̾ �̻��ѵ����� ������ �����Ѵٸ�
            //�̰� ���� �ö��ִ� �÷��̾ ���� ������.
            //������ ĳ���Ͱ� �������� �ö󰡱����̶� ��������
            //Ȥ�� ������ ���� ���� ����ؼ� ���ɼ��� ����д�.
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

        //�˷��ش�.
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

            //������ �̻��� ������ �ٲ�淡 �ô��� ���⼭ ����� ��ȣ�� �ȹٲ� ������..
            board[0].SetPanelNumber(0);
            board[choice].SetPanelNumber(choice);
        }
    }

    //ExchangePanelNumber���µ�, �г� �ű�� �Լ��� �̸��� �Ʒ��� �ٲ�.
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

        //���� ȿ���� �ϼ��� ��, �� �гε鿡�� �ڽ��� ���� ��ġ�� �����ؾ� ���� �˷���.
        //������ �ε����� �ᱹ �г��� ��ȣ�̹Ƿ�, ���� �ε����� �ִ� �г��� ��ȣ�� �� ������ �ε���
        //�����ϴ� �������� �����ϸ� ����. �� ��ġ�� �гγѹ��� �°� �����Ѵ�.
        board[firstIndex].SetPanelNumber(firstIndex);
        board[secondIndex].SetPanelNumber(secondIndex);

        return true;
    }

    

    /// <summary>
    /// �������� �г� ���� ��ġ�� ��ü�ϴ� �Լ��̴�.
    /// </summary>
    /// <param name="indexPairList"> Vector2.x �� ��ȯ�ϰ��� �ϴ� firstIndex, Vector2.y�� ��ȯ�ϰ��� �ϴ� secondIndex�̴�. </param>
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
        //������ ����
        int boardLength = board.Length;
        //������ �� ���� ���� - 1
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
            //3�� �������μ�(1�� �����ν�) �𼭸�ģ���� �ڽ��� �ٶ󺸴� �ݴ���� ���ο��� ���������� �߰��Ѵ�.
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



    // ���߿� passable�� ������ �����, �� �Լ� �ȿ� passable ���� ������ ���� �̵���Ű�� ������ �� �Լ��� �и���Ű��,
    // passable ���� ���� �Լ� �ȿ� �����̵� �Լ��� �ְ�, �׳� �̵��� �ʿ��� ��� �� ���� �Լ��� �����Ű���� �Ѵ�.
    // �ؿ� ó�� �и��� ��ų �� ������, �г� �̵� �� �Բ��̵��ϴ� �Ϳ� ���ؼ��� ó���� �� �ƴϴ�.
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

    //�츮�� ��ǥ�� �̵���Ű�°� �ƴϴ�.
    //�÷��̾�� ��¥�� �г��� ���󰣴�.
    //�г� ��ȣ�� �����ؾ� �Ѵ�.
    //����, �̵��� ��Ű�� ���� �Լ��� �츮�� �ϰ����ϴ� ���� �Ἥ�� �ȵȴ�.
}
