using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TurnManager
{
    /*
    public void SetIsRollDice(bool value)
    {
        isRollDice = value;
    }
    */

    /*
    public void SetIsTurnEndButtonClick(bool value)
    {
        isTurnEndButtonCilck = value;
    }
    */

    protected Phase currentPhase;

    protected LinkedList<Player> playerList;
    protected LinkedListNode<Player> currentPlayer;
    //protected Dictionary<Player, GameObject> playerGameObject;

    //protected bool isRollDice;
    //protected bool isTurnEndButtonCilck;

    protected int turn;

    public TurnManager()
    {
        
        currentPhase = Phase.TURN_START;
        playerList = new LinkedList<Player>();
        //isRollDice = false;
        //isTurnEndButtonCilck = false;
        turn = 0;

        {
            
            Player player = new Player(new Booster_CharacterInfo(), "Character1");
            playerList.AddLast(player);

                
            Player player2 = new Player(new Booster_CharacterInfo(), "Character2");
            playerList.AddLast(player2);

            //

            /*
            Player player4 = new Player(new Booster_CharacterInfo(), "Character2");
            playerList.AddLast(player4);

            Player player3 = new Player(new Booster_CharacterInfo(), "Character1");
            playerList.AddLast(player3);
            */

        }

        currentPlayer = playerList.First;
    }



    public void InitializeRemain()
    {
        Panel firstPanel = GameManager.GameManagerInstance.BoardManagerInstance.Board[0];
        foreach(Player eachPlayer in playerList)
        {
            firstPanel.AddExistence(eachPlayer);
        }


        UIManager.UIManagerInstance.InstantiatePlayerUIVisual(playerList);
        /*
        playerGameObject = new Dictionary<Player, GameObject>();

    float playerColor = 0;

        foreach (Player eachPlayer in playerList)
        {
            //위치가 다소 임시 (visual board center)
            GameObject eachPlayerGameObject = GameObject.Instantiate(UIManager.UIManagerInstance.uiResourceManager.playerPrefab, UIManager.UIManagerInstance.boardCenter.transform.position, UIManager.UIManagerInstance.boardCenter.transform.rotation);
            //임시
            eachPlayerGameObject.GetComponent<SpriteRenderer>().color = new Color(playerColor, 0, 0);
            playerColor += 1;
            eachPlayerGameObject.name = eachPlayer.playerName;
            eachPlayerGameObject.SetActive(false);
            eachPlayerGameObject.SetActive(true);
            //임시
            playerGameObject.Add(eachPlayer, eachPlayerGameObject);
            eachPlayer.MoveTeleport(0, eachPlayer);
        }
        */
    }

    public void TurnTask()
    {
        if (playerList.Count == 0)
            return;


        /*
        //임시로 위치 파악 기능임. 잘 움직이나.
        UIManager.UIManagerInstance.asdfasdf.text = "";
        for (int i = 0; i < GameManager.GameManagerInstance.BoardManagerInstance.board.Length; i++)
        {
            Panel tempt = GameManager.GameManagerInstance.BoardManagerInstance.board[i];

            foreach (Player tempt2 in tempt.existences)
            {
                if (tempt2.playerName != null && tempt2.playerName.Length > 0)
                {
                    UIManager.UIManagerInstance.asdfasdf.text += $"player : {tempt2.playerName} / current position : {i}\n";
                    break;
                }
            }
        }
        //임시로 위치 파악 기능임. 잘 움직이나.
        */


        switch (currentPhase)
		{
			case Phase.TURN_START:
                //임시
                //UIManager.UIManagerInstance.SetTurnUI(currentPlayer.Value.playerName);
                currentPlayer.Value.DoTurnStart();
                //SetIsRollDice(false);
                //currentPlayer.Value.SetTurnInfo(true);
                turn++;


                UIManager.UIManagerInstance.DoTurnStart(playerList, currentPlayer);


                NextPhase();
				break;
			case Phase.DRAW:
				currentPlayer.Value.DrawTask();
                NextPhase();
				break;
			case Phase.BEHAVIOUR:
                
                if (CheckBEHAVIOURPhaseEndCondition())
                {
                    NextPhase();
                }
                else if(currentPlayer.Value.Select(out Action action))
                {
                    action.DoAction();
                    GameManager.GameManagerInstance.EffectManagerInstance.UpdateEntireEffectList(Phase.BEHAVIOUR);
                }

                //SetIsTurnEndButtonClick(false);


                /*
                if(currentPlayer.Value.Select(out Action action))
                {
                    action.DoAction();
                    break;
                }
                else if(CheckBEHAVIOURPhaseEndCondition())
                {
                    NextPhase();
                }
                SetIsTurnEndButtonClick(false);
                */

                /*
                    if (isTurnEndButtonCilck && currentPlayer.Value.GetDice().GetIsRollDice())
                    {
                        NextPhase();
                    }z
                    else
                    {
                        SetIsTurnEndButtonClick(false);
                        if(currentPlayer.Value.Select(out Action action))
                        {
                            Debug.Log(action);
                            action.DoAction();
                        }
                    }
                */
                break;
            case Phase.TURN_END:
                //SetIsTurnEndButtonClick(false);
                GameManager.GameManagerInstance.EffectManagerInstance.UseEffect(Timing.TURN_END);
                GameManager.GameManagerInstance.EffectManagerInstance.UpdateEntireEffectList(Phase.TURN_END);
                currentPlayer.Value.DoTurnEnd();

                //임시
                UIManager.UIManagerInstance.HideCard();
                //currentPlayer.Value.SetTurnInfo(false);
                if (currentPlayer.Next != null)
                    currentPlayer = currentPlayer.Next;
                else
                    currentPlayer = playerList.First;
                //isTurnEndButtonCilck = false;
                NextPhase();
                break;
        }

	}
	private void NextPhase()
    {
        if (currentPhase == Phase.TURN_END)
        {
            currentPhase = Phase.TURN_START;
        }
        else
            currentPhase += 1;
    }

    /* 만약 enum에 Index용 값이 있다면?
     currentPhase = (currentPhase + 1) % (int)Phase.Index;
     */

    public Player GetCurrentPlayer()
    {
        return currentPlayer.Value;
    }

    /*
    public GameObject GetPlayerGameObject(Player player)
    {
        if (playerGameObject.TryGetValue(player, out GameObject value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
    */

    //--------
    public bool CheckBEHAVIOURPhaseEndCondition()
    {
        return currentPlayer.Value.GetIsBehaviourEnd();
    }

    //--------

}
