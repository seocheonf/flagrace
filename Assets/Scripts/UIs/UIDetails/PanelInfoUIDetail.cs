using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelInfoUIDetail : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer panelTypeSpriteRenderer;
    [SerializeField]
    TextMeshPro panelEffectDescription;
    [SerializeField]
    TextMeshPro trapEffectDescription;

    void ApplyPanelInfoUIDetail(Color panelTypeColor, string panelEffectDescription, string trapEffectDescription)
    {
        panelTypeSpriteRenderer.color = panelTypeColor;
        this.panelEffectDescription.text = panelEffectDescription;
        this.trapEffectDescription.text = trapEffectDescription;
    }

    public void SetPanelInfoUIDetail(Panel targetPanel, SpriteRenderer targetVisualPanel)
    {
        /*
        if (!targetPanel.IsOpen)
        {
            ApplyPanelInfoUIDetail(UIVisualManager.backPanelColor, "패널이 닫혀있습니다.", "");
            return;
        }
        */

        if(targetVisualPanel.color == UIVisualManager.backPanelColor)
        {
            ApplyPanelInfoUIDetail(UIVisualManager.backPanelColor, "패널이 닫혀있습니다.", "함정 없음");
            return;
        }

        Color panelTypeColor = UIVisualManager.backPanelColor;
        string panelDescription = "패널 없음";
        string trapDescription = "함정 없음";

        switch (targetPanel.GetPanelType())
        {
            case PanelType.CHECKPOINT:
                panelTypeColor = UIVisualManager.checkPointPanelColor;
                panelDescription = "체크 포인트 패널입니다. 패널에 도착하거나, 지나칠 시 승리의 깃발을 얻습니다!";
                break;
            case PanelType.EMPTY:
                panelTypeColor = UIVisualManager.emptyPanelColor;
                panelDescription = "빈 패널입니다.";
                break;
            case PanelType.EVENT:
                panelTypeColor = UIVisualManager.eventPanelColor;
                panelDescription = targetPanel.GetEffectDescription();
                break;
        }

        foreach (Existence existence in targetPanel.Existences)
        {
            if (existence.ExistenceType == ExistenceType.TRAP)
            {
                
                if (!existence.GetEffect().CheckDeadCondition())
                    trapDescription = existence.GetEffectDescription();
                
                break;
            }
        }

        ApplyPanelInfoUIDetail(panelTypeColor, panelDescription, trapDescription);
    }

}
