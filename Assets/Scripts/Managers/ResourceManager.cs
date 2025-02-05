using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResourceManager
{
    // resource 초기화를 위한 변수들의 모음
    public GameObject player1Prefab { get; private set; }
    public GameObject player2Prefab { get; private set; }

    public GameObject dicePrefab { get; private set; }
    public GameObject emptyPanelPrefab { get; private set; }
    public GameObject eventPanelPrefab { get; private set; }
    public GameObject checkPointPanelPrefab { get; private set; }
    public GameObject backPanelPrefab { get; private set; }

    public GameObject trapPrefab { get; private set; }
    public GameObject trapInstanceInitialPositionPrefab { get; private set; }

    public GameObject diceButtonPrefab { get; private set; }
    public GameObject cardButtonPrefab { get; private set; }

    public GameObject boardCenterPrefab { get; private set; }
    public GameObject cardStartPrefab { get; private set; }
    public GameObject cardCenterPrefab { get; private set; }

    public GameObject useEffectUIPrefab { get; private set; }

    public GameObject panelInfoUIPrefab { get; private set; }

    public GameObject gameOverPrefab { get; private set; }

    //
    public GameObject character1FlagUIPrefab { get; private set; }
    public GameObject character2FlagUIPrefab { get; private set; }

    public GameObject goalFlagUIPrefab { get; private set; }
    //


    public UIResourceManager()
    {
        player1Prefab = Resources.Load<GameObject>("Prefabs/UI/Players/Player1");
        player2Prefab = Resources.Load<GameObject>("Prefabs/UI/Players/Player2");


        emptyPanelPrefab = Resources.Load<GameObject>("Prefabs/UI/Panels/EmptyPanel");
        eventPanelPrefab = Resources.Load<GameObject>("Prefabs/UI/Panels/EventPanel");
        checkPointPanelPrefab = Resources.Load<GameObject>("Prefabs/UI/Panels/CheckPointPanel");
        backPanelPrefab = Resources.Load<GameObject>("Prefabs/UI/Panels/BackPanel");

        trapPrefab = Resources.Load<GameObject>("Prefabs/UI/Trap");
        trapInstanceInitialPositionPrefab = Resources.Load<GameObject>("Prefabs/UI/TrapInstanceInitialPosition");

        diceButtonPrefab = Resources.Load<GameObject>("Prefabs/UIUX/DiceButton");
        cardButtonPrefab = Resources.Load<GameObject>("Prefabs/UIUX/CardButton");
        
        boardCenterPrefab = Resources.Load<GameObject>("Prefabs/UIUX/BoardCenter");

        useEffectUIPrefab = Resources.Load<GameObject>("Prefabs/UI/UseEffectUI");

        panelInfoUIPrefab = Resources.Load<GameObject>("Prefabs/UI/PanelInfoUI");

        gameOverPrefab = Resources.Load<GameObject>("Prefabs/UIUX/GameOver");

        //
        character1FlagUIPrefab = Resources.Load<GameObject>("Prefabs/UI/FlagUI/Character1_FlagUI");
        character2FlagUIPrefab = Resources.Load<GameObject>("Prefabs/UI/FlagUI/Character2_FlagUI");

        goalFlagUIPrefab = Resources.Load<GameObject>("Prefabs/UI/FlagUI/FlagCountToWinUI");
        //
    }
}
