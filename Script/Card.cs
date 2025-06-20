using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CardShape // 자료형의 자료를 만들었음
{
    Diamond = 1, Club, Heart, Spade
}

public class Card : MonoBehaviour
{
    public CardShape cardShape => shape;
    [SerializeField]
    public CardShape shape = CardShape.Diamond; // 카드 문양p

    public int cardNumber => number;
    [SerializeField]
    public int number = 1; // 카드 번호

    [SerializeField]
    public bool selection = true; // 바꿀 수 없으면 false

    public bool flip = false;

    public SpriteRenderer spriteRenderer; // sprite 변경
    public new ParticleSystem particleSystem;
    new Renderer renderer;
    public bool isMoving = false;
    public GameObject DiaEffect;
    public GameObject CloverEffect;
    public GameObject HeartEffect;
    public GameObject SpadeEffect;
    private GameObject effectInstance = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Init(CardShape shape, int number) // 카드 모양 넘버 설정 및 sprite 변경
    {
        this.shape = shape;
        this.number = number;
        this.gameObject.name = $"Card_{shape.ToString()}_{number}";

        spriteRenderer = GetComponent<SpriteRenderer>(); // 카드 sprite 설정
        string spriteName = $"{this.number}_of_{this.shape.ToString().ToLower()}s";
        Sprite newSprite = Resources.Load<Sprite>($"Cards/{spriteName}");

        // 쉐이더
        /*string materialName = $"{this.number}_of_{this.shape.ToString().ToLower()}s";
        Material newMaterial = Resources.Load<Material>($"CardShader/{materialName}");*/

        if (newSprite == null)
        {
            Debug.LogError($"Sprite '{spriteName}' not found!");
        }
        else
        {
            spriteRenderer.sprite = newSprite;
            //spriteRenderer.material = newMaterial;
        }

        Flip(false);
    }

    // 이펙트 활성화
    public void EnableEffects()
    {
        if (effectInstance != null)
        {
            effectInstance.SetActive(true);
            Debug.Log("이펙트on");
        }
    }

    // 이펙트 비활성화
    public void DisableEffects()
    {
        if (effectInstance != null)
        {
            effectInstance.SetActive(false);
            Debug.Log("이펙트off");
        }
    }

    public void Flip(bool value)
    {
        flip = value;

        string spriteName = "0";

        if (flip == true)
            spriteName = $"{this.number}_of_{this.shape.ToString().ToLower()}s";      

        Sprite newSprite = Resources.Load<Sprite>($"Cards/{spriteName}");

        spriteRenderer.sprite = newSprite;
        
    }

    /*public void Move(Vector3 destination, bool setOriginalPosition = false)
    {   
        isMoving = true;
        StartCoroutine(MoveRoutine(destination));
        if (shape == CardShape.Diamond)
        {
            Instantiate(DiaEffect, transform.position, Quaternion.Euler(-90, 0, 0));
        }
    }
    private IEnumerator MoveRoutine(Vector3 destination)
    {
        isMoving = true;

        float duration = 0.5f;
        float timeElapsed = 0f;
        Vector3 startPosition = transform.position;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, destination, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }*/

    public void Move(Vector3 destination, bool setOriginalPosition = false)
    {
        isMoving = true;

        // 이미 이펙트가 할당되어 있는 경우 생성하지 않고 기존 이펙트를 사용
        if (effectInstance == null && flip == true)
        {
            if (shape == CardShape.Diamond)
            {
                effectInstance = Instantiate(DiaEffect, transform.position, Quaternion.Euler(-90, 0, 0));
                //DiaEffect.SetActive(true);
            }
            else if (shape == CardShape.Club)
            {
                effectInstance = Instantiate(CloverEffect, transform.position, Quaternion.Euler(-90, 0, 0));
                //CloverEffect.SetActive(true);
            }
            else if (shape == CardShape.Heart)
            {
                effectInstance = Instantiate(HeartEffect, transform.position, Quaternion.Euler(-90, 0, 0));
                //HeartEffect.SetActive(true);
            }
            else if (shape == CardShape.Spade)
            {
                effectInstance = Instantiate(SpadeEffect, transform.position, Quaternion.Euler(-90, 0, 0));
                //SpadeEffect.SetActive(true);
            }

            if (effectInstance != null)
            {
                effectInstance.transform.parent = transform;
                particleSystem = effectInstance.GetComponent<ParticleSystem>();
            }
        }

        StartCoroutine(MoveRoutine(destination, effectInstance));
    }

    /*
    public void Move(Vector3 destination, bool setOriginalPosition = false)
    {
        isMoving = true;
        
        if (shape == CardShape.Diamond)
        {
            effectInstance = Instantiate(DiaEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            effectInstance.transform.parent = transform;
            particleSystem = effectInstance.GetComponent<ParticleSystem>();
        }
        if (shape == CardShape.Club)
        {
            effectInstance = Instantiate(CloverEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            effectInstance.transform.parent = transform;
            particleSystem = effectInstance.GetComponent<ParticleSystem>();
        }
        if (shape == CardShape.Heart)
        {
            effectInstance = Instantiate(HeartEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            effectInstance.transform.parent = transform;
            particleSystem = effectInstance.GetComponent<ParticleSystem>();
        }
        if (shape == CardShape.Spade)
        {
            effectInstance = Instantiate(SpadeEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            effectInstance.transform.parent = transform;
            particleSystem = effectInstance.GetComponent<ParticleSystem>();
        }

        if (effectInstance != null)
        {
            effectInstance.transform.parent = transform;
            particleSystem = effectInstance.GetComponent<ParticleSystem>();
        }

        StartCoroutine(MoveRoutine(destination, effectInstance));
    }
    */

    private IEnumerator MoveRoutine(Vector3 destination, GameObject effectInstance)
    {
        isMoving = true;

        float duration = 0.5f;
        float timeElapsed = 0f;
        Vector3 startPosition = transform.position;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, destination, timeElapsed / duration);

            if (effectInstance != null)
            {
                effectInstance.transform.position = transform.position;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }


    public IEnumerator EnmeyMoveCard(float speed, GameObject cardObject, Vector3 targetPosition)
    {
        Vector3 startPosition = cardObject.transform.position;
        float startTime = Time.time;

        while (transform.position != targetPosition)
        {
            // 이동 속도에 따라 이동
            float t = (Time.time - startTime) * speed;
            cardObject.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }
    }

    public bool scaleSignal = false;

    public void OnMouseEnter() // 마우스 올렸을 때 행동
    {
        if (flip == true)
        {
            spriteRenderer.enabled = false;
            Zoomer.SetSprite(spriteRenderer.sprite);
            Zoomer.SetEffect(particleSystem, transform);
            effectInstance.SetActive(false);
            scaleSignal = true;
            //Debug.Log(scaleSignal);
        }
        ScaleManager scaleManager = FindObjectOfType<ScaleManager>();

        if (scaleManager != null)
        {
            // 다른 스크립트의 스케일 변경 메서드 호출
            scaleManager.ScaleUp();
        }
    }

    public void OnMouseExit() // 마우스가 나갔을 때 행동
    {
        if (flip == true)
        {
            transform.localScale = new Vector3(1, 1, 1);
            spriteRenderer.enabled = true;
            Zoomer.SetSprite(null);
            Zoomer.RemoveEffect();
            particleSystem.GetComponent<Renderer>().enabled = true;
            effectInstance.SetActive(true);
            scaleSignal = false;
        }
        ScaleManager scaleManager = FindObjectOfType<ScaleManager>();

        if (scaleManager != null)
        {
            // 다른 스크립트의 스케일 변경 메서드 호출
            scaleManager.ScaleReturn();
        }
    }
}