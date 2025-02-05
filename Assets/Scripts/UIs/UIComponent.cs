using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIComponent
{
    protected GameObject targetGameObject;

    protected float currentTime = 0;
    protected float duration = 0.5f;

    //bool start = false;

    public UIComponent(GameObject targetGameObject, float duration)
    {
        this.targetGameObject = targetGameObject;
        this.duration = duration;
    }
    public bool UpdateUI()
    {
        /*
        if (!start)
        {
            StartAnimation();
            start = true;
        }
        */

        //만약 시간이 다 되었거나, updateanimation이 임의로 끝났다면(false)
        //종료해야 함.
        if (currentTime >= duration || !UpdateAnimation())
        {
            EndAnimation();
            return false;
        }
        else
        {
            currentTime += Time.deltaTime;
            return true;
        }

    }

    //자신이 활성화되는 시기에 붙여넣자.
    protected void AnnounceActive()
    {
        //활성화화 동시에 현재시간 초기화.
        currentTime = 0;
        UIManager.UIManagerInstance.AddUIComponentList(this);
    }
    public virtual void StartAnimation()
    { }
    //애니메이션이 진행되며 해야할 일. 일반적으로 true를 반환하고, 할일이 없거나 그만 둬야 하면 false를 반환한다.
    public abstract bool UpdateAnimation();
    //애니메이션 종료 후에 해야할 일.
    public virtual void EndAnimation()
    { }
}
