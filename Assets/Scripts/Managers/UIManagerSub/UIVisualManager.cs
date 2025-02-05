using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UIVisualManager
{
    public Sprite emptyPanelSprite;
    public static Color emptyPanelColor = Color.white;
    public Sprite eventPanelSprite;
    public static Color eventPanelColor = Color.yellow;
    public Sprite checkPointPanelSprite;
    public static Color checkPointPanelColor = Color.red;
    public Sprite backPanelSprite;
    public static Color backPanelColor = Color.black;


    public GameObject boardCenter;
    public List<SpriteRenderer> visualBoard;


    public Dictionary<Existence, GameObject> existenceGameObject;

    public GameObject useEffectUI;
    public UseEffectUIDetail useEffectUIDetail;

    List<TrapUIDetail> trapUIDetailList;
    Transform trapInstanceInitialTransform;

    public GameObject panelInfoUI;
    public PanelInfoUIDetail panelInfoUIDetail;

    public int defaultPlayerLayer = 2;
    public Dictionary<Player, PlayerUIDetail> playerUIDetail;

    public GameObject gameOver;
    public GameOverUIDetail gameOverUIDetail;

    public Dictionary<Player, PlayerFlagUIDetail> playerFlagUIDetail;

    public GoalFlagUIDetail goalFlagUIDetail;

    public UIVisualManager()
    {

        existenceGameObject = new Dictionary<Existence, GameObject>();

        playerFlagUIDetail = new Dictionary<Player, PlayerFlagUIDetail>();


        InitializeVisualBoard();

        InstantiateUseEffectUIVisual();

        InstantiatePanelInfoUIVisual();

    }

    void InitializeVisualBoard()
    {
        UIResourceManager uiResourceManager = UIManager.UIManagerInstance.uiResourceManager;


        boardCenter = uiResourceManager.boardCenterPrefab;

        emptyPanelSprite = uiResourceManager.emptyPanelPrefab.GetComponent<SpriteRenderer>().sprite;
        eventPanelSprite = uiResourceManager.eventPanelPrefab.GetComponent<SpriteRenderer>().sprite;
        checkPointPanelSprite = uiResourceManager.checkPointPanelPrefab.GetComponent<SpriteRenderer>().sprite;
        backPanelSprite = uiResourceManager.backPanelPrefab.GetComponent<SpriteRenderer>().sprite;


        visualBoard = new List<SpriteRenderer>();

    }

    void InstantiateUseEffectUIVisual()
    {
        UIResourceManager uiResourceManager = UIManager.UIManagerInstance.uiResourceManager;

        useEffectUI = GameObject.Instantiate(uiResourceManager.useEffectUIPrefab, uiResourceManager.boardCenterPrefab.transform.position, uiResourceManager.boardCenterPrefab.transform.rotation);
        useEffectUIDetail = useEffectUI.GetComponent<UseEffectUIDetail>();

    }

    void InstantiatePanelInfoUIVisual()
    {
        UIResourceManager uiResourceManager = UIManager.UIManagerInstance.uiResourceManager;

        panelInfoUI = GameObject.Instantiate(uiResourceManager.panelInfoUIPrefab);
        panelInfoUIDetail = panelInfoUI.GetComponent<PanelInfoUIDetail>();

    }

    


    public void InstantiatePanelUIVisual(Panel[] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            GameObject panelInstance = GameObject.Instantiate(UIManager.UIManagerInstance.uiResourceManager.backPanelPrefab, boardCenter.transform.position, boardCenter.transform.rotation);
            existenceGameObject.Add(board[i], panelInstance);
            visualBoard.Add(panelInstance.GetComponent<SpriteRenderer>());

            UIManager.UIManagerInstance.SetAnimationPanelMove(i, panelInstance);
        }
    }

    /*
    public void SetPanelTypeUIVisual(Panel panel)
    {
        SpriteRenderer targetPanel = visualBoard[panel.GetPanelNumber()];
        Sprite panelTypeSprite = null;
        //없는 색깔
        Color panelTypeColor = Color.blue;

        switch(panel.GetPanelType())
        {
            case PanelType.EMPTY:
                panelTypeSprite = emptyPanelSprite;
                panelTypeColor = emptyPanelColor;
                break;
            case PanelType.CHECKPOINT:
                panelTypeSprite = checkPointPanelSprite;
                panelTypeColor = checkPointPanelColor;
                break;
            case PanelType.EVENT:
                panelTypeSprite = eventPanelSprite;
                panelTypeColor = eventPanelColor;
                break;
        }


        targetPanel.sprite = panelTypeSprite;
        targetPanel.color = panelTypeColor;

    }
    */

    public void SetPlayerUIDetail(Player player, int orderInLayer, float colorAlpha)
    {

        playerUIDetail[player].SetSpriteRenderer(orderInLayer, colorAlpha);
        
    }

    GameObject SelectPlayerUIVisual(string playerName)
    {
        switch (playerName)
        {
            case "Character1":
                return UIManager.UIManagerInstance.uiResourceManager.player1Prefab;
            case "Character2":
                return UIManager.UIManagerInstance.uiResourceManager.player2Prefab;
        }


        return UIManager.UIManagerInstance.uiResourceManager.player1Prefab;
    }

    public void InstantiatePlayerUIVisual(LinkedList<Player> playerList, GameObject[] playerFlagUIPositionList)
    {
        playerUIDetail = new Dictionary<Player, PlayerUIDetail>();

        int playerIndex = 0;


        foreach(Player eachPlayer in playerList)
        {
            //위치가 다소 임시 (visual board center)

            //플레이어 ui 설정
            GameObject playerInstance = GameObject.Instantiate(SelectPlayerUIVisual(eachPlayer.playerName), boardCenter.transform.position, boardCenter.transform.rotation);
            PlayerUIDetail eachPlayerUIDetail = playerInstance.GetComponent<PlayerUIDetail>();
            eachPlayerUIDetail.Initialize(eachPlayer);
            playerUIDetail.Add(eachPlayer, eachPlayerUIDetail);


            if(playerIndex >= playerFlagUIPositionList.Length)
            {
                InstantiatePlayerFlagUI(eachPlayer, playerFlagUIPositionList[0]);
            }
            else
            {
                InstantiatePlayerFlagUI(eachPlayer, playerFlagUIPositionList[playerIndex]);
            }
            playerIndex++;

            //임시
            /*
            playerInstance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            playerInstance.name = eachPlayer.playerName;
            playerInstance.SetActive(false);
            playerInstance.SetActive(true);
            */
            existenceGameObject.Add(eachPlayer, playerInstance);

            eachPlayer.MoveTeleport(0, eachPlayer);

            UIManager.UIManagerInstance.SetAnimationOpenPanel(0);

        }



    }

    public void InstantiateTrapUIVisual()
    {
        //풀링 여유분
        int boundaryPlus = 5;
        trapInstanceInitialTransform = UIManager.UIManagerInstance.uiResourceManager.trapInstanceInitialPositionPrefab.transform;

        trapUIDetailList = new List<TrapUIDetail>();
        GameObject trapInstance;
        for (int i = 0; i<(visualBoard.Count + boundaryPlus); i++)
        {
            trapInstance = GameObject.Instantiate(UIManager.UIManagerInstance.uiResourceManager.trapPrefab, trapInstanceInitialTransform.position, trapInstanceInitialTransform.rotation);

            trapUIDetailList.Add(trapInstance.GetComponent<TrapUIDetail>());
        }
        
    }


    bool FindTrapUIDetailInPooling(Trap trap, out TrapUIDetail trapUIDetail)
    {
        trapUIDetail = null;

        if (trap == null)
            return false;

        foreach (TrapUIDetail trapInstance in trapUIDetailList)
        {
            if(trapInstance.GetOwnerTrap() == trap)
            {
                trapUIDetail = trapInstance;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// TrapUIDetail을 TrapInstance풀링으로 부터 받아오고, "대응되는 Trap 객체"와 "TrapUIDetail이 부착된 GameObject"를 연결하는 함수.
    /// </summary>
    /// <param name="trap"> 대응되는 Trap 객체 </param>
    /// <param name="trapUIDetail"> 반환되는 TrapUIDetail </param>
    /// <returns> 풀링 성공 여부 </returns>
    public bool ConnectTrapUIDetailInPooling(Trap trap, out TrapUIDetail trapUIDetail)
    {
        trapUIDetail = null;

        if(FindTrapUIDetailInPooling(trap, out trapUIDetail))
        {
            return true;
        }
        else
        {
            foreach (TrapUIDetail trapInstance in trapUIDetailList)
            {
                if (trapInstance.gameObject.activeSelf == false)
                {
                    trapInstance.SetOwnerTrap(trap);
                    trapInstance.gameObject.SetActive(true);

                    trapUIDetail = trapInstance;

                    existenceGameObject.Add(trap, trapUIDetail.gameObject);

                    return true;
                }
            }
            return false;
        }
    }


    /// <summary>
    /// Trap 객체에 대응되는 TrapUIDetail을 TrapInstance풀링에게 반납하는 함수
    /// </summary>
    /// <param name="trap"> 대응할 Trap 객체 </param>
    /// <param name="trapUIDetail"> 반납한 TrapUIDetail객체 </param>
    /// <returns> 풀링 반납 성공 여부 </returns>
    public bool DisconnectTrapUIDetailInPooling(Trap trap)
    {
        if(FindTrapUIDetailInPooling(trap, out TrapUIDetail trapUIDetail))
        {
            trapUIDetail.SetOwnerTrap(null);

            //--
            trapUIDetail.gameObject.transform.position = trapUIDetail.initialPosition;
            //--
            
            trapUIDetail.gameObject.SetActive(false);

            if(existenceGameObject.TryGetValue(trap, out GameObject temptGameObject))
                existenceGameObject.Remove(trap);

            return true;
        }

        return false;
    }


    public void SetPanelUIVisual(Panel panel)
    {
        //visualBoard[panel.GetPanelNumber()]
    }

    public bool GetVisualPanel(GameObject panelGameObject, out SpriteRenderer targetVisualPanel, out int index)
    {
        targetVisualPanel = null;
        index = 0;
        foreach(SpriteRenderer visualPanel in visualBoard)
        {
            if (visualPanel.gameObject == panelGameObject)
            {
                targetVisualPanel = visualPanel;
                return true;
            }
            else
                index++;

        }
        index = -1;
        return false;
    }

    public bool GetVisualPanel(Panel targetPanel, out SpriteRenderer targetVisualPanel, out int index)
    {
        targetVisualPanel = null;
        index = targetPanel.GetPanelNumber();
        if (index >= visualBoard.Count)
        {
            index = -1;
            return false;
        }
        targetVisualPanel = visualBoard[index];
        return true;
    }


    public void InstantiateGameOver(GameObject targetCanvas)
    {
        gameOver = GameObject.Instantiate<GameObject>(UIManager.UIManagerInstance.uiResourceManager.gameOverPrefab, targetCanvas.transform);
        gameOverUIDetail = gameOver.GetComponent<GameOverUIDetail>();
        gameOver.SetActive(false);
    }

    /// <summary>
    /// 플레이어에 대응하는 Player Flag UI Detail을 생성하는 함수
    /// </summary>
    /// <param name="player"> 플레이어 정보 </param>
    /// <param name="playerFlagUIPositionList"> player flag position </param>
    public void InstantiatePlayerFlagUI(Player player, GameObject playerFlagUIPosition)
    {
        GameObject targetPlayerFlagUI;
        GameObject playerFlagUIInstance;
        PlayerFlagUIDetail eachPlayerFlagUIDetail;


        switch (player.playerName)
        {
            case "Character1":
                targetPlayerFlagUI = UIManager.UIManagerInstance.uiResourceManager.character1FlagUIPrefab;
                break;
            case "Character2":
                targetPlayerFlagUI = UIManager.UIManagerInstance.uiResourceManager.character2FlagUIPrefab;
                break;
            default:
                targetPlayerFlagUI = UIManager.UIManagerInstance.uiResourceManager.character1FlagUIPrefab;
                break;
        }

        playerFlagUIInstance = GameObject.Instantiate<GameObject>(targetPlayerFlagUI, playerFlagUIPosition.transform);
        eachPlayerFlagUIDetail = playerFlagUIInstance.GetComponent<PlayerFlagUIDetail>();
        eachPlayerFlagUIDetail.Initialize();
        playerFlagUIDetail.Add(player, eachPlayerFlagUIDetail);

    }


    public void InstantiateGoalFlagUIVisual(int playerNumber, GameObject goalFlagUIPosition)
    {
        UIResourceManager uiResourceManager = UIManager.UIManagerInstance.uiResourceManager;

        GameObject goalFlagUIInstance;

        goalFlagUIInstance = GameObject.Instantiate<GameObject>(uiResourceManager.goalFlagUIPrefab, goalFlagUIPosition.transform);
        goalFlagUIDetail = goalFlagUIInstance.GetComponent<GoalFlagUIDetail>();
        goalFlagUIDetail.Initialize(GameManager.GameManagerInstance.needFlagCount);
    }

}