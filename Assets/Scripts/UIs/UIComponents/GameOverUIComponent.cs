using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIComponent : UIComponent
{
    GameOverUIDetail gameOverUIDetail;
    int characterIndex;
    public GameOverUIComponent(GameObject targetGameObject, GameOverUIDetail gameOverUIDetail, int characterIndex) : base(targetGameObject, 10f)
    {
        this.gameOverUIDetail = gameOverUIDetail;
        this.characterIndex = characterIndex;
    }

    public override void StartAnimation()
    {
        targetGameObject.SetActive(true);
        targetGameObject.transform.SetSiblingIndex(targetGameObject.transform.parent.childCount);
        gameOverUIDetail.StartGameOverAnimation(characterIndex);
    }

    public override bool UpdateAnimation()
    {
        return false;
    }
}
