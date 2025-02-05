using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : Action
{
    private bool isRollDice = false;

    //주사위 이동 범위를 주사위의 면 갯수만큼의 배열로 선언
    protected int[] diceEyes = { 1, 2, 3, 4, 5, 6 };

    //주사위를 굴리는 함수
    public int Roll()
    {
        int diceNumber = diceEyes[Random.Range(0, diceEyes.Length)];

        //로직의 주사위가 UI로 값을 넘겨주는 과정.
        UIManager.UIManagerInstance.SetDiceUI(diceNumber);
        //주사위를 굴렸음을 알리는 함수
        isRollDice = true;

        return diceNumber;
    }

    // 주사위 눈 설정
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
