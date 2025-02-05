using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Existence
{
    public Trap(int panelNumber, Effect effect)
    {
        this.existenceType = ExistenceType.TRAP;
        this.panelNumber = panelNumber;
        this.effect = effect;
        if (this.effect != null)
        {
            GameManager.GameManagerInstance.EffectManagerInstance.RegisterEffect(this.effect, this);
        }
    }
}
