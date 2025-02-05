using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    AnimationCurve animationCurve;

    public UIResourceManager uiResourceManager { get; private set; }

    GameObject mainCanvas;

    [SerializeField]
    GameObject cardCenter;
    RectTransform cardCenterRectTransform;

    [SerializeField]
    GameObject cardStart;
    RectTransform cardStartRectTransform;

    [SerializeField]
    GameObject cardNetural;
    RectTransform cardNeturalRectTransform;

    [SerializeField]
    GameObject diceCenter;

    GameObject diceButtonGameObject;
    Button diceButton;

    List<RectTransform> cardsRectTransform;
    List<Button> cardsButton;
    List<CardButtonDetail> cardsButtonDetail;
    // ī���� ��Ŀ�� middle, center�� ���
    Vector2 cardSize;

    [SerializeField]
    GameObject[] playerFlagUIPositionList;
    [SerializeField]
    GameObject[] goalFlagUIPositionList;


    public void InitializeVisual()
    {
        //InitializeVisualBoard();
        InitializeVisualDice();
        InitializeVisualCard();
        InitializeVisualGameOver();
    }

    public void InitializeVisualDice()
    {
        diceButtonGameObject = Instantiate(uiResourceManager.diceButtonPrefab, diceCenter.transform);
        diceButton = diceButtonGameObject.GetComponent<Button>();
        diceButton.onClick.AddListener(() => { ControllerManager.ControllerManagerInstance.SelectDiceActionFromCurrentPlayer(); });
    }

    public void InitializeVisualCard()
    {
        cardsRectTransform = new List<RectTransform>();
        cardsButton = new List<Button>();
        cardsButtonDetail = new List<CardButtonDetail>();

        cardCenterRectTransform = cardCenter.GetComponent<RectTransform>();
        cardStartRectTransform = cardStart.GetComponent<RectTransform>();
        cardNeturalRectTransform = cardNetural.GetComponent<RectTransform>();

        cardSize = cardCenterRectTransform.sizeDelta;

        //�ӽ÷� 10���� ī�� ��ư�� ����
        for (int i = 0; i < 10; i++)
        {
            RectTransform eachCardButtonRectTransform = Instantiate(uiResourceManager.cardButtonPrefab, mainCanvas.transform).GetComponent<RectTransform>();
            eachCardButtonRectTransform.anchoredPosition = cardStartRectTransform.anchoredPosition;
            cardsRectTransform.Add(eachCardButtonRectTransform);
        }

        foreach (RectTransform eachRectTransform in cardsRectTransform)
        {
            cardsButton.Add(eachRectTransform.GetComponent<Button>());
            cardsButtonDetail.Add(eachRectTransform.GetComponent<CardButtonDetail>());
        }

        for(int i = 0; i < cardsButton.Count; i++)
        {
            int addlistenerIndex = i;
            cardsButton[i].interactable = false;
            cardsButton[i].onClick.AddListener(() => { ControllerManager.ControllerManagerInstance.SelectCardActionFromCurrentPlayer(addlistenerIndex); });
            cardsButton[i].gameObject.SetActive(false);
        }
    }

    void InitializeVisualGameOver()
    {
        uiVisualManagerInstance.InstantiateGameOver(mainCanvas);
    }

    /*
    public List<GameObject> visualBoard;
    public GameObject boardCenter;


    public void InitializeVisualBoard()
    {
        visualBoard = new List<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            GameObject eachPanelGameObject = Instantiate(uiResourceManager.backPanelPrefab, boardCenter.transform.position, boardCenter.transform.rotation);
            visualBoard.Add(eachPanelGameObject);
            SetAnimationPanelMove(i, eachPanelGameObject);
        }
    }
    */

    //�ϸŴ��� ��ġ �����ֱ� �ӽÿ�
    public TextMeshProUGUI asdfasdf;
    //

    public static UIManager UIManagerInstance { get; private set; }
    //--
    Queue<UIComponent> uiComponentList;
    //--
    DiceUIComponent diceUI;
    [SerializeField]
    DiceUIDetail diceUIDetail;
    //--
    //List<PlayerUI> playerUI;
    //--
    //TurnUI turnUI;
    public TextMeshProUGUI turnTMP;
    //--

    private void Awake()
    {
        if (UIManagerInstance == null)
        {
            UIManagerInstance = this;
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //�ڽ��� �����ϴ� UI���� �����ϰ� �ʱ�ȭ.
    private void Initialize()
    {

        uiResourceManager = new UIResourceManager();

        uiVisualManagerInstance = new UIVisualManager();

        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");


        uiComponentList = new Queue<UIComponent>();

        diceUI = new DiceUIComponent(diceUIDetail, null);

        //playerUI = new List<PlayerUI>();

        //turnUI = new TurnUI(turnTMP, 1);

        InitializeVisual();

    }

    /// <summary>
    /// ���� �÷��̾��� ��ġ�� ���� �޸��� �ִϸ��̼� ����.
    /// </summary>
    /// <param name="player"> �̵��� �÷��̾��� ���� </param>
    public void SetAnimationPlayerMoveRun(Player player)
    {
        GameObject playerGameObject = uiVisualManagerInstance.existenceGameObject[player];
        Transform playerTransform = uiVisualManagerInstance.visualBoard[player.GetPanelNumber()].transform;
        uiComponentList.Enqueue(new PlayerMoveRunUIComponent(uiVisualManagerInstance.playerUIDetail[player], playerGameObject, playerTransform, animationCurve, 0.5f));
    }

    /// <summary>
    /// ���� �÷��̾��� ��ġ�� �ڷ���Ʈ �ִϸ��̼� ����.
    /// </summary>
    /// <param name="player"> �̵��� �÷��̾��� ���� </param>
    public void SetAnimationPlayerMoveTeleport(Player player)
    {
        GameObject playerGameObject = uiVisualManagerInstance.existenceGameObject[player];
        Transform playerTransform = uiVisualManagerInstance.visualBoard[player.GetPanelNumber()].transform;
        uiComponentList.Enqueue(new PlayerMoveTeleportUIComponent(playerGameObject, playerTransform, 0.3f));
        uiComponentList.Enqueue(new PlayerMoveTeleportAfterUIComponent(uiVisualManagerInstance.playerUIDetail[player]));
    }

    public void SetAnimationPanelMove(int index, GameObject gameObject)
    {
        uiComponentList.Enqueue(new PanelMoveUIComponent(uiVisualManagerInstance.boardCenter.transform.position, index, gameObject, 0.1f));
    }

    public void SetAnimationSetPanelType(Panel panel)
    {

        SpriteRenderer targetPanel = uiVisualManagerInstance.visualBoard[panel.GetPanelNumber()];
        Sprite panelTypeSprite = null;
        //���� ����
        Color panelTypeColor = Color.blue;

        switch (panel.GetPanelType())
        {
            case PanelType.EMPTY:
                panelTypeSprite = uiVisualManagerInstance.emptyPanelSprite;
                panelTypeColor = UIVisualManager.emptyPanelColor;
                break;
            case PanelType.CHECKPOINT:
                panelTypeSprite = uiVisualManagerInstance.checkPointPanelSprite;
                panelTypeColor = UIVisualManager.checkPointPanelColor;
                break;
            case PanelType.EVENT:
                panelTypeSprite = uiVisualManagerInstance.eventPanelSprite;
                panelTypeColor = UIVisualManager.eventPanelColor;
                break;
        }

        uiComponentList.Enqueue(new ColorSetUIComponent(targetPanel, panelTypeColor, 0.3f));

    }

    public void SetAnimationSetPanelType(int panelNumber)
    {
        Panel panel = GameManager.GameManagerInstance.BoardManagerInstance.Board[panelNumber];

        SpriteRenderer targetPanel = uiVisualManagerInstance.visualBoard[panelNumber];
        Sprite panelTypeSprite = null;
        //���� ����
        Color panelTypeColor = Color.blue;

        switch (panel.GetPanelType())
        {
            case PanelType.EMPTY:
                panelTypeSprite = uiVisualManagerInstance.emptyPanelSprite;
                panelTypeColor = UIVisualManager.emptyPanelColor;
                break;
            case PanelType.CHECKPOINT:
                panelTypeSprite = uiVisualManagerInstance.checkPointPanelSprite;
                panelTypeColor = UIVisualManager.checkPointPanelColor;
                break;
            case PanelType.EVENT:
                panelTypeSprite = uiVisualManagerInstance.eventPanelSprite;
                panelTypeColor = UIVisualManager.eventPanelColor;
                break;
        }

        uiComponentList.Enqueue(new ColorSetUIComponent(targetPanel, panelTypeColor, 0.3f));

    }

    public void SetAnimationOpenPanel(Panel panel)
    {
        /*
        if (panel.IsOpen)
            return;
        */
        SpriteRenderer targetPanel = uiVisualManagerInstance.visualBoard[panel.GetPanelNumber()];
        Sprite panelTypeSprite = null;
        //���� ����
        Color panelTypeColor = Color.blue;

        switch (panel.GetPanelType())
        {
            case PanelType.EMPTY:
                panelTypeSprite = uiVisualManagerInstance.emptyPanelSprite;
                panelTypeColor = UIVisualManager.emptyPanelColor;
                break;
            case PanelType.CHECKPOINT:
                panelTypeSprite = uiVisualManagerInstance.checkPointPanelSprite;
                panelTypeColor = UIVisualManager.checkPointPanelColor;
                break;
            case PanelType.EVENT:
                panelTypeSprite = uiVisualManagerInstance.eventPanelSprite;
                panelTypeColor = UIVisualManager.eventPanelColor;
                break;
        }

        uiComponentList.Enqueue(new ColorSetUIComponent(targetPanel, panelTypeColor, 0.3f));
    }

    public void SetAnimationOpenPanel(int panelNumber)
    {
        Panel panel = GameManager.GameManagerInstance.BoardManagerInstance.Board[panelNumber];
        /*
        if (panel.IsOpen)
            return;
        */
        SpriteRenderer targetPanel = uiVisualManagerInstance.visualBoard[panelNumber];
        Sprite panelTypeSprite = null;
        //���� ����
        Color panelTypeColor = Color.blue;

        switch (panel.GetPanelType())
        {
            case PanelType.EMPTY:
                panelTypeSprite = uiVisualManagerInstance.emptyPanelSprite;
                panelTypeColor = UIVisualManager.emptyPanelColor;
                break;
            case PanelType.CHECKPOINT:
                panelTypeSprite = uiVisualManagerInstance.checkPointPanelSprite;
                panelTypeColor = UIVisualManager.checkPointPanelColor;
                break;
            case PanelType.EVENT:
                panelTypeSprite = uiVisualManagerInstance.eventPanelSprite;
                panelTypeColor = UIVisualManager.eventPanelColor;
                break;
        }

        uiComponentList.Enqueue(new ColorSetUIComponent(targetPanel, panelTypeColor, 0.3f));

    }

    public void SetAnimationUseEffectUI(Effect effect)
    {
        string effectOwner = "empty";
        switch(effect.Owner.ExistenceType)
        {
            case ExistenceType.TURN_PLAYER:
            case ExistenceType.OTHER_PLAYER:
                effectOwner = "�÷��̾� : " + ((Player)effect.Owner).playerName;
                break;
            case ExistenceType.PANEL:
                effectOwner = effect.Owner.GetPanelNumber() + "�� �г�";
                break;
            case ExistenceType.TRAP:
                effectOwner = effect.Owner.GetPanelNumber() + "�� �г� ����";
                break;
        }

        uiComponentList.Enqueue(new RegisterEffectUseUIComponent(effectOwner, effect.description, uiVisualManagerInstance.useEffectUIDetail, 0.8f));
    }

    /// <summary>
    /// �г��� ��ȯ�� ��, �гΰ��� ��ȯ �ִϸ��̼��� �����Ų��.
    /// </summary>
    /// <param name="firstPanel">��ȯ ���1</param>
    /// <param name="secondPanel">��ȯ ���2</param>
    /// <param name="durationWeight">�ִϸ��̼� ���� �ð� ����ġ</param>
    public void SetAnimationExchangePanel(Panel firstPanel, Panel secondPanel, float durationWeight = 1.0f)
    {
        //�����Ǵ� ���ӿ�����Ʈ ��ġ�� ������ ��ȯ�� �Ķ� ���� ��߳��� �Ǿ�����.
        SpriteRenderer firstIndexPanelSpriteRenderer = uiVisualManagerInstance.visualBoard[secondPanel.GetPanelNumber()]; 
        SpriteRenderer secondIndexPanelSpriteRenderer = uiVisualManagerInstance.visualBoard[firstPanel.GetPanelNumber()];

        List<Transform> firstIndexAllTargetTransforms = new List<Transform>();
        List<Transform> secondIndexAllTargetTransforms = new List<Transform>();

        firstIndexAllTargetTransforms.Add(firstIndexPanelSpriteRenderer.transform);
        foreach (Existence eachExistence in firstPanel.Existences)
        {
            firstIndexAllTargetTransforms.Add(uiVisualManagerInstance.existenceGameObject[eachExistence].transform);
        }

        secondIndexAllTargetTransforms.Add(secondIndexPanelSpriteRenderer.transform);
        foreach (Existence eachExistence in secondPanel.Existences)
        {
            secondIndexAllTargetTransforms.Add(uiVisualManagerInstance.existenceGameObject[eachExistence].transform);
        }

        uiComponentList.Enqueue(new PanelExchangeUIComponent(firstIndexPanelSpriteRenderer, firstIndexAllTargetTransforms, secondIndexPanelSpriteRenderer, secondIndexAllTargetTransforms, durationWeight * 1.0f));
    }

    public void SetAnimationGenerateTrap(Trap trap)
    {
        if (!UseTrapUIDetail(trap, out TrapUIDetail trapUIDetail))
            return;

        Transform targetTransform = uiVisualManagerInstance.visualBoard[trap.GetPanelNumber()].transform;

        uiComponentList.Enqueue(new TrapGenerateUIComponent(trapUIDetail, targetTransform, 0.5f));
    }

    public void SetAnimationRemoveTrap(Trap trap)
    {
        if (!UseTrapUIDetail(trap, out TrapUIDetail trapUIDetail))
            return;

        uiComponentList.Enqueue(new TrapRemoveUIComponent(trapUIDetail));
    }


    //���
    public bool UseTrapUIDetail(Trap trap, out TrapUIDetail trapUIDetail)
    {
        return uiVisualManagerInstance.ConnectTrapUIDetailInPooling(trap, out trapUIDetail);
    }

    //�ݳ�
    public void DisuseTrapUIDetail(Trap trap)
    {
        uiVisualManagerInstance.DisconnectTrapUIDetailInPooling(trap);
    }



    public void SetPanelInfoUIDetail(GameObject panelGameObject)
    {
        if(uiVisualManagerInstance.GetVisualPanel(panelGameObject, out SpriteRenderer targetVisualPanel, out int panelIndex))
        { 
            try
            {
                Panel targetPanel = GameManager.GameManagerInstance.BoardManagerInstance.Board[panelIndex];
                ApplyPanelInfoUIDetail(targetPanel, targetVisualPanel);
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    public void SetPanelInfoUIDetail(Panel targetPanel)
    {
        if(uiVisualManagerInstance.GetVisualPanel(targetPanel, out SpriteRenderer targetVisualPanel, out int panelIndex))
        {
            ApplyPanelInfoUIDetail(targetPanel, targetVisualPanel);
        }
    }



    void ApplyPanelInfoUIDetail(Panel targetPanel, SpriteRenderer targetVisualPanel)
    {
        uiVisualManagerInstance.panelInfoUIDetail.SetPanelInfoUIDetail(targetPanel, targetVisualPanel);
    }


    public void SetDiceUI(int number)
    {
        diceUI.SetDiceUINumber(number);
    }

    /*
    public void SetPlayerUI()
    {
        //playerUI.SetPlayerUI_etc....;
    }
    */

    /*
    //�ӽ�
    public void SetTurnUI(string name)
    {
        turnUI.SetUIPlayerName(name);
    }
    */




    public void AddUIComponentList(UIComponent uiComponent)
    {
        uiComponentList.Enqueue(uiComponent);
    }


    
    // �ִϸ��̼��� List�� ���
    /*
    public bool UpdateUI()
    {
        if (uiComponentList.Count > 0)
        {
            if (!uiComponentList[0].UpdateUI())
                uiComponentList.RemoveAt(0);
            return true;
        }
        else
            return false;
    }
    */

    
    //�ִϸ��̼��� Queue�� ���
    UIComponent currentUIComponent;
    public bool UpdateUI()
    {
        if (currentUIComponent == null)
        {
            if (uiComponentList.TryDequeue(out currentUIComponent))
                currentUIComponent.StartAnimation();
            else
                return false;
        }
        else if (!currentUIComponent.UpdateUI())
            currentUIComponent = null;

        return true;
    }
    



    // ī�� �ڵ� ����


    //�ӽ�
    public void HideCard()
    {
        for (int i = 0; i < cardsRectTransform.Count; i++)
        {
            cardsRectTransform[i].anchoredPosition = cardStartRectTransform.anchoredPosition;
            cardsButton[i].interactable = false;
            cardsRectTransform[i].gameObject.SetActive(false);
        }
    }

    // �÷��̾� �ڵ带 �޾ƿ� UI�� �ݿ��Ѵ�.
    List<RectTransform> SetVisualCard(List<Card> playerHand)
    {
        //�ӽ�
        for(int i = 0; i<cardsRectTransform.Count; i++)
        {
            cardsButton[i].interactable = false;
            cardsRectTransform[i].gameObject.SetActive(false);
        }

        List<RectTransform> realVisualCard = new List<RectTransform>();
        for (int i = 0; i < playerHand.Count; i++)
        {
            if (playerHand[i] == null)
                continue;
            else
            {

                realVisualCard.Add(cardsRectTransform[i]);
                cardsRectTransform[i].gameObject.SetActive(true);
                cardsButton[i].interactable = true;
                cardsButtonDetail[i].SetCardButtonDetail(playerHand[i].moveRange, playerHand[i].cardEffect.description);

            }
        }
        return realVisualCard;
    }

    void DrawVisualCard(List<Card> playerHand, Card drawCard)
    {

    }

    void ThrowVisualCard(List<Card> playerHand, Card throwCard)
    {

    }









    /// <summary>
    /// ī�带 ��ο��� �� �ִϸ��̼�.
    /// </summary>
    /// <param name="playerHand"> ���� �÷��̾��� �� �� </param>
    /// <param name="drawCard"> ��ο��� ī�� </param>
    public void SetAnimationDrawCard(List<Card> playerHand, Card drawCard)
    {

        //uiComponentList.Add(new CardsSetUIComponent(,,, 0.6f));
    }



    /// <summary>
    /// ī�带 ��ο��� �� �ִϸ��̼�.
    /// </summary>
    /// <param name="playerHand"> ���� �÷��̾��� �� �� </param>
    /// <param name="drawCard"> ��ο��� ī�� </param>
    public void SetAnimationThrowCard(List<Card> playerHand, Card throwCard)
    {

        //uiComponentList.Add(new CardsSetUIComponent(,,, 0.6f));
    }



    // �߰����� ��ο쳪 ��� �ִϸ��̼��� ���� ���, �� �Լ��� �ٷ� ����ϸ� �ȵ�..��? ����غ���.
    /// <summary>
    /// ī�� �и� �ʱ�ȭ�� �� �ִϸ��̼�.
    /// </summary>
    /// <param name="playerHand"> ���� �÷��̾��� �� �� </param>
    /// <param name="drawCard"> ��ο��� ī�� </param>
    public void SetAnimationSetCards(List<Card> playerHand)
    {
        uiComponentList.Enqueue(new CardsSetUIComponent(SetVisualCard(playerHand),cardCenterRectTransform.anchoredPosition, cardSize, 0.6f));
    }


    //--


    public void DoTurnStart(LinkedList<Player> playerList, LinkedListNode<Player> currentPlayer)
    {

        LinkedListNode<Player> temptPlayer = currentPlayer.Previous;
        if(temptPlayer == null)
        {
            temptPlayer = playerList.Last;
        }

        int currentPlayerLayer = uiVisualManagerInstance.defaultPlayerLayer;

        while (temptPlayer != currentPlayer)
        {
            uiVisualManagerInstance.SetPlayerUIDetail(temptPlayer.Value, currentPlayerLayer, 0.5f);
            temptPlayer = temptPlayer.Previous;
            if (temptPlayer == null)
            {
                temptPlayer = playerList.Last;
            }
            currentPlayerLayer++;
        }

        uiVisualManagerInstance.SetPlayerUIDetail(temptPlayer.Value, currentPlayerLayer, 1f);

    }



    //--

    public void SetAnimationPlayerFlagUI(Player player, int flagCount)
    {
        PlayerFlagUIDetail playerFlagUIDetail = uiVisualManagerInstance.playerFlagUIDetail[player];

        uiComponentList.Enqueue(new PlayerFlagUIComponent(playerFlagUIDetail, flagCount));
    }


    //--

    public void SetGameOver(Player winner)
    {
        int characterIndex = 0;

        switch(winner.playerName)
        {
            case "Character1":
                characterIndex = 1;
                break;
            case "Character2":
                characterIndex = 2;
                break;
        }

        uiComponentList.Enqueue(new GameOverUIComponent(uiVisualManagerInstance.gameOver, uiVisualManagerInstance.gameOverUIDetail, characterIndex));
    }




    //--

    UIVisualManager uiVisualManagerInstance;

    public void InstantiatePanelUIVisual(Panel[] board)
    {
        uiVisualManagerInstance.InstantiatePanelUIVisual(board);
        uiVisualManagerInstance.InstantiateTrapUIVisual();
    }

    public void InstantiatePlayerUIVisual(LinkedList<Player> playerList)
    {
        uiVisualManagerInstance.InstantiatePlayerUIVisual(playerList, playerFlagUIPositionList);

        InstantiateGoalFlagUIVisual(playerList.Count);

    }

    void InstantiateGoalFlagUIVisual(int playerCount)
    {
        //Ȥ�ø� ���� üũ
        if (goalFlagUIPositionList == null || goalFlagUIPositionList.Length < 1)
            return;

        //�ּ� 2��
        int goalFlagUIPositionIndex = playerCount - 2;

        //�� �ο�, 1�� ���� ó��. �׳� 0���� �ع�����.
        if (goalFlagUIPositionIndex >= goalFlagUIPositionList.Length || goalFlagUIPositionIndex < 0)
            goalFlagUIPositionIndex = 0;

        uiVisualManagerInstance.InstantiateGoalFlagUIVisual(playerCount, goalFlagUIPositionList[goalFlagUIPositionIndex]);

    }



}