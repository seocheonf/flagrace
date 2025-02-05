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
    /// ī�� ��ư���� �����ִ� �ִϸ��̼��� �����ϱ� ���� ��ü
    /// </summary>
    /// <param name="cardsRect"> �ִϸ��̼��� ����� �Ǵ� ī�� ��ư UI�� RectTransform </param>
    /// <param name="cardsCenterRect"> �ִϸ��̼��� �߾� �������� �Ǵ� Rect ��ǥ </param>
    /// <param name="cardSize"> �ִϸ��̼��� �������� �Ǵ� ī�� ��ư UI�� �ʺ�� ���� </param>
    /// <param name="duration"> �ִϸ��̼� ���� �ð� </param>
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
        if(cardsRect.Count % 2 == 0) //¦�����
        {
            targetPositionStartX = cardsCenterRect.x + -cardNumber * cardSize.x + cardSize.x / 2;
        }
        else                           //Ȧ�����
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
