using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class test : MonoBehaviour
{

    public void testtest()
    {
        GameManager.GameManagerInstance.BoardManagerInstance.ExchangePanel(0, 8);

    }

    public void teststests()
    {
        Vector2Int[] a = new Vector2Int[1000];

        for(int i = 0; i<1000; i++)
        {
            a[i] = new Vector2Int(Random.Range(0, 20), Random.Range(0, 20));
        }
        GameManager.GameManagerInstance.BoardManagerInstance.ExchangePanels(a);

    }


    public void Awake()
    {
        aa = new List<Trap>();
    }

    List<Trap> aa;
    public void testTrap()
    {
        int number = Random.Range(0, 20);

        Trap a = new Trap(number, null);
        a.SetPanelNumber(number);

        /*
        foreach (Existence existence in GameManager.GameManagerInstance.BoardManagerInstance.Board[a.GetPanelNumber()].Existences)
        {
            if(existence.GetType() == a.GetType())
            {
                UIManager.UIManagerInstance.SetAnimationRemoveTrap();
            }
        }
        */

        UIManager.UIManagerInstance.SetAnimationGenerateTrap(a);
        aa.Add(a);
    }

    public void testRemoveTrap()
    {
        UIManager.UIManagerInstance.SetAnimationRemoveTrap(aa[0]);
        aa.RemoveAt(0);
    }
}
