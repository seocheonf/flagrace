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
    /// 애니메이션 중인지 결과 값 반환
    /// </summary>
    /// <returns> 애니메이션 진행중이면 true, 애니메이션 진행중이 아니라면 false 반환 </returns>
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
    /// 유니티의 애니메이션이 종료되면 EndAnimation을 실행 시켜주도록 event를 등록하였음.
    /// </summary>
    void EndAnimation()
    {
        isAnimation = false;
    }


}
