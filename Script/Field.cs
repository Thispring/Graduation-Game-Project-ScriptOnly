using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class Field : MonoBehaviour
{
    // 애니메이션
    Animator anim_panel;
    Animator anim_ch;
    Animator anim_Draw01;
    Animator anim_Draw02;
    public GameObject panel;
    public GameObject battlepanel01;
    public GameObject battlepanel02;
    public GameObject battlepanelE02;
    public GameObject battlepanel03;
    public GameObject battlepaneldraw;
    public GameObject ch;
    public GameObject chD01;
    public GameObject chD02;

    Animator anim_startbk;
    Animator anim_startrd;
    public GameObject black;
    public GameObject round;

    public List<int> fieldList = new List<int>();
    public List<GameObject> objectList = new List<GameObject>();

    public UnitCard playerUnitCard;
    public UnitCard enemyUnitCard;

    private SpriteRenderer spriteRenderer;

    public Manager manager;

    public GameObject battleEffect;

    public GameObject Destroyeffect;
    public int battleOutcome = 0;
    private int BEnumber = 0;
    public float fadeValue = 1f;
    public float fadeDuration = 2f; // 서서히 페이드 아웃되는 시간 (초)
    private float fadeTimer; // 페이드 타이머

    public int player1WinNum = 0;
    public int player2WinNum = 0;
    public Player player;
    private int roundCount
    {
        get
        {
            if (battleOutcome == 0)
                return 0;
            else if (battleOutcome == 5 || battleOutcome == -3 || battleOutcome == 1)
                return 1;
            else if (battleOutcome == 10 || battleOutcome == 6 || battleOutcome == 2 || battleOutcome == -2 || battleOutcome == -6)
                return 2;
            else
                return -1;
        }
    }
    public GameObject skipButton;
    private void ReloadManager()
    {
        // Manager 클래스의 Reload 함수 실행
        manager.Reload();
    }
    public List<int> skipFieldList = new List<int>();
    public int skipPlayerValue;
    public int skipEnemyValue;
    public void battleSkip()
    {
        Invoke("ReloadManager", 5f);
        skipButton.SetActive(false);
        shouldStopFightField = true;
        battleOutcome = 0;
        player1WinNum = 0;
        player2WinNum = 0;

        if (skipFieldList[0] > skipFieldList[3])
        {
            player1WinNum += 1;
            battleOutcome += 5;
        }
        else if (skipFieldList[0] < skipFieldList[3])
        {
            player2WinNum += 1;
            battleOutcome -= 3;
        }
        else if (skipFieldList[0] == skipFieldList[3])
        {
            battleOutcome += 1;
        }

        if (skipFieldList[1] > skipFieldList[4])
        {
            player1WinNum += 1;
            battleOutcome += 5;
        }
        else if (skipFieldList[1] < skipFieldList[4])
        {
            player2WinNum += 1;
            battleOutcome -= 3;
        }
        else if (skipFieldList[1] == skipFieldList[4])
        {
            battleOutcome += 1;
        }

        if (skipFieldList[2] > skipFieldList[5])
        {
            player1WinNum += 1;
            battleOutcome += 5;
        }
        else if (skipFieldList[2] < skipFieldList[5])
        {
            player2WinNum += 1;
            battleOutcome -= 3;
        }
        else if (skipFieldList[2] == skipFieldList[5])
        {
            battleOutcome += 1;
        }

        VDD();
    }

    private void BattleResult()
    {
        if (skipFieldList[0] > skipFieldList[3])
        {
            player1WinNum += 1;
        }
        else if (skipFieldList[0] < skipFieldList[3])
        {
            player2WinNum += 1;
        }

        if (skipFieldList[1] > skipFieldList[4])
        {
            player1WinNum += 1;
        }
        else if (skipFieldList[1] < skipFieldList[4])
        {
            player2WinNum += 1;
        }

        if (skipFieldList[2] > skipFieldList[5])
        {
            player1WinNum += 1;
        }
        else if (skipFieldList[2] < skipFieldList[5])
        {
            player2WinNum += 1;
        }
    }

    private void Awake()
    {
        skipButton.SetActive(false);
        // 애니메이션
        panel = GameObject.Find("BattlePanel");
        ch = GameObject.Find("BattleCharacter_R1");
        chD01 = GameObject.Find("BattleCharacter_Draw01");
        chD02 = GameObject.Find("BattleCharacter_Draw02");

        battlepanel01 = GameObject.Find("BattlePanel_R1");
        battlepanel02 = GameObject.Find("BattlePanel_02");
        battlepanel03 = GameObject.Find("BattlePanel_03");
        battlepanelE02 = GameObject.Find("BattlePanel_02_Enem");
        battlepaneldraw = GameObject.Find("BattlePanel_Draw");

        battlepanel01.SetActive(false);
        battlepanel02.SetActive(false);
        battlepanel03.SetActive(false);
        battlepanelE02.SetActive(false);
        battlepaneldraw.SetActive(false);

        anim_panel = panel.GetComponent<Animator>();
        anim_ch = ch.GetComponent<Animator>();
        anim_Draw01 = chD01.GetComponent<Animator>();
        anim_Draw02 = chD02.GetComponent<Animator>();

        black = GameObject.Find("Black");
        round = GameObject.Find("Round");
        anim_startbk = black.GetComponent<Animator>();
        anim_startrd = round.GetComponent<Animator>();

    }

    private void StartAni()
    {
        anim_startbk.SetBool("Black", true);
        anim_startrd.SetBool("BattleStart", true);
    }

    // 애니메이션
    private void FieldOneBattle()
    {
        anim_panel.SetBool("isBattle", true);
        anim_ch.SetBool("isBattleCh_R1", true);
    }
    private void FieldTwoBattle()
    {
        anim_panel.SetBool("isBattle", true);
        anim_ch.SetBool("isBattleCh_R2", true);
    }
    private void FieldThreeBattle()
    {
        anim_panel.SetBool("isBattle", true);
        anim_ch.SetBool("isBattleCh_R3", true);
    }
    private void FieldETwoBattle()
    {
        anim_panel.SetBool("isBattle", true);
        anim_ch.SetBool("isBattleCh_R2_Enem", true);
    }
    private void FieldBattle_Draw()
    {
        anim_panel.SetBool("isBattle", true);
        anim_Draw01.SetBool("isBattleDraw", true);
        anim_Draw02.SetBool("isBattleDraw2", true);
        //Debug.Log("isBattleDraw 찾음");
    }

    IEnumerator BattleEffect()
    {
        if (fieldList[0] != 0)
        {
            //battlepanel01.SetActive(true);
            ch = GameObject.Find("BattleCharacter_R1");
        }
        else if (fieldList[0] == 0 && fieldList[1] != 0 && fieldList[1] > fieldList[4])
        {
            //battlepanel02.SetActive(true);
            ch = GameObject.Find("BattleCharacter_R2");
        }
        else if (fieldList[0] == 0 && fieldList[1] != 0 && fieldList[1] < fieldList[4])
        {
            //battlepanelE02.SetActive(true);
            ch = GameObject.Find("BattleCharacter_R2_Enem");
        }
        else if (fieldList[0] == 0 && fieldList[1] == 0 && fieldList[2] != 0)
        {
            //battlepanel03.SetActive(true);
            ch = GameObject.Find("BattleCharacter_R3");
        }
        else if (fieldList[0] == fieldList[3] && roundCount == 0)
        {
            Debug.Log("1라운드 비김");
            chD01 = GameObject.Find("BattleCharacter_Draw01");
            chD02 = GameObject.Find("BattleCharacter_Draw02");
        }
        else if (fieldList[2] == fieldList[4] && roundCount == 1)
        {
            Debug.Log("2라운드 비김");
            chD01 = GameObject.Find("BattleCharacter_Draw01");
            chD02 = GameObject.Find("BattleCharacter_Draw02");
        }
        else if (fieldList[3] == fieldList[5] && roundCount == 2)
        {
            Debug.Log("3라운드 비김");
            chD01 = GameObject.Find("BattleCharacter_Draw01");
            chD02 = GameObject.Find("BattleCharacter_Draw02");
        }


        Sprite newSprite = Resources.Load<Sprite>("UnitImage/BE" + BEnumber);
        SpriteRenderer spriteRenderer = ch.GetComponent<SpriteRenderer>();
        SpriteRenderer drawSpriteRenderer1 = chD01.GetComponent<SpriteRenderer>();
        SpriteRenderer drawSpriteRenderer2 = chD02.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;
        drawSpriteRenderer1.sprite = newSprite;
        drawSpriteRenderer2.sprite = newSprite;
        anim_ch = ch.GetComponent<Animator>();
        anim_Draw01 = chD01.GetComponent<Animator>();
        anim_Draw02 = chD02.GetComponent<Animator>();

        if (roundCount == 0)
        {
            black.SetActive(true);
            round.SetActive(true);
            Sprite newSprite2 = Resources.Load<Sprite>("BattleStart/battlestart_1");
            SpriteRenderer spriteRenderer2 = anim_startrd.GetComponent<SpriteRenderer>();
            spriteRenderer2.sprite = newSprite2;
            yield return new WaitForSeconds(0.1f);

            if (fieldList[0] != fieldList[3])
            {
                anim_startbk.SetBool("Black", true);
                anim_startrd.SetBool("BattleStart", true);
                yield return new WaitForSeconds(0.1f);
                anim_startbk.SetBool("Black", false);
                anim_startrd.SetBool("BattleStart", false);
                yield return new WaitForSeconds(1.5f);
                black.SetActive(false);
                round.SetActive(false);
                battlepanel01.SetActive(true);
                FieldOneBattle();
            }
            else if (fieldList[0] == fieldList[3])
            {
                anim_startbk.SetBool("Black", true);
                anim_startrd.SetBool("BattleStart", true);
                yield return new WaitForSeconds(0.1f);
                anim_startbk.SetBool("Black", false);
                anim_startrd.SetBool("BattleStart", false);
                yield return new WaitForSeconds(1.5f);
                black.SetActive(false);
                round.SetActive(false);
                battlepaneldraw.SetActive(true);
                FieldBattle_Draw();
                yield return new WaitForSeconds(1.3f);
            }
        }
        else if (roundCount == 1)
        {

            black.SetActive(true);
            round.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Sprite newSprite2 = Resources.Load<Sprite>("BattleStart/battlestart_2");
            SpriteRenderer spriteRenderer2 = anim_startrd.GetComponent<SpriteRenderer>();
            spriteRenderer2.sprite = newSprite2;
            yield return new WaitForSeconds(0.1f);

            if (fieldList[1] > fieldList[4])
            {
                anim_startbk.SetBool("Black", true);
                anim_startrd.SetBool("BattleStart", true);
                yield return new WaitForSeconds(0.1f);
                anim_startbk.SetBool("Black", false);
                anim_startrd.SetBool("BattleStart", false);
                yield return new WaitForSeconds(1.5f);
                black.SetActive(false);
                round.SetActive(false);
                if (fieldList[0] == 0 && fieldList[1] != 0 && fieldList[1] > fieldList[4])
                {
                    battlepanel02.SetActive(true);
                }
                FieldTwoBattle();
            }
            else if (fieldList[1] < fieldList[4])
            {
                anim_startbk.SetBool("Black", true);
                anim_startrd.SetBool("BattleStart", true);
                yield return new WaitForSeconds(0.1f);
                anim_startbk.SetBool("Black", false);
                anim_startrd.SetBool("BattleStart", false);
                yield return new WaitForSeconds(1.5f);
                black.SetActive(false);
                round.SetActive(false);
                if (fieldList[0] == 0 && fieldList[1] != 0 && fieldList[1] < fieldList[4])
                {
                    battlepanelE02.SetActive(true);
                }
                FieldETwoBattle();
            }
            else if (fieldList[1] == fieldList[4])
            {
                anim_startbk.SetBool("Black", true);
                anim_startrd.SetBool("BattleStart", true);
                yield return new WaitForSeconds(0.1f);
                anim_startbk.SetBool("Black", false);
                anim_startrd.SetBool("BattleStart", false);
                yield return new WaitForSeconds(1.5f);
                black.SetActive(false);
                round.SetActive(false);
                battlepaneldraw.SetActive(true);
                FieldBattle_Draw();
                yield return new WaitForSeconds(1.3f);
            }
        }
        else if (roundCount == 2)
        {

            black.SetActive(true);
            round.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Sprite newSprite2 = Resources.Load<Sprite>("BattleStart/battlestart_3");
            SpriteRenderer spriteRenderer2 = anim_startrd.GetComponent<SpriteRenderer>();
            spriteRenderer2.sprite = newSprite2;
            yield return new WaitForSeconds(0.1f);

            if (fieldList[0] == 0 && fieldList[1] == 0 && fieldList[2] != 0 && fieldList[2] != fieldList[5])
            {
                anim_startbk.SetBool("Black", true);
                anim_startrd.SetBool("BattleStart", true);
                yield return new WaitForSeconds(0.1f);
                anim_startbk.SetBool("Black", false);
                anim_startrd.SetBool("BattleStart", false);
                yield return new WaitForSeconds(1.5f);
                black.SetActive(false);
                round.SetActive(false);
                battlepanel03.SetActive(true);
                FieldThreeBattle();
            }
            else if (fieldList[2] == fieldList[5])
            {
                anim_startbk.SetBool("Black", true);
                anim_startrd.SetBool("BattleStart", true);
                yield return new WaitForSeconds(0.1f);
                anim_startbk.SetBool("Black", false);
                anim_startrd.SetBool("BattleStart", false);
                yield return new WaitForSeconds(1.5f);
                black.SetActive(false);
                round.SetActive(false);
                battlepaneldraw.SetActive(true);
                FieldBattle_Draw();
                yield return new WaitForSeconds(1.3f);
            }
        }

        yield return new WaitForSeconds(0.1f);
        anim_panel.SetBool("isBattle", false);

        if (roundCount == 0)
        {
            anim_ch.SetBool("isBattleCh_R1", false);
            anim_Draw01.SetBool("isBattleDraw", false);
            anim_Draw02.SetBool("isBattleDraw2", false);
        }
        else if (roundCount == 1)
        {
            if (fieldList[1] > fieldList[4])
            {
                anim_ch.SetBool("isBattleCh_R2", false);
            }
            else if (fieldList[1] < fieldList[4])
            {
                anim_ch.SetBool("isBattleCh_R2_Enem", false);
            }
            anim_Draw01.SetBool("isBattleDraw", false);
            anim_Draw02.SetBool("isBattleDraw2", false);
        }
        else if (roundCount == 2)
        {
            anim_ch.SetBool("isBattleCh_R3", false);
            anim_Draw01.SetBool("isBattleDraw", false);
            anim_Draw02.SetBool("isBattleDraw2", false);
        }

        yield return new WaitForSeconds(0.7f);
        battlepanel01.SetActive(false);
        battlepanel02.SetActive(false);
        battlepanel03.SetActive(false);
        battlepanelE02.SetActive(false);
        battlepaneldraw.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator Win(float delay)
    {
        yield return new WaitForSeconds(delay);

        int[] xPositions = { -5, 0, 5 };
        Instantiate(Destroyeffect, battleEffect.transform.position = new Vector3(xPositions[roundCount], 1.65f, 0), Quaternion.Euler(-90, 0, 0));

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag($"UnitCard_0{roundCount + 3}");
        float fadeDuration = 0.5f;

        foreach (GameObject obj in objectsWithTag)
        {
            Material material = obj.GetComponent<Renderer>().material;
            float fadeValue = 1f;

            while (fadeValue > 0f)
            {
                fadeValue -= Time.deltaTime / fadeDuration;
                fadeValue = Mathf.Clamp01(fadeValue);
                material.SetFloat("_Fade", fadeValue);
                yield return null;
            }
        }

        fieldList[roundCount] = 0;
        fieldList[roundCount + 3] = 0;
        Debug.Log("승리 애니메이션 + 라운드 카운트:" + roundCount);
        if (roundCount == 0)
        {
            //player1WinNum += 1;
            yield return new WaitForSeconds(0.3f);
            battleOutcome += 5;
            yield return new WaitForSeconds(1f);
            FightField();
        }
        else if (roundCount == 1)
        {
            //player1WinNum += 1;
            yield return new WaitForSeconds(0.3f);
            battleOutcome += 5;
            yield return new WaitForSeconds(1f);
            FightField();
        }
        else if (roundCount == 2)
        {
            //player1WinNum += 1;
            battleOutcome += 5;
            yield return new WaitForSeconds(1f);
        }

        VDD();
    }
    IEnumerator Lose(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (roundCount == 0)
        {
            yield return new WaitForSeconds(delay);
            Instantiate(Destroyeffect, battleEffect.transform.position = new Vector3(-5, -1.1f, 0), Quaternion.Euler(-90, 0, 0));
            if (objectList.Count > 0)
            {
                GameObject Object = objectList[0];
                float fadeDuration = 0.5f;
                Material material = Object.GetComponent<Renderer>().material;
                float fadeValue = 1f;

                while (fadeValue > 0f)
                {
                    fadeValue -= Time.deltaTime / fadeDuration;
                    fadeValue = Mathf.Clamp01(fadeValue);
                    material.SetFloat("_Fade", fadeValue);
                    yield return null;
                }
            }
            fieldList[0] = 0;
            fieldList[3] = 0;
            //player2WinNum += 1;
            yield return new WaitForSeconds(0.3f);
            battleOutcome -= 3;

            yield return new WaitForSeconds(1f);
            FightField();
        }
        else if (roundCount == 1)
        {
            yield return new WaitForSeconds(delay);
            Instantiate(Destroyeffect, battleEffect.transform.position = new Vector3(0, -1.1f, 0), Quaternion.Euler(-90, 0, 0));
            if (objectList.Count > 0)
            {
                GameObject Object = objectList[1];
                float fadeDuration = 0.5f;
                Material material = Object.GetComponent<Renderer>().material;
                float fadeValue = 1f;

                while (fadeValue > 0f)
                {
                    fadeValue -= Time.deltaTime / fadeDuration;
                    fadeValue = Mathf.Clamp01(fadeValue);
                    material.SetFloat("_Fade", fadeValue);
                    yield return null;
                }
            }
            fieldList[1] = 0;
            fieldList[4] = 0;
            //player2WinNum += 1;
            yield return new WaitForSeconds(0.3f);
            battleOutcome -= 3;

            yield return new WaitForSeconds(1f);
            FightField();
        }
        else if (roundCount == 2)
        {
            yield return new WaitForSeconds(delay);
            Instantiate(Destroyeffect, battleEffect.transform.position = new Vector3(5, -1.1f, 0), Quaternion.Euler(-90, 0, 0));
            if (objectList.Count > 0)
            {
                GameObject Object = objectList[2];
                float fadeDuration = 0.5f;
                Material material = Object.GetComponent<Renderer>().material;
                float fadeValue = 1f;

                while (fadeValue > 0f)
                {
                    fadeValue -= Time.deltaTime / fadeDuration;
                    fadeValue = Mathf.Clamp01(fadeValue);
                    material.SetFloat("_Fade", fadeValue);
                    yield return null;
                }
            }
            fieldList[2] = 0;
            fieldList[5] = 0;
            //player2WinNum += 1;
            yield return new WaitForSeconds(0.3f);
            battleOutcome -= 3;
            yield return new WaitForSeconds(1f);
        }
        VDD();
    }
    IEnumerator Draw(float delay)
    {
        yield return new WaitForSeconds(delay);

        fieldList[roundCount] = 0;
        fieldList[roundCount + 3] = 0;

        Debug.Log(battleOutcome);
        Debug.Log(roundCount);

        yield return new WaitForSeconds(0.3f);
        battleOutcome += 1;
        yield return new WaitForSeconds(0.3f);
        if (roundCount == 0)
            FightField();
        else if (roundCount == 1)
            FightField();
        else if (roundCount == 2)
            FightField();

        yield return new WaitForSeconds(1f);
        VDD();
    }

    public void RemovePlayerUnitCard(UnitCard unitCard)
    {
        if (!fieldList.Contains(unitCard.attack)) return;

        fieldList.Remove(unitCard.attack);
        objectList.Remove(unitCard.gameObject);
        skipFieldList.Remove(unitCard.attack);
    }

    public void AddPlayerUnitCard(UnitCard unitCard)
    {
        //playerUnitCard = unitCard;
        fieldList.Add(unitCard.attack);
        objectList.Add(unitCard.gameObject);
        skipFieldList.Add(unitCard.attack);
    }

    public void Fight(float second)
    {
        BattleResult();
        Invoke(nameof(FightField), second);
    }

    public bool Fightable()
    {
        return fieldList.Count == 3;
    }

    // 승패무 여부 체크
    public bool vdd = false;
    public void VDD()
    {
        switch (battleOutcome)
        {
            case 15:
            case 11:
            case 7:
                vdd = true;
                manager.enemylife--;
                break;
            case 3:
                vdd = true;
                break;
            case -1:
            case -5:
            case -9:
                vdd = true;
                manager.life--;
                break;
        }
    }

    public bool shouldStopFightField = false;
    public void FightField()
    {
        // 중지 조건 추가
        if (shouldStopFightField)
        {
            Debug.Log("FightField 함수가 중지되었습니다.");
            return;
        }

        //skipButton.SetActive(true);

        if (fieldList.Count < 2)
        {
            Debug.LogError("리스트가 비어있습니다.");
            return;
        }
        
        /* player 및 enemy 공격력 설정 */
        int playerValue = fieldList[roundCount];
        int enemyValue = fieldList[roundCount + 3];

        int targetValue = Mathf.Max(playerValue, enemyValue);  // 사진을 얻을 기반 값 (player, enemyVlaue 중 높은 값)
        BEnumber = GetBEnumberByTargetValue(targetValue);  // BEnumber 구하기

        // 승패 판정
        if (playerValue > enemyValue)
        {
            StartCoroutine(BattleEffect());
            StartCoroutine(Win(3f));
        }
        else if (playerValue == enemyValue && roundCount < 3) // 무승부
        {
            StartCoroutine(BattleEffect());
            StartCoroutine(Draw(3.5f));
        }
        else if (playerValue < enemyValue)
        {
            StartCoroutine(BattleEffect());
            StartCoroutine(Lose(1.5f));
        }
        
    }

    private List<BEnumberCase> BEnumberCases = new List<BEnumberCase>()
    {
        new BEnumberCase(1,    8,     new int[]{0, 1, 0}),
        new BEnumberCase(9,    21,    new int[]{2, 3, 2}),
        new BEnumberCase(22,   34,    new int[]{4, 5, 4}),
        new BEnumberCase(35,   47,    new int[]{6, 7, 6}),
        new BEnumberCase(48,   56,    new int[]{8, 9, 8}),

        new BEnumberCase(57,   57,    new int[]{10, 11, 10}),
        new BEnumberCase(58,   58,    new int[]{12, 13, 12}),
        new BEnumberCase(59,   59,    new int[]{14, 15, 14}),
        new BEnumberCase(60,   60,    new int[]{16, 17, 16}),

        new BEnumberCase(61,   73,    new int[]{18, 19, 18}),
        new BEnumberCase(74,   86,    new int[]{20, 21, 20}),

        new BEnumberCase(87,   87,    new int[]{22, 23, 22}),
        new BEnumberCase(88,   88,    new int[]{24, 25, 24}),
        new BEnumberCase(89,   89,    new int[]{26, 27, 26}),
        new BEnumberCase(90,   90,    new int[]{28, 29, 28}),
        new BEnumberCase(91,   91,    new int[]{30, 31, 30}),
        new BEnumberCase(92,   92,    new int[]{32, 33, 32}),
        new BEnumberCase(93,   93,    new int[]{34, 35, 34}),
        new BEnumberCase(94,   94,    new int[]{36, 37, 36}),
        new BEnumberCase(95,   95,    new int[]{38, 39, 38}),
        new BEnumberCase(96,   96,    new int[]{40, 41, 40}),
        new BEnumberCase(97,   97,    new int[]{42, 43, 42}),
        new BEnumberCase(98,   98,    new int[]{44, 45, 44}),
    };

    private int GetBEnumberByTargetValue(int targetValue)
    {
        foreach (var c in BEnumberCases)
        {
            if (c.CheckRange(targetValue))
            {
                if (roundCount == 0 && fieldList[0] == fieldList[3] || roundCount == 1 && fieldList[1] == fieldList[4] || roundCount == 2 && fieldList[2] == fieldList[5])
                {
                    Debug.Log("roundCount:" + roundCount);
                    Debug.Log("비겼을때 사진 변경");
                    return c.BEnumbers[0]; // roundCount가 1이고 비기는 조건이면 첫 번째 숫자 반환
                }
                else
                {
                    return c.BEnumbers[roundCount];
                }
            }

        }
        return 0;
    }

    private class BEnumberCase
    {
        public int startValue;
        public int endValue;
        public int[] BEnumbers;

        public BEnumberCase(int startValue, int endValue, int[] bEnumbers)
        {
            this.startValue = startValue;
            this.endValue = endValue;
            this.BEnumbers = bEnumbers;
        }

        public bool CheckRange(int targetValue)
        {
            return startValue <= targetValue && targetValue <= endValue;
        }
    }
}