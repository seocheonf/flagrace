using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerateUIComponent : UIComponent
{
    Transform targetTransform;
    public TrapGenerateUIComponent(TrapUIDetail trapUIDetail, Transform targetTransform, float duration) : base(trapUIDetail.gameObject, duration)
    {
        this.targetTransform = targetTransform;
    }

    public override void StartAnimation()
    {
        //targetGameObject.gameObject.SetActive(true);
        targetGameObject.transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + 3, targetTransform.position.z);
    }

    public override bool UpdateAnimation()
    {
        targetGameObject.transform.position = Vector3.Lerp(targetGameObject.transform.position, targetTransform.position, 3 * Time.deltaTime / duration);
        return true;
    }

    public override void EndAnimation()
    {
        targetGameObject.transform.position = targetTransform.position;
    }
}
