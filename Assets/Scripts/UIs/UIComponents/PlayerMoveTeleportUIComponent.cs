using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveTeleportUIComponent : UIComponent
{
    Transform targetTransform;


    public PlayerMoveTeleportUIComponent(GameObject targetGameObject, Transform targetTransform, float duration) : base(targetGameObject, duration)
    {
        this.targetTransform = targetTransform;
    }


    public override bool UpdateAnimation()
    {
        targetGameObject.transform.position = targetTransform.position;
        return false;
    }

}
