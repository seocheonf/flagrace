using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CardsSetUIComponent : UIComponent
{
    List<RectTransform> cardsRect;
    Vector2 cardsCenterRect;
    Vector2 cardSize;
    List<Vector2> targetPosition;

    /// <summary>
    /// 카드 버튼들을 펼쳐주는 애니메이션을 수행하기 위한 객체
    /// </summary>
    /// <param name="cardsRect"> 애니메이션의 대상이 되는 카드 버튼 UI의 RectTransform </param>
    /// <param name="cardsCenterRect"> 애니메이션의 중앙 기준점이 되는 Rect 좌표 </param>
    /// <param name="cardSize"> 애니메이션의 기준점이 되는 카드 버튼 UI의 너비와 높이 </param>
    /// <param name="duration"> 애니메이션 지속 시간 </param>
    public CardsSetUIComponent(List<RectTransform> cardsRect, Vector2 cardsCenterRect, Vector2 cardSize, float duration) : base(null, duration)
    {
        this.cardsRect = cardsRect;
        this.cardsCenterRect = cardsCenterRect;
        this.cardSize = cardSize;

        targetPosition = new List<Vector2>();
        SetTargetPosition();

    }

    private void SetTargetPosition()
    {
        int cardNumber = cardsRect.Count / 2;
        float targetPositionStartX;
        if(cardsRect.Count % 2 == 0) //짝수라면
        {
            targetPositionStartX = cardsCenterRect.x + -cardNumber * cardSize.x + cardSize.x / 2;
        }
        else                           //홀수라면
        {
            targetPositionStartX = cardsCenterRect.x + -cardNumber * cardSize.x;
        }

        for (int i = 0; i < cardsRect.Count; i++)
        {
            targetPosition.Add(new Vector3(targetPositionStartX, cardsCenterRect.y));
            targetPositionStartX += cardSize.x;      
        }
    }

    public override bool UpdateAnimation()
    {
        for(int i = 0; i < cardsRect.Count; i++)
        {
            cardsRect[i].anchoredPosition = Vector2.Lerp(cardsRect[i].anchoredPosition, targetPosition[i], 3 * Time.deltaTime / duration);
        }
        
        return true;
    }


    public override void EndAnimation()
    {
        for (int i = 0; i < cardsRect.Count; i++)
        {
            cardsRect[i].anchoredPosition = targetPosition[i];
        }

    }
}
