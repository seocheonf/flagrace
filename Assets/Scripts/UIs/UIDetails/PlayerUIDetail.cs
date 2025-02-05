using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIDetail : MonoBehaviour
{
    Player player;
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;


    public void Initialize(Player player)
    {
        this.player = player;
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    public bool isAnimation = false;


    public void SetSpriteRenderer(int orderInLayer, float colorAlpha)
    {
        playerSpriteRenderer.sortingOrder = orderInLayer;
        playerSpriteRenderer.color = new Color(1, 1, 1, colorAlpha);
    }










    //- 局聪皋捞记 包访 贸府 -

    public void StartDeadAnimation()
    {
        playerAnimator.SetTrigger("DeadTrigger");
        isAnimation = true;
    }

    public void StartRunAnimation()
    {
        playerAnimator.SetBool("isRun", true);
        isAnimation = true;
    }

    public void StopRunAnimation()
    {
        playerAnimator.SetBool("isRun", false);
        isAnimation = false;
    }

    public void EndAnimation()
    {
        isAnimation = false;
    }










        /*
    private void OnEnable()
    {
        TextMeshPro tempt = GetComponentInChildren<TextMeshPro>();
        if (tempt != null)
            tempt.text = name;
    }
        */
}
