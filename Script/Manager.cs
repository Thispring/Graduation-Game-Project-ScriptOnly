using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;

public class Manager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public Dealer dealer;
    private GameObject[] objectsWithTag;
    private GameObject[] objectsWithTag2;
    private GameObject battlebuttonObj;
    private GameObject unitmakebuttonObj;
    private GameObject restartObj;
    public GameObject sufflebutton;
    private GameObject timerUI;
    private int unitclick = 0;
    private bool battlebutton = false;
    private Dealer dealerInstance;
    private Enemy enemyInstance;
    private float time = 60f;
    private bool isRunning = false;
    [SerializeField]
    public int life = 1;
    [SerializeField]
    public int enemylife = 1;
    public GameObject creatEffect;
    public float savedPlaybackTime = 1f;
    public TextMeshProUGUI deckCount;
    public SoundManager soundManager;
    public EffectSoundManager effectSoundManager;
    public Option option;


    void Awake()
    {
        objectsWithTag2 = GameObject.FindGameObjectsWithTag("Result");
        foreach (GameObject obj in objectsWithTag2)
        {
            obj.SetActive(false);
        }

        timerUI = GameObject.Find("TimerUI");
        timerUI.SetActive(false);
        unitmakebuttonObj = GameObject.Find("MainCanvas/UnitMakeButton");
        unitmakebuttonObj.SetActive(false);
        sufflebutton.SetActive(false);

        StartCoroutine(GameStart(0.5f));

        // life 수치 변경 하는 곳
        life = PlayerPrefs.GetInt("life", 3);
        enemylife = PlayerPrefs.GetInt("enemylife", 3);

        TextMeshProUGUI playerlifes = GameObject.Find("LifeCanvas/PlayerLife").GetComponent<TextMeshProUGUI>();
        playerlifes.text = life.ToString();
        TextMeshProUGUI enemylifes = GameObject.Find("LifeCanvas/EnemyLife").GetComponent<TextMeshProUGUI>();
        enemylifes.text = enemylife.ToString();

        time = PlayerPrefs.GetFloat("timer", time);

        objectsWithTag = GameObject.FindGameObjectsWithTag("Field");
        foreach (GameObject obj in objectsWithTag)
        {
            obj.SetActive(false);
        }

        battlebuttonObj = GameObject.Find("MainCanvas/BattleButton");
        battlebuttonObj.SetActive(false);

        restartObj = GameObject.Find("RestartCanvas/Restart");
        restartObj.SetActive(false);

        dealerInstance = FindObjectOfType<Dealer>();
        enemyInstance = FindObjectOfType<Enemy>();

        // PlayerPrefs에서 저장된 재생 시간 불러오기
        float savedPlaybackTime = PlayerPrefs.GetFloat("MusicPlaybackTime", 0f);
        //audioSource.time = savedPlaybackTime;
        option.soundManager.ingametargetAudioSource.time = savedPlaybackTime;
    }

    public bool active = false;
    public int testwld = 0;
    public Field field;
    void Update()
    {
        int cardCount = dealer.cardList.Count;
        deckCount.text = "남은 카드 수: " + cardCount.ToString();

        if (field.vdd == true)
        {
            foreach (GameObject obj in objectsWithTag2)
            {
                obj.SetActive(true);
            }
        }

        if (life <= 0)
        {
            restartObj.SetActive(true);
        }
        if (enemylife <= 0)
        {
            restartObj.SetActive(true);
        }

        // 전투시작 버튼 생성
        if (battlebutton == true)
        {
            GameObject fieldObj = GameObject.Find("Field");

            Field fieldComponent = fieldObj.GetComponent<Field>();

            if (!fieldComponent.Fightable())
            {
                battlebuttonObj.SetActive(false);
            }
            else
            {
                battlebuttonObj.SetActive(true);
            }
        }
        if (battlebutton == false)
        {
            battlebuttonObj.SetActive(false);
        }

        // 타이머 기능
        if (isRunning)
        {
            time -= Time.deltaTime; // 경과 시간 갱신
            TextMeshProUGUI timer = GameObject.Find("MainCanvas/Timer").GetComponent<TextMeshProUGUI>();
            timer.text = "" + Mathf.RoundToInt(time).ToString();
            if (time <= 0f)
            {
                int nullCount = player.selectedhands.Count(card => card == null);
                int requiredCount = player.selectedhands.Count - nullCount;

                if (requiredCount < 5)
                {
                    // firsthands 리스트에서 null이 아닌 카드들의 인덱스를 수집
                    List<int> availableIndexes = new List<int>();
                    for (int i = 0; i < player.firsthands.Count; i++)
                    {
                        if (player.firsthands[i] != null)
                        {
                            availableIndexes.Add(i);
                        }
                    }

                    // 필요한 개수만큼 인덱스를 중복 없이 랜덤하게 선택
                    System.Random random = new System.Random();
                    int availableCount = availableIndexes.Count;
                    if (requiredCount + availableCount < 5)
                    {
                        requiredCount = availableCount;
                    }
                    else
                    {
                        requiredCount = 5 - requiredCount;
                    }

                    for (int i = 0; i < requiredCount; i++)
                    {
                        int randomIndex = random.Next(availableIndexes.Count);
                        int selectedIndex = availableIndexes[randomIndex];
                        Card card = player.firsthands[selectedIndex];
                        int index = player.selectedhands.FindIndex(c => c == null);
                        player.selectedhands[index] = card;
                        player.firsthands[selectedIndex] = null;
                        availableIndexes.RemoveAt(randomIndex);
                    }
                }

                UnitCardMake();
                time = 60f; // 시간 초기화
            }
        }

        // 재생 중인 음악의 현재 시간을 저장
        //savedPlaybackTime = audioSource.time;
        savedPlaybackTime = option.soundManager.ingametargetAudioSource.time;

        // PlayerPrefs에 재생 시간 저장
        PlayerPrefs.SetFloat("MusicPlaybackTime", savedPlaybackTime);
    }

    public int remainingCards = 0;
    public void NewShuffleCards()
    {
        HashSet<Card> dealerCardSet = new HashSet<Card>(dealer.cardList);

        foreach (Card card in player.firsthands.Where(card => card != null))
        {
            card.DisableEffects();
            dealerCardSet.Add(card);
            card.Move(new Vector3(0f, 10f, 0), true);
            card.Flip(false);
        }

        // 선택된 카드의 개수를 계산
        int selectedCardCount = player.selectedhands.Count(card => card != null);

        dealer.cardList = dealerCardSet.ToList();

        // 9에서 선택된 카드 개수를 뺀 나머지 카드를 셔플하기
        remainingCards = 9 - selectedCardCount;
        List<Card> cardsToShuffle = dealer.cardList.Where(card => !player.selectedhands.Contains(card)).ToList();
        ShuffleList(cardsToShuffle);

        int index = 0;

        // player.firsthands에 남은 카드 할당
        for (int i = 0; i < player.firsthands.Count; i++)
        {
            if (player.firsthands[i] == null)
            {
                player.firsthands[i] = cardsToShuffle[index];
                index++;

                if (index >= remainingCards)
                    break; // 필요한 개수만큼 할당했으면 종료
                    
            }
            foreach (Card card in player.firsthands.Where(card => card != null))
            {
                card.EnableEffects();
            }
        }
        player.firsthands.Clear();
        player.firsthandsIndex.Clear();
        player.firsthands = new List<Card>(new Card[9]);

        player.clickAlbe = false;
        Debug.Log(index);
        Debug.Log(remainingCards);
        dealer.ReShareCard(remainingCards);
        Destroy(sufflebutton.gameObject);
    }
    // List를 무작위로 섞는 메서드
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void ReStartButton()
    {
        SceneManager.LoadScene("Main");
    }

    private int unitMoveDelay = 0;
    public void UnitCardMake()
    {
        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI");
        if (player.selectedhands[0] != null && player.selectedhands[1] != null && player.selectedhands[2] != null && player.selectedhands[3] != null && player.selectedhands[4] != null)
        {
            unitMoveDelay ++;
            player.clickAlbe = false;
            time = 60f;
            List<string> unithands = new List<string>();
            string handRank = PokerChecker.GetPokerHand(player.selectedhands);
            Player.handRank = handRank;
            player.unithands.Add(handRank); // handRank 값을 unithand 리스트에 추가

            UnitCard unitCard = Instantiate<UnitCard>(player.unitCardPrefab, new Vector3(0.78f, -1.02f, 0), Quaternion.identity);
            unitCard.transform.DOMove(new Vector3(player.Unit_xPositions[unitclick], player.Unit_yPositions[unitclick], 0), 0.7f);
            unitCard.Init(handRank, unitclick);
            unitclick++;
            // 리스트 안에 있는 모든 게임 오브젝트를 파괴합니다.
            foreach (Card card in player.selectedhands)
            {
                Destroy(card.gameObject);
            }

            // 리스트를 비웁니다.
            player.selectedhands = new List<Card>(new Card[5]);

            //enemy.CreateUnitCard();
            enemy.NewCreateUnitCard();

            if (unitMoveDelay <= 2)
            {
                dealer.ShareCard();
                dealer.EnemyReShareCard();
            }

            GameObject buttonObj = GameObject.Find("UnitMakeButton");
            if (buttonObj != null && player.unithands.Count >= 3)
            {
                player.clickAlbe = false;
                GameObject[] enemyCards = new GameObject[3];
                enemyCards[0] = GameObject.FindWithTag("UnitCard_03");
                enemyCards[1] = GameObject.FindWithTag("UnitCard_04");
                enemyCards[2] = GameObject.FindWithTag("UnitCard_05");

                int randomIndex = UnityEngine.Random.Range(0, enemyCards.Length);

                Vector3 newPosition;

                // 각각의 위치를 지정해줍니다
                if (randomIndex == 0)
                {
                    newPosition = new Vector3(-5f, 1.65f, 0f);
                }
                else if (randomIndex == 1)
                {
                    newPosition = new Vector3(0f, 1.65f, 0f);
                }
                else
                {
                    newPosition = new Vector3(5f, 1.65f, 0f);
                }

                enemyCards[randomIndex].transform.position = newPosition;

                if (sufflebutton != null)
                {
                    Destroy(sufflebutton.gameObject);
                }

                GameObject unitcard00 = GameObject.FindWithTag("UnitCard_00");
                GameObject unitcard01 = GameObject.FindWithTag("UnitCard_01");
                GameObject unitcard02 = GameObject.FindWithTag("UnitCard_02");

                unitcard00.transform.DOMove(new Vector3(player.Select_Unit_xPositions[0], -3.7f, 0), 0.7f);
                unitcard01.transform.DOMove(new Vector3(player.Select_Unit_xPositions[1], -3.7f, 0), 0.7f);
                unitcard02.transform.DOMove(new Vector3(player.Select_Unit_xPositions[2], -3.7f, 0), 0.7f);

                GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
                foreach (GameObject card in cards)
                {
                    Destroy(card);
                }
                foreach (Card card in player.firsthands)
                {
                    if (card != null)
                    {
                        Destroy(card.gameObject);
                    }
                }
                foreach (Card card in enemyInstance.firsthands)
                {
                    if (card != null)
                    {
                        Destroy(card.gameObject);
                    }
                }
                foreach (Card card in dealerInstance.cardList)
                {
                    if (card != null)
                    {
                        Destroy(card.gameObject);
                    }
                }
                foreach (GameObject obj in uiObjects)
                {
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    GameObject unitCardObject = GameObject.FindWithTag("UnitCard_0" + (i));

                    if (unitCardObject != null)
                    {
                        UnitCard TheunitCard = unitCardObject.GetComponent<UnitCard>();
                        player.firstunitCards.Add(TheunitCard);
                    }
                }
                isRunning = false;

                // 유닛카드 생성 버튼 비활성화
                buttonObj.SetActive(false);
                StartCoroutine(FieldMake(0.7f));
                //StartCoroutine(ClickUnit(0.5f));
            }
        }
    }
    
    public void OnClickBattleButton()
    {
        Field fieldScript = FindObjectOfType<Field>();
        List<int> fieldList = fieldScript.fieldList;

        Enemy enemy = FindObjectOfType<Enemy>();

        GameObject fieldObj = GameObject.Find("Field");

        Field fieldComponent = fieldObj.GetComponent<Field>();

        player.practice = false;
        enemy.PushUnitCard(); // field로 enemyUnitCard 넣기
        battlebutton = false;
        Fight(new Field[] { fieldComponent });
        field.skipButton.SetActive(true);
    }

    public bool startfight = false;
    private void Fight(Field[] fields)
    {
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].Fight(3 * (i + 1));
            startfight = true;
        }
        
        Invoke("Reload", 19f);
    }

    public void Reload()
    {
        if (life > 0 && enemylife > 0)
        {
            PlayerPrefs.SetInt("life", life);
            PlayerPrefs.SetInt("enemylife", enemylife);
            SceneManager.LoadScene("PVE");
            option.soundManager.BGMSaveCurrentVolume();
            option.effectSoundManager.EffectSaveCurrentVolume();
            float savedPlaybackTime = PlayerPrefs.GetFloat("MusicPlaybackTime", 0f);
            //audioSource.time = savedPlaybackTime;
            option.soundManager.ingametargetAudioSource.time = savedPlaybackTime;
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    IEnumerator GameStart(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject shUI = GameObject.Find("selecthandsUI");
        GameObject uhUI = GameObject.Find("unithandsUI");
        GameObject fhUI = GameObject.Find("firsthandUI");
        GameObject bUI = GameObject.Find("BottomUI");
        GameObject tUI = GameObject.Find("TopUI");

        shUI.transform.DOMove(new Vector3(-3.22f, -1f, 0f), 0.7f);
        uhUI.transform.DOMove(new Vector3(5.69f, -1f, 0f), 0.7f);
        fhUI.transform.DOMove(new Vector3(0f, -3.57f, 0f), 0.7f);
        bUI.transform.DOMove(new Vector3(0f, -5f, 0f), 0.7f);
        tUI.transform.DOMove(new Vector3(0f, 5f, 0f), 0.7f);

        yield return new WaitForSeconds(0.5f);
        StartTimer();
        unitmakebuttonObj.SetActive(true);
        sufflebutton.SetActive(true);
        timerUI.SetActive(true);

    }
    public bool fieldStart = false;
    IEnumerator FieldMake(float delay)
    {
        player.clickAlbe = false;

        yield return new WaitForSeconds(delay);

        battlebutton = true;

        foreach (GameObject obj in objectsWithTag)
        {
            obj.SetActive(true);
        }

        //yield return new WaitForSeconds(delay);
        player.clickAlbe = true;
    }
}