using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalFlagUIDetail : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI flagCountTMP;
    int flagCount;

    public void Initialize(int flagCount)
    {
        this.flagCount = flagCount;
        flagCountTMP.text = $"x{this.flagCount} Win!";
    }

    /*

    public void SetFlagCount(int flagCount)
    {
        this.flagCount = flagCount;
        flagCountTMP.text = $"x{flagCount} Win!";
    }
    */
}
