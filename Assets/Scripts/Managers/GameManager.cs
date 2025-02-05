using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager GameManagerInstance { get; private set; }


    public CardManager CardManagerInstance { get; private set; }

    public TurnManager TurnManagerInstance { get; private set; }

    public EffectManager EffectManagerInstance { get; private set; }

    public BoardManager BoardManagerInstance { get; private set; }

    private bool isApplicated;

    public int needFlagCount = 2;

    //
    [SerializeField]
    private int playerNumber;
    //

    private void Awake()
    {
        if (GameManagerInstance == null)
        {
            GameManagerInstance = this;
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {

        if (Application() && TurnManagerInstance.CheckBEHAVIOURPhaseEndCondition())
            return;
        Simulation();

        /*
        if (Application())
            return;
        Simulation();
        */
    }

    private bool Simulation()
    {
        if (TurnManagerInstance != null)
        {
            TurnManagerInstance.TurnTask();
            return true;
        }
        return false;
    }

    private bool Application()
    {
        if (UIManager.UIManagerInstance == null)
            Debug.Log("error");
        return UIManager.UIManagerInstance.UpdateUI();
    }

    public bool ApplicationCompelete()
    {
        return default;
    }

    private void Initialize()
    {
        CardManagerInstance = new CardManager();

        EffectManagerInstance = new EffectManager();

        TurnManagerInstance = new TurnManager();

        BoardManagerInstance = new BoardManager();

        TurnManagerInstance.InitializeRemain();

    }

    Player winner;
    public void GameOver(Player winner)
    {
        if (winner != null)
        {
            this.winner = winner;
        }
        else
            return;

        UIManager.UIManagerInstance.SetGameOver(this.winner);


    }


    private void OnDestroy()
    {
        CardManagerInstance = null;
        TurnManagerInstance = null;
        EffectManagerInstance = null;
        BoardManagerInstance = null;
    }

}