using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerFlagUIDetail : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI flagCountTMP;
    int flagCount;

    public void Initialize()
    {
        flagCount = 0;
        flagCountTMP.text = $"x{flagCount}";
    }

    public void SetFlagCount(int flagCount)
    {
        this.flagCount = flagCount;
        flagCountTMP.text = $"x{flagCount}";
    }
}
