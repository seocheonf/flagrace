using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveRunUIComponent : UIComponent
{
    PlayerUIDetail playerUIDetail;
    AnimationCurve animationCurve;
    Transform targetTransform;
    Vector2 startPosition;

    public PlayerMoveRunUIComponent(PlayerUIDetail playerUIDetail, GameObject targetGameObject, Transform targetTransform, AnimationCurve animationCurve, float duration) : base(targetGameObject, duration)
    {
        this.playerUIDetail = playerUIDetail;
        this.targetTransform = targetTransform;
        this.animationCurve = animationCurve;
    }

    public override void StartAnimation()
    {
        startPosition = targetGameObject.transform.position;
        playerUIDetail.StartRunAnimation();
    }


    public override bool UpdateAnimation()
    {
        //Debug.Log(animationCurve.Evaluate(currentTime / duration));
        targetGameObject.transform.position = Vector3.Lerp(startPosition, targetTransform.position, animationCurve.Evaluate(currentTime/duration));

        return true;
    }

    public override void EndAnimation()
    {
        targetGameObject.transform.position = targetTransform.position;
        playerUIDetail.StopRunAnimation();
    }

}
