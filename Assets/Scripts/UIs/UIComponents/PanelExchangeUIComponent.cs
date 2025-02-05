using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelExchangeUIComponent : UIComponent
{
    SpriteRenderer firstIndexPanelSpriteRenderer;
    List<Transform> firstIndexAllTargetSpriteRenderers;
    Vector3 firstIndexTargetPosition;
    SpriteRenderer secondIndexPanelSpriteRenderer;
    List<Transform> secondIndexAllTargetSpriteRenderers;
    Vector3 secondIndexTargetPosition;

    public PanelExchangeUIComponent(SpriteRenderer firstIndexPanelSpriteRenderer, List<Transform> firstIndexAllTargetSpriteRenderers, SpriteRenderer secondIndexPanelSpriteRenderer, List<Transform> secondIndexAllTargetSpriteRenderers, float duration) : base(null, duration)
    {
        this.firstIndexPanelSpriteRenderer = firstIndexPanelSpriteRenderer;
        this.firstIndexAllTargetSpriteRenderers = firstIndexAllTargetSpriteRenderers;

        this.secondIndexPanelSpriteRenderer = secondIndexPanelSpriteRenderer;
        this.secondIndexAllTargetSpriteRenderers = secondIndexAllTargetSpriteRenderers;

    }

    public override void StartAnimation()
    {
        firstIndexTargetPosition = firstIndexPanelSpriteRenderer.transform.position;
        secondIndexTargetPosition = secondIndexPanelSpriteRenderer.transform.position;
    }

    public override bool UpdateAnimation()
    {
        foreach(Transform eachTransform in firstIndexAllTargetSpriteRenderers)
        {
            eachTransform.position = Vector3.Lerp(firstIndexTargetPosition, secondIndexTargetPosition, currentTime / duration);
        }

        foreach (Transform eachTransform in secondIndexAllTargetSpriteRenderers)
        {
            eachTransform.position = Vector3.Lerp(secondIndexTargetPosition, firstIndexTargetPosition, currentTime / duration);
        }

        return true;
    }

    public override void EndAnimation()
    {
        foreach (Transform eachTransform in firstIndexAllTargetSpriteRenderers)
        {
            eachTransform.position = secondIndexTargetPosition;
        }

        foreach (Transform eachTransform in secondIndexAllTargetSpriteRenderers)
        {
            eachTransform.position = firstIndexTargetPosition;
        }


        //패널만 원상복구

        firstIndexPanelSpriteRenderer.transform.position = firstIndexTargetPosition;
        secondIndexPanelSpriteRenderer.transform.position = secondIndexTargetPosition;

        //스프라이트 이미지 교체
        Sprite temptSprite = firstIndexPanelSpriteRenderer.sprite;
        Color temptColor = firstIndexPanelSpriteRenderer.color;

        firstIndexPanelSpriteRenderer.sprite = secondIndexPanelSpriteRenderer.sprite;
        firstIndexPanelSpriteRenderer.color = secondIndexPanelSpriteRenderer.color;

        secondIndexPanelSpriteRenderer.sprite = temptSprite;
        secondIndexPanelSpriteRenderer.color = temptColor;

    }
}

/*
firstIndexPanelTransform.position = firstIndexTargetPosition;
secondIndexPanelTransform.position = secondIndexTargetPosition;

SpriteRenderer firstIndexPanelSpriteRenderer;
SpriteRenderer secondIndexPanelSpriteRenderer;
firstIndexPanelSpriteRenderer = firstIndexPanelTransform.GetComponent<SpriteRenderer>();
secondIndexPanelSpriteRenderer = secondIndexPanelTransform.GetComponent<SpriteRenderer>();

SpriteRenderer temptRenderer = firstIndexPanelSpriteRenderer;
firstIndexPanelSpriteRenderer = secondIndexPanelSpriteRenderer;
secondIndexPanelSpriteRenderer = temptRenderer;
*/