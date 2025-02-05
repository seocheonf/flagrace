using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardButtonDetail : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI number;
    [SerializeField]
    TextMeshProUGUI description;

    public void SetCardButtonDetail(int number, string description)
    {
        this.number.text = number.ToString();
        this.description.text = description;
    }
}
