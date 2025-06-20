using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dealer : MonoBehaviour
{
    public List<Card> cardList = new List<Card>();

    [SerializeField]
    public Card cardPrefab;

    [SerializeField]
    private Player player;
    [SerializeField]
    private Enemy enemy;

    public SpriteRenderer spriteRenderer; // sprite 변경
    new Renderer renderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer = GetComponent<Renderer>();

        StartCoroutine(Waiting(0.5f));
    }

    public void CreateCard()
    {
        for (int s = 1; s <= 4; s++)
        {
            for (int i = 1; i < 14; i++)
            {
                Card card = Instantiate<Card>(cardPrefab, new Vector3(0f, 10f, 0), Quaternion.identity);
                card.Init((CardShape)s, i);
                cardList.Add(card);
            }
        }
    }

    public void ShareCard()
    {
        for (int i = 0; i < 9; i++)
        {
            if (player.firsthands[i] == null)
            {
                int randomIndex = Random.Range(0, cardList.Count);
                Card card = cardList[randomIndex];
                player.AddCard(card);
                cardList.RemoveAt(randomIndex);
            }
        }
    }

    public void ReShareCard(int i)
    {
        for (int j = 0; j < i; j++)
        {
            Debug.Log(i);
            Debug.Log(j);
            if (player.firsthands[j] == null)
            {
                int randomIndex = Random.Range(0, cardList.Count);
                Card card = cardList[randomIndex];
                player.SuffleAddCard(card);
                cardList.RemoveAt(randomIndex);
            }
        }
    }

    public void EnemyReShareCard()
    {
        for (int i = 0; i < 9; i++)
        {
            if (enemy.firsthands[i] == null)
            {
                int randomIndex = Random.Range(0, cardList.Count);
                Card card = cardList[randomIndex];
                enemy.AddCard(card);
                cardList.RemoveAt(randomIndex);
            }
        }

        /*
        float probability = 0.1f; // 실행 확률 (0.0 ~ 1.0 사이의 값)
        float randomValue = Random.Range(0f, 1f);

        if (randomValue < probability)
        {
            // 실행할 함수 호출
            //EnemyFlushShare();
            Debug.Log("플러시");
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                if (enemy.firsthands[i] == null)
                {
                    int randomIndex = Random.Range(0, cardList.Count);
                    Card card = cardList[randomIndex];
                    enemy.AddCard(card);
                    cardList.RemoveAt(randomIndex);
                }
            }
        }*/
    }

    IEnumerator Waiting(float delay)
    {
        yield return new WaitForSeconds(delay);

        CreateCard();
        ShareCard();
        EnemyReShareCard();
    }

    // 밸런스를 위한 Enemy에게 좋은 카드 주기
    // Flush
    public void EnemyFlushShare()
    {
        float pro1 = 0.8f; // 실행 확률 (0.0 ~ 1.0 사이의 값)
        float pro2 = 0.7f;
        float pro3 = 0.6f;
        float randomValue = Random.Range(0f, 1f);

        List<Card> cards = new List<Card>(FindObjectsOfType<Card>());

        // 특정 조건을 만족하는 카드 찾기
        Card dia = cards.Find(card => card.cardShape == CardShape.Diamond);
        Card club = cards.Find(card => card.cardShape == CardShape.Club);
        Card heart = cards.Find(card => card.cardShape == CardShape.Heart);
        Card spade = cards.Find(card => card.cardShape == CardShape.Spade);

        if (dia != null)
        {
            Debug.Log("다이아");
            for (int i = 0; i < 9; i++)
            {
                if (enemy.firsthands[i] == null)
                {

                    Card flush = cardList[i];
                    enemy.AddCard(flush);
                    cardList.RemoveAt(i);
                }
            }
        }
        if (club != null && randomValue < pro1)
        {
            Debug.Log("클로버");
            for (int i = 0; i < 9; i++)
            {
                if (enemy.firsthands[i] == null)
                {

                    Card flush = cardList[i];
                    enemy.AddCard(flush);
                    cardList.RemoveAt(i);
                }
            }
        }
        if (heart != null && randomValue < pro2)
        {
            Debug.Log("하트");
            for (int i = 0; i < 9; i++)
            {
                if (enemy.firsthands[i] == null)
                {

                    Card flush = cardList[i];
                    enemy.AddCard(flush);
                    cardList.RemoveAt(i);
                }
            }
        }
        if (spade != null && randomValue < pro3)
        {
            Debug.Log("스페이드");
            for (int i = 0; i < 9; i++)
            {
                if (enemy.firsthands[i] == null)
                {

                    Card flush = cardList[i];
                    enemy.AddCard(flush);
                    cardList.RemoveAt(i);
                }
            }
        }
    }

    // Four of a kind
    public void EnemyFourCardShare()
    {
        List<Card> sortedCardList = new List<Card>();
        while (sortedCardList.Count < 9 && cardList.Count > 0)
        {
            int randomIndex = Random.Range(0, cardList.Count);
            Card card = cardList[randomIndex];
            sortedCardList.Add(card);
            cardList.RemoveAt(randomIndex);
        }
        sortedCardList = sortedCardList.OrderBy(card => card.cardShape).ThenBy(card => card.cardNumber).ToList();
        for (int i = 0; i < sortedCardList.Count; i++)
        {
            Card card = sortedCardList[i];
            card.selection = true;
            enemy.AddCard(card);
        }
    }

}
