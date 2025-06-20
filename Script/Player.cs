using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Enemy;
using TMPro;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public readonly float[] Card_xPositions = new float[] { -7.55f, -5.69f, -3.83f, -1.96f, -0.02f, 1.93f, 3.8f, 5.69f, 7.54f }; // 패의 X 포지션
    public readonly float[] Card_yPositions = new float[] { -3.45f, -3.45f, -3.45f, -3.45f, -3.45f, -3.45f, -3.45f, -3.45f, -3.45f }; // 패의 X 포지션
    public readonly float[] Select_xPositions = new float[] { -7.24f, -5.95f, -4.66f, -3.37f, -2.08f };
    public readonly float[] Unit_xPositions = new float[] { 3.9f, 5.65f, 7.4f };
    public readonly float[] Unit_yPositions = new float[] { -1.02f, -1.02f, -1.02f };
    public readonly float[] Select_Unit_xPositions = new float[] { -2f, 0f, 2f };
    public readonly float[] Select_Unit_yPositions = new float[] { -3.7f, -3.7f, -3.7f };
    public readonly float[] Field_xPositions = new float[] { -5f, 0f, 5f };

    public List<Card> firsthands = new List<Card>(new Card[9]);
    public Dictionary<Card, int> firsthandsIndex = new Dictionary<Card, int>();
    public List<Card> selectedhands = new List<Card>(new Card[5]);
    public List<string> unithands = new List<string>();

    public static string handRank;
    public UnitCard unitCardPrefab;

    public List<UnitCard> firstunitCards = new List<UnitCard>(new UnitCard[3]);
    public List<UnitCard> selectunits = new List<UnitCard>(new UnitCard[3]);

    public GameObject clickeffect; // 이펙트
    public Card cards;

    public bool practice = true;
    public AudioClip cardAudio;
    private AudioSource audioSource;
    public Manager manager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public bool AddCard(Card card)
    {
        //Debug.Log("nomral");
        List<(int index, Card card)> cardList = new List<(int index, Card card)>();

        for (int i = 0; i < Mathf.Min(firsthands.Count, Card_xPositions.Length, Card_yPositions.Length); i++)
        {
            if (firsthands[i] == null)
            {
                card.Flip(true);
                cardList.Add((i, card));
            }
        }

        if (cardList.Count == 0)
        {
            return false;
        }

        // 인덱스를 기준으로 정렬
        cardList.Sort((a, b) => a.index.CompareTo(b.index));

        // 가장 작은 인덱스에 카드 추가
        int index = cardList[0].index;
        firsthands[index] = card;
        //card.transform.DOMove(new Vector3(Card_xPositions[index], Card_yPositions[index], 0f), 0.7f);
        card.Move(new Vector3(Card_xPositions[index], Card_yPositions[index], 0f));
        firsthandsIndex.Add(card, index);
        StartCoroutine(ClickDelay(0.5f));
        return true;
    }

    public bool SuffleAddCard(Card card)
    {
        //Debug.Log("suffle");

        List<(int index, Card card)> cardList = new List<(int index, Card card)>();

        for (int i = 0; i < Mathf.Min(manager.remainingCards, Card_xPositions.Length, Card_yPositions.Length); i++)
        {
            if (firsthands[i] == null)
            {
                card.Flip(true);
                cardList.Add((i, card));
            }
        }

        if (cardList.Count == 0)
        {
            return false;
        }

        // 인덱스를 기준으로 정렬
        cardList.Sort((a, b) => a.index.CompareTo(b.index));

        // 가장 작은 인덱스에 카드 추가
        int index = cardList[0].index;
        firsthands[index] = card;
        //card.transform.DOMove(new Vector3(Card_xPositions[index], Card_yPositions[index], 0f), 0.7f);
        card.Move(new Vector3(Card_xPositions[index], Card_yPositions[index], 0f));
        firsthandsIndex.Add(card, index);
        StartCoroutine(ClickDelay(0.5f));
        return true;
    }

    private void Update()
    {
        for (int i = 0; i < Mathf.Min(firsthands.Count, Card_xPositions.Length, Card_yPositions.Length); i++) // firsthand
        {
            if (firsthands[i] == null) continue;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 cardPosition = new Vector2(Card_xPositions[i], Card_yPositions[i]);
            float distance = Vector2.Distance(mousePosition, cardPosition);

            if (Input.GetMouseButtonDown(0) && distance < 1 && clickAlbe == true)
            {
                if (cards.isMoving)
                {
                    return;
                }
                Card card = firsthands[i];

                // selectedhands로 넣어보기 시도
                bool success = AddSelectedHands(card);
                if (success)
                {
                    firsthands[i] = null;
                    audioSource.PlayOneShot(cardAudio);
                    Instantiate(clickeffect, transform.position = new Vector3(Card_xPositions[i], Card_yPositions[i], 0), Quaternion.Euler(-90, 0, 0));
                    break;
                }

                break;
            }
        }

        // 유닛카드 처음 넣었을 때
        for (int i = 0; i < Mathf.Min(firstunitCards.Count, Select_Unit_xPositions.Length, Select_Unit_yPositions.Length); i++)
        {
            if (i >= firstunitCards.Count) continue;

            if (firstunitCards[i] == null) continue;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 cardPosition = new Vector2(Select_Unit_xPositions[i], Select_Unit_yPositions[i]);
            float distance = Vector2.Distance(mousePosition, cardPosition);

            if (practice == true && Input.GetMouseButtonDown(0) && distance < 1 && clickAlbe == true)
            {
                UnitCard unitCard = firstunitCards[i];

                bool success = AddUnitSelectedHands(unitCard);
                if (success)
                {
                    audioSource.PlayOneShot(cardAudio);
                    Instantiate(clickeffect, transform.position = new Vector3(Select_Unit_xPositions[i], Select_Unit_yPositions[i], 0), Quaternion.Euler(-90, 0, 0));
                    firstunitCards[i] = null;
                }

                break;
            }
        }
        
        for (int i = 0; i < 5; i++)
        {
            if (selectedhands[i] == null) continue;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 cardPosition = new Vector2(Select_xPositions[i], -1.47f);
            float distance = Vector2.Distance(mousePosition, cardPosition);

            if (Input.GetMouseButtonDown(0) && distance < 1 && clickAlbe == true)
            {
                if (cards.isMoving)
                {
                    return;
                }
                Card card = selectedhands[i];

                bool success = AddFirstHands(card);
                if (success)
                {
                    // 이동 이펙트
                    Instantiate(clickeffect, transform.position = new Vector3(Select_xPositions[i], -1.42f, 0), Quaternion.Euler(-90, 0, 0));
                    audioSource.PlayOneShot(cardAudio);
                    selectedhands[i] = null;
                }

                break;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (i >= selectunits.Count) continue;

            if (selectunits[i] == null) continue;

            Vector2 currentCardPosition = new Vector2(Field_xPositions[i], -1.1f);
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distanceToCurrent = Vector2.Distance(currentMousePosition, currentCardPosition);

            if (practice == true && Input.GetMouseButtonDown(0) && distanceToCurrent < 1 && clickAlbe == true)
            {
                UnitCard unitCard = selectunits[i];

                bool success = AddUnitFirstHands(unitCard);
                if (success)
                {
                    audioSource.PlayOneShot(cardAudio);
                    Instantiate(clickeffect, transform.position = new Vector3(Field_xPositions[i], -1.1f, 0), Quaternion.Euler(-90, 0, 0));
                    selectunits[i] = null;
                }

                break;

            }
        }
    }

    private bool AddUnitSelectedHands(UnitCard unitCard)
    {
        for (int i = 0; i < selectunits.Count; i++)
        {
            if (selectunits[i] != null) continue;

            selectunits[i] = unitCard;
            unitCard.transform.position = new Vector3(Field_xPositions[i], -1.1f);
            return true;
        }

        return false;   // 넣을 자리 없음
    }

    private bool AddUnitFirstHands(UnitCard unitCard)
    {
        for (int i = 0; i < firstunitCards.Count; i++)
        {
            if (firstunitCards[i] != null) continue;

            firstunitCards[i] = unitCard;
            int currentIndex = selectunits.IndexOf(unitCard);
            selectunits[currentIndex] = null;
            unitCard.transform.position = new Vector3(Select_Unit_xPositions[i], -3.7f);
            return true;
        }

        return false;   // 넣을 자리 없음
    }

    private bool AddSelectedHands(Card card)
    {
        for (int i = 0; i < selectedhands.Count; i++)
        {
            if (selectedhands[i] != null) continue;

            selectedhands[i] = card;
            card.transform.position = new Vector3(Select_xPositions[i], -1.42f);
            return true;
        }

        return false;   // 넣을 자리 없음
    }
    private bool AddFirstHands(Card card)
    {
        for (int i = 0; i < firsthands.Count; i++)
        {
            if (firsthands[i] != null) continue;

            firsthands[i] = card;
            card.transform.position = new Vector3(Card_xPositions[i], Card_yPositions[i]);
            return true;
        }

        return false;   // 넣을 자리 없음
    }
    public bool clickAlbe = false;
    IEnumerator ClickDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        clickAlbe = true;
        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("delay:"+i);
        }
    }
}