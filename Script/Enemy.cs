using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public readonly float[] Card_xPositions = new float[] { -6f, -4.5f, -3f, -1.5f, 0f, 1.5f, 3f, 4.5f, 6f }; // 패의 X 포지션
    public readonly float[] Card_yPositions = new float[] { -2f, -2f, -2f, -2f, -2f, -2f, -2f, -2f, -2f }; // 패의 X 포지션
    private Dictionary<Card, int> firsthandsIndex = new Dictionary<Card, int>();
    public UnitCard unitCardPrefab;
    public List<Card> firsthands = new List<Card>(new Card[9]);
    public List<Card> secondhands = new List<Card>(new Card[5]);
    public List<UnitCard> unithands = new List<UnitCard>();
    public readonly float[] FieldxPositions = new float[] { -5f, 0f, 5f };
    public Field field;

    public bool AddCard(Card card)
    {
        List<(int index, Card card)> cardList = new List<(int index, Card card)>();

        for (int i = 0; i < Mathf.Min(firsthands.Count, Card_xPositions.Length, Card_yPositions.Length); i++)
        {
            if (firsthands[i] == null)
            {
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
        firsthandsIndex.Add(card, index);

        return true;
    }

    private int i = 3;

    public void CreateUnitCard()
    {
        for (int i = 0; i < 5; i++)
        {
            secondhands[i] = firsthands[i];
            firsthands[i] = null;
        }

        string handRank = PokerChecker.GetPokerHand(secondhands);

        foreach (Card card in secondhands)
        {
            Destroy(card.gameObject);
        }

        secondhands = new List<Card>(new Card[5]);

        // 상대방 유닛카드
        UnitCard unitCard = Instantiate<UnitCard>(unitCardPrefab, new Vector3(999f, 999f, 0), Quaternion.identity);
        unitCard.Init(handRank, i);
        unithands.Add(unitCard);
        //field.AddEnemyUnitCard(unitCard);
        i++;

    }

    public void NewCreateUnitCard()
    {
        string bestHandRank = "";
        List<Card> bestHand = new List<Card>();

        // firsthands에서 5장의 카드로 가능한 모든 조합 반복
        foreach (var combination in Combinations(firsthands, 9))
        {
            List<Card> currentHand = combination.ToList();
            string currentHandRank = PokerChecker.GetPokerHand(currentHand);

            // 최초 비교를 위해 bestHandRank에 첫 번째 족보를 할당
            if (bestHandRank == "")
            {
                bestHandRank = currentHandRank;
            }
            // 현재 최고 핸드와 비교
            if (IsHandRankHigher(currentHandRank, bestHandRank, currentHand, bestHand))
            {
                Debug.Log(currentHandRank);
                bestHandRank = currentHandRank;
                bestHand = currentHand;
            }
        }

        // 최고의 핸드로 UnitCard 생성
        UnitCard unitCard = Instantiate<UnitCard>(unitCardPrefab, new Vector3(999f, 999f, 0), Quaternion.identity);

        // 최고의 핸드 카드를 secondhands에 할당
        secondhands = new List<Card>(bestHand);

        Debug.Log(bestHandRank);
        // UnitCard 초기화
        unitCard.Init(bestHandRank, i);
        unithands.Add(unitCard);

        // 최고의 핸드 카드를 firsthands에서 제거
        foreach (Card card in bestHand)
        {
            firsthands.Remove(card);
        }
        // bestHand에 있는 카드를 firsthands에서 제거하고 해당 리스트 부분은 null 처리
        foreach (Card card in firsthands.ToList())
        {
            if (bestHand.Contains(card))
            {
                firsthands.Remove(card);
                Destroy(card.gameObject);
            }
            else
            {
                // 해당 리스트 부분은 null 처리
                card.gameObject.SetActive(false);
                firsthands[firsthands.IndexOf(card)] = null;
            }
        }
        secondhands = new List<Card>(new Card[5]);

        i++;
    }


    private bool IsHandRankHigher(string currentHandRank, string bestHandRank, List<Card> currentHand, List<Card> bestHand)
    {
        // 미리 정의된 순서를 기반으로 포커 핸드 랭크를 비교합니다.
        List<string> handRanksOrder = new List<string> { "High Card", "One Pair", "Two Pair", "Three of a Kind", "Straight", "Flush", "Full House", "Four of a Kind", "Straight Flush", "Mountain", "Royal Straight Flush" };

        int currentRankIndex = handRanksOrder.IndexOf(currentHandRank);
        int bestRankIndex = handRanksOrder.IndexOf(bestHandRank);

        // 두 핸드 랭크를 비교
        if (currentRankIndex > bestRankIndex)
        {
            return true;
        }
        else if (currentRankIndex < bestRankIndex)
        {
            return false;
        }
        else
        {
            // 랭크가 같으면 상세하게 비교
            switch (currentRankIndex)
            {
                case 1: // One Pair
                    int currentPairNumber = GetPairNumber(currentHand);
                    int bestPairNumber = GetPairNumber(bestHand);
                    return currentPairNumber > bestPairNumber;

                case 2: // Two Pair
                    int currentHighPairNumber = GetHighPairNumber(currentHand);
                    int bestHighPairNumber = GetHighPairNumber(bestHand);
                    return currentHighPairNumber > bestHighPairNumber;

                case 3: // Three of a Kind
                    int currentThreeOfAKindNumber = GetThreeOfAKindNumber(currentHand);
                    int bestThreeOfAKindNumber = GetThreeOfAKindNumber(bestHand);
                    return currentThreeOfAKindNumber > bestThreeOfAKindNumber;

                case 4: // Straight
                    int currentStraightHighNumber = GetStraightHighNumber(currentHand);
                    int bestStraightHighNumber = GetStraightHighNumber(bestHand);
                    return currentStraightHighNumber > bestStraightHighNumber;

                case 5: // Flush
                    int currentFlushHighNumber = GetFlushHighNumber(currentHand);
                    int bestFlushHighNumber = GetFlushHighNumber(bestHand);
                    return currentFlushHighNumber > bestFlushHighNumber;

                case 6: // Full House
                    int currentFullHouseThreeNumber = GetFullHouseThreeNumber(currentHand);
                    int bestFullHouseThreeNumber = GetFullHouseThreeNumber(bestHand);
                    return currentFullHouseThreeNumber > bestFullHouseThreeNumber;

                case 7: // Four of a Kind
                    int currentFourOfAKindNumber = GetFourOfAKindNumber(currentHand);
                    int bestFourOfAKindNumber = GetFourOfAKindNumber(bestHand);
                    return currentFourOfAKindNumber > bestFourOfAKindNumber;

                case 8: // Straight Flush
                    int currentStraightFlushHighNumber = GetStraightFlushHighNumber(currentHand);
                    int bestStraightFlushHighNumber = GetStraightFlushHighNumber(bestHand);
                    return currentStraightFlushHighNumber > bestStraightFlushHighNumber;

                case 9: // Mountain
                    int currentMountainHighNumber = GetMountainHighNumber(currentHand);
                    int bestMountainHighNumber = GetMountainHighNumber(bestHand);
                    return currentMountainHighNumber > bestMountainHighNumber;

                case 10: // Royal Straight Flush
                         // Royal Straight Flush는 항상 가장 높음
                    return true;

                default:
                    // 기본적으로는 무조건 false를 반환하도록 설정
                    return false;
            }
        }
    }

    private int GetPairNumber(List<Card> hand)
    {
        var groups = hand.GroupBy(card => card.number).OrderByDescending(group => group.Count()).ToArray();
        return groups[0].Key;
    }

    private int GetHighPairNumber(List<Card> hand)
    {
        var groups = hand.GroupBy(card => card.number).OrderByDescending(group => group.Count()).ToArray();
        return groups[0].Key > groups[1].Key ? groups[0].Key : groups[1].Key;
    }

    private int GetThreeOfAKindNumber(List<Card> hand)
    {
        var groups = hand.GroupBy(card => card.number).OrderByDescending(group => group.Count()).ToArray();
        return groups[0].Key;
    }

    private int GetStraightHighNumber(List<Card> hand)
    {
        return hand.Max(card => card.number);
    }

    private int GetFlushHighNumber(List<Card> hand)
    {
        return hand.Max(card => card.number);
    }

    private int GetFullHouseThreeNumber(List<Card> hand)
    {
        var groups = hand.GroupBy(card => card.number).OrderByDescending(group => group.Count()).ToArray();
        return groups[0].Key;
    }

    private int GetFourOfAKindNumber(List<Card> hand)
    {
        var groups = hand.GroupBy(card => card.number).OrderByDescending(group => group.Count()).ToArray();
        return groups[0].Key;
    }

    private int GetStraightFlushHighNumber(List<Card> hand)
    {
        return hand.Max(card => card.number);
    }

    private int GetMountainHighNumber(List<Card> hand)
    {
        return hand.Max(card => card.number);
    }



    // 카드의 조합을 생성하는 도우미 메서드
    private IEnumerable<IEnumerable<T>> Combinations<T>(List<T> elements, int k)
    {
        return k == 0 ? new[] { new T[0] } :
            elements.SelectMany((e, i) =>
                Combinations(elements.Skip(i + 1).ToList(), k - 1).Select(c => (new[] { e }).Concat(c)));
    }


    public void PushUnitCard()
    {
        for (int i = 0; i < unithands.Count; i++)
        {
            UnitCard unitCard = unithands[i];
            field.fieldList.Add(unitCard.attack);
            field.skipFieldList.Add(unitCard.attack);
            unitCard.transform.position = new Vector3(FieldxPositions[i], 1.65f, 0);
        }
    }
}