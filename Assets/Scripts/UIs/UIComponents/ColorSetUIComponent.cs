using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetUIComponent : UIComponent
{
    SpriteRenderer spriteRenderer;
    Color targetColor;
    Color currentColor;

    public ColorSetUIComponent(SpriteRenderer spriteRenderer, Color targetColor, float duration) : base(null, duration)
    {
        this.spriteRenderer = spriteRenderer;
        this.targetColor = targetColor;
        currentColor = new Color(0, 0, 0);
    }

    public override bool UpdateAnimation()
    {


        currentColor = spriteRenderer.color;

        currentColor.r = Mathf.Lerp(currentColor.r, targetColor.r, 3 * Time.deltaTime / duration);
        currentColor.g = Mathf.Lerp(currentColor.g, targetColor.g, 3 * Time.deltaTime / duration);
        currentColor.b = Mathf.Lerp(currentColor.b, targetColor.b, 3 * Time.deltaTime / duration);


        spriteRenderer.color = currentColor;
        return true;
    }
    public override void EndAnimation()
    {
        spriteRenderer.color = targetColor;
    }
}
