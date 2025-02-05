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

        //���� �ð��� �� �Ǿ��ų�, updateanimation�� ���Ƿ� �����ٸ�(false)
        //�����ؾ� ��.
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

    //�ڽ��� Ȱ��ȭ�Ǵ� �ñ⿡ �ٿ�����.
    protected void AnnounceActive()
    {
        //Ȱ��ȭȭ ���ÿ� ����ð� �ʱ�ȭ.
        currentTime = 0;
        UIManager.UIManagerInstance.AddUIComponentList(this);
    }
    public virtual void StartAnimation()
    { }
    //�ִϸ��̼��� ����Ǹ� �ؾ��� ��. �Ϲ������� true�� ��ȯ�ϰ�, ������ ���ų� �׸� �־� �ϸ� false�� ��ȯ�Ѵ�.
    public abstract bool UpdateAnimation();
    //�ִϸ��̼� ���� �Ŀ� �ؾ��� ��.
    public virtual void EndAnimation()
    { }
}
