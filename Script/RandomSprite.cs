using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // 스프라이트 이미지를 렌더링할 Sprite Renderer 컴포넌트
    public List<Sprite> spriteList; // 랜덤으로 선택할 스프라이트 이미지 목록

    private void Start()
    {
        // 게임 시작 시 스프라이트를 무작위로 설정합니다.
        ChangeSprite();
    }

    private void Update()
    {
        // 예를 들어 5초마다 스프라이트를 무작위로 변경하려면 다음과 같이 사용할 수 있습니다.
        if (Time.time % 5.0f < Time.deltaTime)
        {
            ChangeSprite();
        }
    }

    void ChangeSprite()
    {
        // 무작위 스프라이트를 선택하고 Sprite Renderer에 할당합니다.
        if (spriteList.Count > 0)
        {
            int randomIndex = Random.Range(0, spriteList.Count);
            spriteRenderer.sprite = spriteList[randomIndex];
        }
    }
}
