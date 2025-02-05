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
            ApplyPanelInfoUIDetail(UIVisualManager.backPanelColor, "�г��� �����ֽ��ϴ�.", "");
            return;
        }
        */

        if(targetVisualPanel.color == UIVisualManager.backPanelColor)
        {
            ApplyPanelInfoUIDetail(UIVisualManager.backPanelColor, "�г��� �����ֽ��ϴ�.", "���� ����");
            return;
        }

        Color panelTypeColor = UIVisualManager.backPanelColor;
        string panelDescription = "�г� ����";
        string trapDescription = "���� ����";

        switch (targetPanel.GetPanelType())
        {
            case PanelType.CHECKPOINT:
                panelTypeColor = UIVisualManager.checkPointPanelColor;
                panelDescription = "üũ ����Ʈ �г��Դϴ�. �гο� �����ϰų�, ����ĥ �� �¸��� ����� ����ϴ�!";
                break;
            case PanelType.EMPTY:
                panelTypeColor = UIVisualManager.emptyPanelColor;
                panelDescription = "�� �г��Դϴ�.";
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
