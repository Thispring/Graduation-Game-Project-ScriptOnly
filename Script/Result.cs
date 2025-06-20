using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI p1Text;
    public TextMeshProUGUI p2Text;

    public Manager manager;
    public Field field;

    private void Start()
    {
        // 'P1' TextMeshPro 오브젝트에 0을 출력
        p1Text.text = "0";

        // 'P2' TextMeshPro 오브젝트에 1을 출력
        p2Text.text = "0";
    }

    private void Update()
    {
        p1Text.text = field.player1WinNum.ToString();
        p2Text.text = field.player2WinNum.ToString();

        ChangeWLD();
    }
    public GameObject wldObject;

    // 스프라이트 변경 함수
    public void ChangeWLD()
    {
        // 'WLD' 자식 오브젝트의 SpriteRenderer 컴포넌트를 가져옴
        SpriteRenderer spriteRenderer = wldObject.GetComponent<SpriteRenderer>();

        // SpriteRenderer 컴포넌트가 있을 경우 스프라이트 변경
        if (spriteRenderer != null)
        {
            Sprite winSprite = Resources.Load<Sprite>($"BattleStart/win");
            Sprite loseSprite = Resources.Load<Sprite>($"BattleStart/lose");
            Sprite drawSprite = Resources.Load<Sprite>($"BattleStart/draw");

            if (winSprite != null && field.battleOutcome == 15 || field.battleOutcome == 11 || field.battleOutcome == 7)
            {
                spriteRenderer.sprite = winSprite;
            }
            else if (loseSprite != null && field.battleOutcome == -1 || field.battleOutcome == -5 || field.battleOutcome == -9)
            {
                spriteRenderer.sprite = loseSprite;
            }
            else if (drawSprite != null && field.battleOutcome == 3)
            {
                spriteRenderer.sprite = drawSprite;
            }
        }
        else
        {
            Debug.LogWarning("SpriteRenderer 컴포넌트를 찾을 수 없습니다.");
        }
    }

}
