using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : Action
{
    private bool isRollDice = false;

    //�ֻ��� �̵� ������ �ֻ����� �� ������ŭ�� �迭�� ����
    protected int[] diceEyes = { 1, 2, 3, 4, 5, 6 };

    //�ֻ����� ������ �Լ�
    public int Roll()
    {
        int diceNumber = diceEyes[Random.Range(0, diceEyes.Length)];

        //������ �ֻ����� UI�� ���� �Ѱ��ִ� ����.
        UIManager.UIManagerInstance.SetDiceUI(diceNumber);
        //�ֻ����� �������� �˸��� �Լ�
        isRollDice = true;

        return diceNumber;
    }

    // �ֻ��� �� ����
    public bool SetEyeValue(int wantIndex, int wantValue)
    {
        if (wantIndex < 0 || wantIndex >= diceEyes.Length) return false;

        diceEyes[wantIndex] = wantValue;
        return true;
    }

    public override bool DoAction()
    {
        if(player == null) return false;
        if(isRollDice)     return false;

        bool result = false;

        int moveRange = Roll();
        
        if (moveRange != 0)
        {
            result = player.Move(moveRange, player, MovePath.RUN, MoveTool.DICE);
        }

        return result;
    }

    public bool GetIsRollDice()
    {
        return isRollDice;
    }

    public void SetIsRollDice(bool isRollDice)
    {
        this.isRollDice = isRollDice;
    }

}
