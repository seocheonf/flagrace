using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceUIDetail : MonoBehaviour
{
    Animator diceAnimator;

    private void Awake()
    {
        diceAnimator = GetComponent<Animator>();
    }


    bool isAnimation = false;

    /// <summary>
    /// �ִϸ��̼� ������ ��� �� ��ȯ
    /// </summary>
    /// <returns> �ִϸ��̼� �������̸� true, �ִϸ��̼� �������� �ƴ϶�� false ��ȯ </returns>
    public bool GetIsAnimation()
    {
        return isAnimation;
    }

    public void SetOnDiceAnimation(int diceNumber)
    {
        diceAnimator.SetTrigger("Roll");
        diceAnimator.SetInteger("Result", diceNumber);
        isAnimation = true;
    }

    /// <summary>
    /// ����Ƽ�� �ִϸ��̼��� ����Ǹ� EndAnimation�� ���� �����ֵ��� event�� ����Ͽ���.
    /// </summary>
    void EndAnimation()
    {
        isAnimation = false;
    }


}
