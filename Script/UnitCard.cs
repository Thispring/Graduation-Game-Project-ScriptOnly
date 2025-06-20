using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitCard : MonoBehaviour
{
    public int attack;     // 유닛 공격력
    public string whatunitcard;
    private Player player;

    private Dictionary<string, UnitCardInfo> RANK_TO_UNITCARDINFO;

    private Field field;

    public bool isInField = false;
    private float moveDuration;
    private GameObject objectToMove;
    public int unitIndex; // unitIndex 필드 추가
    public SpriteRenderer spriteRenderer; // sprite 변경
    public new Renderer renderer;

    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer = GetComponent<Renderer>();

        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("Could not find Player object in scene!");
        }
        //Sprite-Lit-Default
        RANK_TO_UNITCARDINFO = new Dictionary<string, UnitCardInfo>
        {
        {"High Card: 7 High" ,      new UnitCardInfo("UnitCards/High_7", "UnitCardShader/Dissolve", 1)},
        {"High Card: 8 High" ,      new UnitCardInfo("UnitCards/High_8", "UnitCardShader/Dissolve", 2)},
        {"High Card: 9 High" ,      new UnitCardInfo("UnitCards/High_9", "UnitCardShader/Dissolve", 3)},
        {"High Card: 10 High" ,     new UnitCardInfo("UnitCards/High_10", "UnitCardShader/Dissolve", 4)},
        {"High Card: JACK High" ,   new UnitCardInfo("UnitCards/High_J", "UnitCardShader/Dissolve", 5)},
        {"High Card: QUEEN High" ,  new UnitCardInfo("UnitCards/High_Q", "UnitCardShader/Dissolve", 6)},
        {"High Card: KING High" ,   new UnitCardInfo("UnitCards/High_K", "UnitCardShader/Dissolve", 7)},
        {"High Card: ACE High" ,    new UnitCardInfo("UnitCards/High_A", "UnitCardShader/Dissolve", 8)},

        {"One Pair: 2 High" ,    new UnitCardInfo("UnitCards/One_2","UnitCardShader/Dissolve", 9)},
        {"One Pair: 3 High" ,    new UnitCardInfo("UnitCards/One_3","UnitCardShader/Dissolve", 10)},
        {"One Pair: 4 High" ,    new UnitCardInfo("UnitCards/One_4","UnitCardShader/Dissolve", 11)},
        {"One Pair: 5 High" ,    new UnitCardInfo("UnitCards/One_5","UnitCardShader/Dissolve", 12)},
        {"One Pair: 6 High" ,    new UnitCardInfo("UnitCards/One_6","UnitCardShader/Dissolve", 13)},
        {"One Pair: 7 High" ,    new UnitCardInfo("UnitCards/One_7","UnitCardShader/Dissolve", 14)},
        {"One Pair: 8 High" ,    new UnitCardInfo("UnitCards/One_8","UnitCardShader/Dissolve", 15)},
        {"One Pair: 9 High" ,    new UnitCardInfo("UnitCards/One_9","UnitCardShader/Dissolve", 16)},
        {"One Pair: 10 High" ,    new UnitCardInfo("UnitCards/One_10","UnitCardShader/Dissolve", 17)},
        {"One Pair: JACK High" ,    new UnitCardInfo("UnitCards/One_J","UnitCardShader/Dissolve", 18)},
        {"One Pair: QUEEN High" ,    new UnitCardInfo("UnitCards/One_Q","UnitCardShader/Dissolve", 19)},
        {"One Pair: KING High" ,    new UnitCardInfo("UnitCards/One_K","UnitCardShader/Dissolve", 20)},
        {"One Pair: ACE High" ,    new UnitCardInfo("UnitCards/One_A","UnitCardShader/Dissolve", 21)},

        {"Two Pair: 2 High" ,    new UnitCardInfo("UnitCards/Two_2","UnitCardShader/Dissolve", 22)},
        {"Two Pair: 3 High" ,    new UnitCardInfo("UnitCards/Two_3","UnitCardShader/Dissolve", 23)},
        {"Two Pair: 4 High" ,    new UnitCardInfo("UnitCards/Two_4","UnitCardShader/Dissolve", 24)},
        {"Two Pair: 5 High" ,    new UnitCardInfo("UnitCards/Two_5","UnitCardShader/Dissolve", 25)},
        {"Two Pair: 6 High" ,    new UnitCardInfo("UnitCards/Two_6","UnitCardShader/Dissolve", 26)},
        {"Two Pair: 7 High" ,    new UnitCardInfo("UnitCards/Two_7","UnitCardShader/Dissolve", 27)},
        {"Two Pair: 8 High" ,    new UnitCardInfo("UnitCards/Two_8","UnitCardShader/Dissolve", 28)},
        {"Two Pair: 9 High" ,    new UnitCardInfo("UnitCards/Two_9","UnitCardShader/Dissolve", 29)},
        {"Two Pair: 10 High" ,    new UnitCardInfo("UnitCards/Two_10","UnitCardShader/Dissolve", 30)},
        {"Two Pair: JACK High" ,    new UnitCardInfo("UnitCards/Two_J","UnitCardShader/Dissolve", 31)},
        {"Two Pair: QUEEN High" ,    new UnitCardInfo("UnitCards/Two_Q","UnitCardShader/Dissolve", 32)},
        {"Two Pair: KING High" ,    new UnitCardInfo("UnitCards/Two_K","UnitCardShader/Dissolve", 33)},
        {"Two Pair: ACE High" ,    new UnitCardInfo("UnitCards/Two_A","UnitCardShader/Dissolve", 34)},

        {"Three of a Kind: 2 High" ,    new UnitCardInfo("UnitCards/Three_2","UnitCardShader/Dissolve", 35)},
        {"Three of a Kind: 3 High" ,    new UnitCardInfo("UnitCards/Three_3","UnitCardShader/Dissolve", 36)},
        {"Three of a Kind: 4 High" ,    new UnitCardInfo("UnitCards/Three_4","UnitCardShader/Dissolve", 37)},
        {"Three of a Kind: 5 High" ,    new UnitCardInfo("UnitCards/Three_5","UnitCardShader/Dissolve", 38)},
        {"Three of a Kind: 6 High" ,    new UnitCardInfo("UnitCards/Three_6","UnitCardShader/Dissolve", 39)},
        {"Three of a Kind: 7 High" ,    new UnitCardInfo("UnitCards/Three_7","UnitCardShader/Dissolve", 40)},
        {"Three of a Kind: 8 High" ,    new UnitCardInfo("UnitCards/Three_8","UnitCardShader/Dissolve", 41)},
        {"Three of a Kind: 9 High" ,    new UnitCardInfo("UnitCards/Three_9","UnitCardShader/Dissolve", 42)},
        {"Three of a Kind: 10 High" ,    new UnitCardInfo("UnitCards/Three_10","UnitCardShader/Dissolve", 43)},
        {"Three of a Kind: JACK High" ,    new UnitCardInfo("UnitCards/Three_J","UnitCardShader/Dissolve", 44)},
        {"Three of a Kind: QUEEN High" ,    new UnitCardInfo("UnitCards/Three_Q","UnitCardShader/Dissolve", 45)},
        {"Three of a Kind: KING High" ,    new UnitCardInfo("UnitCards/Three_K","UnitCardShader/Dissolve", 46)},
        {"Three of a Kind: ACE High" ,    new UnitCardInfo("UnitCards/Three_A","UnitCardShader/Dissolve", 47)},

        {"Straight: 5 High" ,    new UnitCardInfo("UnitCards/ST_5","UnitCardShader/Dissolve", 48)},
        {"Straight: 6 High" ,    new UnitCardInfo("UnitCards/ST_6","UnitCardShader/Dissolve", 49)},
        {"Straight: 7 High" ,    new UnitCardInfo("UnitCards/ST_7","UnitCardShader/Dissolve", 50)},
        {"Straight: 8 High" ,    new UnitCardInfo("UnitCards/ST_8","UnitCardShader/Dissolve", 51)},
        {"Straight: 9 High" ,    new UnitCardInfo("UnitCards/ST_9","UnitCardShader/Dissolve", 52)},
        {"Straight: 10 High" ,    new UnitCardInfo("UnitCards/ST_10","UnitCardShader/Dissolve", 53)},
        {"Straight: JACK High" ,    new UnitCardInfo("UnitCards/ST_J","UnitCardShader/Dissolve", 54)},
        {"Straight: QUEEN High" ,    new UnitCardInfo("UnitCards/ST_Q","UnitCardShader/Dissolve", 55)},
        {"Straight: KING High" ,    new UnitCardInfo("UnitCards/ST_K","UnitCardShader/Dissolve", 56)},
        //{"Straight: ACE High" ,    new UnitCardInfo("UnitCards/ST_A","UnitCardShader/Dissolve", 57)},
        
        // s>h>d>c
        {"Flush: Clubs High" ,    new UnitCardInfo("UnitCards/Flush_C","UnitCardShader/Dissolve", 57)},
        {"Flush: Diamonds High" ,    new UnitCardInfo("UnitCards/Flush_D","UnitCardShader/Dissolve", 58)},
        {"Flush: Hearts High" ,    new UnitCardInfo("UnitCards/Flush_H","UnitCardShader/Dissolve", 59)},
        {"Flush: Spades High" ,    new UnitCardInfo("UnitCards/Flush_S","UnitCardShader/Dissolve", 60)},

        {"Full House: 2 High" ,    new UnitCardInfo("UnitCards/Full_2","UnitCardShader/Dissolve", 61)},
        {"Full House: 3 High" ,    new UnitCardInfo("UnitCards/Full_3","UnitCardShader/Dissolve", 62)},
        {"Full House: 4 High" ,    new UnitCardInfo("UnitCards/Full_4","UnitCardShader/Dissolve", 63)},
        {"Full House: 5 High" ,    new UnitCardInfo("UnitCards/Full_5","UnitCardShader/Dissolve", 64)},
        {"Full House: 6 High" ,    new UnitCardInfo("UnitCards/Full_6","UnitCardShader/Dissolve", 65)},
        {"Full House: 7 High" ,    new UnitCardInfo("UnitCards/Full_7","UnitCardShader/Dissolve", 66)},
        {"Full House: 8 High" ,    new UnitCardInfo("UnitCards/Full_8","UnitCardShader/Dissolve", 67)},
        {"Full House: 9 High" ,    new UnitCardInfo("UnitCards/Full_9","UnitCardShader/Dissolve", 68)},
        {"Full House: 10 High" ,    new UnitCardInfo("UnitCards/Full_10","UnitCardShader/Dissolve", 69)},
        {"Full House: JACK High" ,    new UnitCardInfo("UnitCards/Full_J","UnitCardShader/Dissolve", 70)},
        {"Full House: QUEEN High" ,    new UnitCardInfo("UnitCards/Full_Q","UnitCardShader/Dissolve", 71)},
        {"Full House: KING High" ,    new UnitCardInfo("UnitCards/Full_K","UnitCardShader/Dissolve", 72)},
        {"Full House: ACE High" ,    new UnitCardInfo("UnitCards/Full_A","UnitCardShader/Dissolve", 73)},

        {"Four of a Kind: 2 High" ,    new UnitCardInfo("UnitCards/Four_2","UnitCardShader/Dissolve", 74)},
        {"Four of a Kind: 3 High" ,    new UnitCardInfo("UnitCards/Four_3","UnitCardShader/Dissolve", 75)},
        {"Four of a Kind: 4 High" ,    new UnitCardInfo("UnitCards/Four_4","UnitCardShader/Dissolve", 76)},
        {"Four of a Kind: 5 High" ,    new UnitCardInfo("UnitCards/Four_5","UnitCardShader/Dissolve", 77)},
        {"Four of a Kind: 6 High" ,    new UnitCardInfo("UnitCards/Four_6","UnitCardShader/Dissolve", 78)},
        {"Four of a Kind: 7 High" ,    new UnitCardInfo("UnitCards/Four_7","UnitCardShader/Dissolve", 79)},
        {"Four of a Kind: 8 High" ,    new UnitCardInfo("UnitCards/Four_8","UnitCardShader/Dissolve", 80)},
        {"Four of a Kind: 9 High" ,    new UnitCardInfo("UnitCards/Four_9","UnitCardShader/Dissolve", 81)},
        {"Four of a Kind: 10 High" ,    new UnitCardInfo("UnitCards/Four_10","UnitCardShader/Dissolve", 82)},
        {"Four of a Kind: JACK High" ,    new UnitCardInfo("UnitCards/Four_J","UnitCardShader/Dissolve", 83)},
        {"Four of a Kind: QUEEN High" ,    new UnitCardInfo("UnitCards/Four_Q","UnitCardShader/Dissolve", 84)},
        {"Four of a Kind: KING High" ,    new UnitCardInfo("UnitCards/Four_K","UnitCardShader/Dissolve", 85)},
        {"Four of a Kind: ACE High" ,    new UnitCardInfo("UnitCards/Four_A","UnitCardShader/Dissolve", 86)},

        {"Straight Flush: Clubs Flush" ,    new UnitCardInfo("UnitCards/SF_C","UnitCardShader/Dissolve", 87)},
        {"Straight Flush: Diamonds Flush" ,    new UnitCardInfo("UnitCards/SF_D","UnitCardShader/Dissolve", 88)},
        {"Straight Flush: Hearts Flush" ,    new UnitCardInfo("UnitCards/SF_H","UnitCardShader/Dissolve", 89)},
        {"Straight Flush: Spades Flush" ,    new UnitCardInfo("UnitCards/SF_S","UnitCardShader/Dissolve", 90)},

        {"Clubs Mountain" ,    new UnitCardInfo("UnitCards/M_C","UnitCardShader/Dissolve", 91)},
        {"Diamonds Mountain" ,    new UnitCardInfo("UnitCards/M_D","UnitCardShader/Dissolve", 92)},
        {"Hearts Mountain" ,    new UnitCardInfo("UnitCards/M_H","UnitCardShader/Dissolve", 93)},
        {"Spades Mountain" ,    new UnitCardInfo("UnitCards/M_S","UnitCardShader/Dissolve", 94)},

        {"Clubs Royal Straight Flush" ,    new UnitCardInfo("UnitCards/RF_C","UnitCardShader/Dissolve", 95)},
        {"Diamonds Royal Straight Flush" ,    new UnitCardInfo("UnitCards/RF_D","UnitCardShader/Dissolve", 96)},
        {"Hearts Royal Straight Flush" ,    new UnitCardInfo("UnitCards/RF_H","UnitCardShader/Dissolve", 97)},
        {"Spades Royal Straight Flush" ,    new UnitCardInfo("UnitCards/RF_S","UnitCardShader/Dissolve", 98)},

        };
    }

    private int number = 1;
    public void Init(string handRank, int number)
    {
        this.number = number;
        this.gameObject.name = $"{handRank}_0{number}";
        this.gameObject.tag = $"UnitCard_0{number}";
        attack = RANK_TO_UNITCARDINFO[handRank].attack;

        whatunitcard = handRank;

        GetComponent<SpriteRenderer>().sprite = RANK_TO_UNITCARDINFO[handRank].sprite;
        GetComponent<SpriteRenderer>().material = RANK_TO_UNITCARDINFO[handRank].material;
    }

    private struct UnitCardInfo
    {
        public Sprite sprite;
        public Material material;
        public int attack;

        public UnitCardInfo(string spritePath, string materialPath, int attack)
        {
            this.sprite = Resources.Load<Sprite>(spritePath);
            this.material = Resources.Load<Material>(materialPath);
            this.attack = attack;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) // 타겟에 들어왓을 때 리스트 추가
    {
        if (!other.TryGetComponent<Field>(out Field field)) return;

        this.field = field;
        field.AddPlayerUnitCard(this);
    }

    private void OnTriggerExit2D(Collider2D other) // 타겟에 나갔을 때 리스트 삭제
    {
        if (!other.TryGetComponent<Field>(out Field field)) return;

        this.field = field;
        field.RemovePlayerUnitCard(this);
    }

    // 확대기능
    public void OnMouseEnter() // 마우스 올렸을 때 행동
    {
        //if(isMoving) return;

        spriteRenderer.enabled = false;
        ZoomerUnit.SetSprite(spriteRenderer.sprite);
        ZoomerUnit.SetMaterial(GetMaterial());
    }

    public void OnMouseExit() // 마우스가 나갔을 때 행동
    {
        transform.localScale = new Vector3(1, 1, 1);
        spriteRenderer.enabled = true;
        ZoomerUnit.SetSprite(null);
        ZoomerUnit.SetMaterial(Resources.Load<Material>("Shader/Dissolve"));
    }
    
    public Material GetMaterial() // Material 변경
    {
        if (renderer != null)
        {
            return renderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found on Card prefab.");
            return null;
        }
    }
    
}