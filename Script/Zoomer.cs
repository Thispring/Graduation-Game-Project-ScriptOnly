using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    private static Zoomer instance;
    private new static Renderer renderer;
    private SpriteRenderer spriteRenderer;
    public new ParticleSystem particleSystem; // 이펙트를 가져올 변수 추가


    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer = GetComponent<Renderer>();
        //particleSystem = GetComponent<ParticleSystem>();
    }

    public static void SetSprite(Sprite cardSprite)
    {
        instance.spriteRenderer.sprite = cardSprite;
    }

    public static void SetEffect(ParticleSystem particleSystemPrefab, Transform parentTransform)
    {
        if (instance != null && particleSystemPrefab != null)
        {
            // 이전 파티클 시스템이 있다면 삭제
            if (instance.particleSystem != null)
            {
                Destroy(instance.particleSystem.gameObject);
            }
            
            // 파티클 시스템을 로드하고 인스턴스화
            ParticleSystem particleSystem = Instantiate(particleSystemPrefab);

            // 파티클 시스템을 특정 오브젝트의 자식으로 설정
            particleSystem.transform.parent = instance.transform;

            // Zoomer에 파티클 시스템을 설정
            instance.particleSystem = particleSystem;
            /*
            instance.particleSystem = particleSystem;
            particleSystem = Instantiate(particleSystem);
            */

            particleSystem.Play();
            //Debug.Log("Particle System: " + particleSystem); 
        }
        else
        {
            Debug.LogError("ParticleSystem component not found on Zoomer or particleSystem is null.");
        }
    }
    
    public static void RemoveEffect()
    {
        if (instance != null && instance.particleSystem != null)
        {
            // 파티클 시스템 게임 오브젝트를 파괴하여 파티클을 제거
            Destroy(instance.particleSystem.gameObject);
        }
    }
}