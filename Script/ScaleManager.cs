using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public void ScaleUp()
    {
        // 스케일을 높이는 코드
        transform.localScale = new Vector3(3f, 3f, 3f);
    }

    public void ScaleReturn()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    /*
    [SerializeField]
    private Card card;

    private void Awake()
    {
        card = FindObjectOfType<Card>();

        if (card == null)
        {
            Debug.LogError("Card 오브젝트를 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        Debug.Log(card.scaleSignal);
        Debug.Log(card);
        if (card.scaleSignal == true)
        {
            transform.localScale = new Vector3(3, 3, 3);
            Debug.Log(card.scaleSignal);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    */
}
