using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMoveUIComponent : UIComponent
{
    int panelCount = 20;
    Vector3 targetPosition;
    float initialX = -1.0f;
    float initialY = -1.0f;
    public PanelMoveUIComponent(Vector3 origin, int index, GameObject targetGameObject, float duration) : base(targetGameObject, duration)
    {
        initialX = origin.x + 2.5f;
        initialY = origin.y - 2.5f;


        targetPosition = new Vector3(initialX, initialY, 0);
        int lineSeperation = panelCount / 4;
        int line = index / lineSeperation;
        int semiLine = index % lineSeperation;

        switch (line)
        {
            case 0:
                targetPosition.x = targetPosition.x - 1f * semiLine;
                break;
            case 1:
                targetPosition.x = initialX - 5f;
                targetPosition.y = targetPosition.y + 1f * semiLine;
                break;
            case 2:
                targetPosition.x = initialX - 5f;
                targetPosition.y = initialY + 5f;
                targetPosition.x = targetPosition.x + 1f * semiLine;
                break;
            case 3:
                targetPosition.y = initialY + 5f;
                targetPosition.y = targetPosition.y - 1f * semiLine;
                break;
        }
    }


    public override bool UpdateAnimation()
    {
        targetGameObject.transform.position = Vector3.Lerp(targetGameObject.transform.position, targetPosition, 3 * Time.deltaTime / duration);

        return true;
    }

    public override void EndAnimation()
    {
        targetGameObject.transform.position = targetPosition;
    }
}
