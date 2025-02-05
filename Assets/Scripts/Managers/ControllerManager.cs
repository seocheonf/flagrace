using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{

    public static ControllerManager ControllerManagerInstance { get; private set; }


    private void Awake()
    {
        if (ControllerManagerInstance == null)
        {
            ControllerManagerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    //----------------------------------------------------------------------------------------

    public void SelectDiceActionFromCurrentPlayer()
    {
        Player currentPlayer = GameManager.GameManagerInstance.TurnManagerInstance.GetCurrentPlayer();
        currentPlayer.SetWaitingAction(currentPlayer.GetDice());
    }


    public void SelectCardActionFromCurrentPlayer(int handIndex)
    {

        TurnManager turnManager = GameManager.GameManagerInstance.TurnManagerInstance;
        if (turnManager.CheckBEHAVIOURPhaseEndCondition())
            return;




        Player currentPlayer = turnManager.GetCurrentPlayer();
        if (currentPlayer.GetCardInHand(handIndex, out Card card))
            currentPlayer.SetWaitingAction(card);
        //로직에 알려주기
        currentPlayer.ThrowHand(handIndex);
    }


    public void SelectTurnEndFromCurrentPlayer()
    {
        
        Player currentPlayer = GameManager.GameManagerInstance.TurnManagerInstance.GetCurrentPlayer();
        currentPlayer.SetWaitingAction(currentPlayer.GetBehaviourEnd());

        /*
        //로직에 알려주기
        GameManager.GameManagerInstance.TurnManagerInstance.SetIsTurnEndButtonClick(true);
        */
    }

    public void ShowPanelInfoFromBoard(GameObject panelGameObject)
    {
        if(panelGameObject.CompareTag("Panel"))
        {
            UIManager.UIManagerInstance.SetPanelInfoUIDetail(panelGameObject);
        }
    }


    public void GameOverNextScene()
    {
        MySceneManager.SceneManagerInstance.StartChangeScene("Title");
    }
}
