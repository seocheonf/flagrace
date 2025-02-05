using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUIDetail : MonoBehaviour
{
    public Vector3 initialPosition;

    /// <summary>
    /// 주인이 되는 Trap객체
    /// </summary>
    Trap ownerTrap;

    /// <summary>
    /// 해당 trap의 spriterenderer 컴포넌트
    /// </summary>
    public SpriteRenderer trapSpriteRenderer;

    Animator trapAnimator;

    private void Awake()
    {
        trapSpriteRenderer = GetComponent<SpriteRenderer>();
        trapAnimator = GetComponent<Animator>();
        gameObject.SetActive(false);
        initialPosition = transform.position;
    }

    public Trap GetOwnerTrap()
    {
        return ownerTrap;
    }

    public void SetOwnerTrap(Trap ownerTrap)
    {
        this.ownerTrap = ownerTrap;
    }

    public bool isAnimation = false;

    public void StartBombAnimation()
    {
        trapAnimator.SetTrigger("Off");
        isAnimation = true;
    }

    public void EndAnimation()
    {
        //transform.position = initialPosition;
        isAnimation = false;
    }

    private void OnEnable()
    {
        isAnimation = false;
    }

    private void OnDisable()
    {
        isAnimation = false;
    }

}
