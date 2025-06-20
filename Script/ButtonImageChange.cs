using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageChange : MonoBehaviour
{
    public Player player;
    public GameObject IdleEffect;

    void Update()
    {
        //Instantiate(IdleEffect, transform.position, Quaternion.Euler(-90, 0, 90));
        if (player.selectedhands != null && player.selectedhands.Count > 0)
        {
            ImageChange(PokerChecker.GetPokerHand(player.selectedhands));
        }
        if (IdleEffect == null)
        {
            Instantiate(IdleEffect, transform.position, Quaternion.Euler(-90, 0, 0));
        }
    }

    public void ImageChange(string pokerHandRank)
    {
        pokerHandRank = PokerChecker.GetPokerHand(player.selectedhands);

        GameObject buttonObj = GameObject.Find("UnitMakeButton");
        Image image = buttonObj.GetComponent<Image>();

        switch (pokerHandRank)
        {
            // 논 카드
            case "None":
                Sprite normalbtn = Resources.Load<Sprite>("UnitCards/0");
                image.sprite = normalbtn;
                break;
            // 하이 카드    
            case "High Card: 7 High":
                Sprite hc7 = Resources.Load<Sprite>("UnitCards/High_7");
                image.sprite = hc7;
                break;
            case "High Card: 8 High":
                Sprite hc8 = Resources.Load<Sprite>("UnitCards/High_8");
                image.sprite = hc8;
                break;
            case "High Card: 9 High":
                Sprite hc9 = Resources.Load<Sprite>("UnitCards/High_9");
                image.sprite = hc9;
                break;
            case "High Card: 10 High":
                Sprite hc10 = Resources.Load<Sprite>("UnitCards/High_10");
                image.sprite = hc10;
                break;
            case "High Card: JACK High":
                Sprite hcj = Resources.Load<Sprite>("UnitCards/High_J");
                image.sprite = hcj;
                break;
            case "High Card: QUEEN High":
                Sprite hcq = Resources.Load<Sprite>("UnitCards/High_Q");
                image.sprite = hcq;
                break;
            case "High Card: KING High":
                Sprite hck = Resources.Load<Sprite>("UnitCards/High_K");
                image.sprite = hck;
                break;
            case "High Card: ACE High":
                Sprite hca = Resources.Load<Sprite>("UnitCards/High_A");
                image.sprite = hca;
                break;
            // 원 페어    
            case "One Pair: 2 High":
                Sprite op2 = Resources.Load<Sprite>("UnitCards/One_2");
                image.sprite = op2;
                break;
            case "One Pair: 3 High":
                Sprite op3 = Resources.Load<Sprite>("UnitCards/One_3");
                image.sprite = op3;
                break;
            case "One Pair: 4 High":
                Sprite op4 = Resources.Load<Sprite>("UnitCards/One_4");
                image.sprite = op4;
                break;
            case "One Pair: 5 High":
                Sprite op5 = Resources.Load<Sprite>("UnitCards/One_5");
                image.sprite = op5;
                break;
            case "One Pair: 6 High":
                Sprite op6 = Resources.Load<Sprite>("UnitCards/One_6");
                image.sprite = op6;
                break;
            case "One Pair: 7 High":
                Sprite op7 = Resources.Load<Sprite>("UnitCards/One_7");
                image.sprite = op7;
                break;
            case "One Pair: 8 High":
                Sprite op8 = Resources.Load<Sprite>("UnitCards/One_8");
                image.sprite = op8;
                break;
            case "One Pair: 9 High":
                Sprite op9 = Resources.Load<Sprite>("UnitCards/One_9");
                image.sprite = op9;
                break;
            case "One Pair: 10 High":
                Sprite op10 = Resources.Load<Sprite>("UnitCards/One_10");
                image.sprite = op10;
                break;
            case "One Pair: JACK High":
                Sprite opj = Resources.Load<Sprite>("UnitCards/One_J");
                image.sprite = opj;
                break;
            case "One Pair: QUEEN High":
                Sprite opq = Resources.Load<Sprite>("UnitCards/One_Q");
                image.sprite = opq;
                break;
            case "One Pair: KING High":
                Sprite opk = Resources.Load<Sprite>("UnitCards/One_K");
                image.sprite = opk;
                break;  
            case "One Pair: ACE High":
                Sprite opa = Resources.Load<Sprite>("UnitCards/One_A");
                image.sprite = opa;
                break;
            // 투 페어    
            case "Two Pair: 2 High":
                Sprite tp2 = Resources.Load<Sprite>("UnitCards/Two_2");
                image.sprite = tp2;
                break;
            case "Two Pair: 3 High":
                Sprite tp3 = Resources.Load<Sprite>("UnitCards/Two_3");
                image.sprite = tp3;
                break;
            case "Two Pair: 4 High":
                Sprite tp4 = Resources.Load<Sprite>("UnitCards/Two_4");
                image.sprite = tp4;
                break;
            case "Two Pair: 5 High":
                Sprite tp5 = Resources.Load<Sprite>("UnitCards/Two_5");
                image.sprite = tp5;
                break;
            case "Two Pair: 6 High":
                Sprite tp6 = Resources.Load<Sprite>("UnitCards/Two_6");
                image.sprite = tp6;
                break;
            case "Two Pair: 7 High":
                Sprite tp7 = Resources.Load<Sprite>("UnitCards/Two_7");
                image.sprite = tp7;
                break;
            case "Two Pair: 8 High":
                Sprite tp8 = Resources.Load<Sprite>("UnitCards/Two_8");
                image.sprite = tp8;
                break;
            case "Two Pair: 9 High":
                Sprite tp9 = Resources.Load<Sprite>("UnitCards/Two_9");
                image.sprite = tp9;
                break;
            case "Two Pair: 10 High":
                Sprite tp10 = Resources.Load<Sprite>("UnitCards/Two_10");
                image.sprite = tp10;
                break;
            case "Two Pair: JACK High":
                Sprite tpj = Resources.Load<Sprite>("UnitCards/Two_J");
                image.sprite = tpj;
                break;
            case "Two Pair: QUEEN High":
                Sprite tpq = Resources.Load<Sprite>("UnitCards/Two_Q");
                image.sprite = tpq;
                break;
            case "Two Pair: KING High":
                Sprite tpk = Resources.Load<Sprite>("UnitCards/Two_K");
                image.sprite = tpk;
                break;  
            case "Two Pair: ACE High":
                Sprite tpa = Resources.Load<Sprite>("UnitCards/Two_A");
                image.sprite = tpa;
                break;
            // 트리플    
            case "Three of a Kind: 2 High":
                Sprite tok2 = Resources.Load<Sprite>("UnitCards/Three_2");
                image.sprite = tok2;
                break;
            case "Three of a Kind: 3 High":
                Sprite tok3 = Resources.Load<Sprite>("UnitCards/Three_3");
                image.sprite = tok3;
                break;
            case "Three of a Kind: 4 High":
                Sprite tok4 = Resources.Load<Sprite>("UnitCards/Three_4");
                image.sprite = tok4;
                break;
            case "Three of a Kind: 5 High":
                Sprite tok5 = Resources.Load<Sprite>("UnitCards/Three_5");
                image.sprite = tok5;
                break;
            case "Three of a Kind: 6 High":
                Sprite tok6 = Resources.Load<Sprite>("UnitCards/Three_6");
                image.sprite = tok6;
                break;
            case "Three of a Kind: 7 High":
                Sprite tok7 = Resources.Load<Sprite>("UnitCards/Three_7");
                image.sprite = tok7;
                break;
            case "Three of a Kind: 8 High":
                Sprite tok8 = Resources.Load<Sprite>("UnitCards/Three_8");
                image.sprite = tok8;
                break;
            case "Three of a Kind: 9 High":
                Sprite tok9 = Resources.Load<Sprite>("UnitCards/Three_9");
                image.sprite = tok9;
                break;
            case "Three of a Kind: 10 High":
                Sprite tok10 = Resources.Load<Sprite>("UnitCards/Three_10");
                image.sprite = tok10;
                break;
            case "Three of a Kind: JACK High":
                Sprite tokj = Resources.Load<Sprite>("UnitCards/Three_J");
                image.sprite = tokj;
                break;
            case "Three of a Kind: QUEEN High":
                Sprite tokq = Resources.Load<Sprite>("UnitCards/Three_Q");
                image.sprite = tokq;
                break;
            case "Three of a Kind: KING High":
                Sprite tokk = Resources.Load<Sprite>("UnitCards/Three_K");
                image.sprite = tokk;
                break;  
            case "Three of a Kind: ACE High":
                Sprite toka = Resources.Load<Sprite>("UnitCards/Three_A");
                image.sprite = toka;
                break;  
            // 스트라이트    
            case "Straight: 5 High":
                Sprite st5 = Resources.Load<Sprite>("UnitCards/ST_5");
                image.sprite = st5;
                break;
            case "Straight: 6 High":
                Sprite st6 = Resources.Load<Sprite>("UnitCards/ST_6");
                image.sprite = st6;
                break;
            case "Straight: 7 High":
                Sprite st7 = Resources.Load<Sprite>("UnitCards/ST_7");
                image.sprite = st7;
                break;
            case "Straight: 8 High":
                Sprite st8 = Resources.Load<Sprite>("UnitCards/ST_8");
                image.sprite = st8;
                break;
            case "Straight: 9 High":
                Sprite st9 = Resources.Load<Sprite>("UnitCards/ST_9");
                image.sprite = st9;
                break;
            case "Straight: 10 High":
                Sprite st10 = Resources.Load<Sprite>("UnitCards/ST_10");
                image.sprite = st10;
                break;
            case "Straight: JACK High":
                Sprite stj = Resources.Load<Sprite>("UnitCards/ST_J");
                image.sprite = stj;
                break;
            case "Straight: QUEEN High":
                Sprite stq = Resources.Load<Sprite>("UnitCards/ST_Q");
                image.sprite = stq;
                break;
            case "Straight: KING High":
                Sprite stk = Resources.Load<Sprite>("UnitCards/ST_K");
                image.sprite = stk;
                break;
            case "Straight: ACE High":
                Sprite sta = Resources.Load<Sprite>("UnitCards/ST_A");
                image.sprite = sta;
                break;
            // 플러시    
            case "Flush: Diamonds High":
                Sprite fd = Resources.Load<Sprite>("UnitCards/Flush_D");
                image.sprite = fd;
                break;
            case "Flush: Clubs High":
                Sprite fc = Resources.Load<Sprite>("UnitCards/Flush_C");
                image.sprite = fc;
                break;
            case "Flush: Hearts High":
                Sprite fh = Resources.Load<Sprite>("UnitCards/Flush_H");
                image.sprite = fh;
                break;
            case "Flush: Spades High":
                Sprite fs = Resources.Load<Sprite>("UnitCards/Flush_S");
                image.sprite = fs;
                break;
            // 풀 하우스    
            case "Full House: 2 High":
                Sprite fh2 = Resources.Load<Sprite>("UnitCards/Full_2");
                image.sprite = fh2;
                break;
            case "Full House: 3 High":
                Sprite fh3 = Resources.Load<Sprite>("UnitCards/Full_3");
                image.sprite = fh3;
                break;
            case "Full House: 4 High":
                Sprite fh4 = Resources.Load<Sprite>("UnitCards/Full_4");
                image.sprite = fh4;
                break;
            case "Full House: 5 High":
                Sprite fh5 = Resources.Load<Sprite>("UnitCards/Full_5");
                image.sprite = fh5;
                break;
            case "Full House: 6 High":
                Sprite fh6 = Resources.Load<Sprite>("UnitCards/Full_6");
                image.sprite = fh6;
                break;
            case "Full House: 7 High":
                Sprite fh7 = Resources.Load<Sprite>("UnitCards/Full_7");
                image.sprite = fh7;
                break;
            case "Full House: 8 High":
                Sprite fh8 = Resources.Load<Sprite>("UnitCards/Full_8");
                image.sprite = fh8;
                break;
            case "Full House: 9 High":
                Sprite fh9 = Resources.Load<Sprite>("UnitCards/Full_9");
                image.sprite = fh9;
                break;
            case "Full House: 10 High":
                Sprite fh10 = Resources.Load<Sprite>("UnitCards/Full_10");
                image.sprite = fh10;
                break;
            case "Full House: JACK High":
                Sprite fhj = Resources.Load<Sprite>("UnitCards/Full_J");
                image.sprite = fhj;
                break;
            case "Full House: QUEEN High":
                Sprite fhq = Resources.Load<Sprite>("UnitCards/Full_Q");
                image.sprite = fhq;
                break;
            case "Full House: KING High":
                Sprite fhk = Resources.Load<Sprite>("UnitCards/Full_K");
                image.sprite = fhk;
                break;  
            case "Full House: ACE High":
                Sprite fha = Resources.Load<Sprite>("UnitCards/Full_A");
                image.sprite = fha;
                break;
            // 포카드    
            case "Four of a Kind: 2 High":
                Sprite fok2 = Resources.Load<Sprite>("UnitCards/Four_2");
                image.sprite = fok2;
                break;
            case "Four of a Kind: 3 High":
                Sprite fok3 = Resources.Load<Sprite>("UnitCards/Four_3");
                image.sprite = fok3;
                break;
            case "Four of a Kind: 4 High":
                Sprite fok4 = Resources.Load<Sprite>("UnitCards/Four_4");
                image.sprite = fok4;
                break;
            case "Four of a Kind: 5 High":
                Sprite fok5 = Resources.Load<Sprite>("UnitCards/Four_5");
                image.sprite = fok5;
                break;
            case "Four of a Kind: 6 High":
                Sprite fok6 = Resources.Load<Sprite>("UnitCards/Four_6");
                image.sprite = fok6;
                break;
            case "Four of a Kind: 7 High":
                Sprite fok7 = Resources.Load<Sprite>("UnitCards/Four_7");
                image.sprite = fok7;
                break;
            case "Four of a Kind: 8 High":
                Sprite fok8 = Resources.Load<Sprite>("UnitCards/Four_8");
                image.sprite = fok8;
                break;
            case "Four of a Kind: 9 High":
                Sprite fok9 = Resources.Load<Sprite>("UnitCards/Four_9");
                image.sprite = fok9;
                break;
            case "Four of a Kind: 10 High":
                Sprite fok10 = Resources.Load<Sprite>("UnitCards/Four_10");
                image.sprite = fok10;
                break;
            case "Four of a Kind: JACK High":
                Sprite fokj = Resources.Load<Sprite>("UnitCards/Four_J");
                image.sprite = fokj;
                break;
            case "Four of a Kind: QUEEN High":
                Sprite fokq = Resources.Load<Sprite>("UnitCards/Four_Q");
                image.sprite = fokq;
                break;
            case "Four of a Kind: KING High":
                Sprite fokk = Resources.Load<Sprite>("UnitCards/Four_K");
                image.sprite = fokk;
                break;  
            case "Four of a Kind: ACE High":
                Sprite foka = Resources.Load<Sprite>("UnitCards/Four_A");
                image.sprite = foka;
                break;                                                         
            default:
                break;
            // 스트라이트 플러시    
            case "Straight Flush: Diamonds Flush":
                Sprite sfd = Resources.Load<Sprite>("UnitCards/SF_D");
                image.sprite = sfd;
                break;
            case "Straight Flush: Clubs Flush":
                Sprite sfc = Resources.Load<Sprite>("UnitCards/SF_C");
                image.sprite = sfc;
                break;
            case "Straight Flush: Hearts Flush":
                Sprite sfh = Resources.Load<Sprite>("UnitCards/SF_H");
                image.sprite = sfh;
                break;
            case "Straight Flush: Spades Flush":
                Sprite sfs = Resources.Load<Sprite>("UnitCards/SF_S");
                image.sprite = sfs;
                break;
            // 마운틴    
            case "Diamonds Mountain":
                Sprite md = Resources.Load<Sprite>("UnitCards/M_D");
                image.sprite = md;
                break;
            case "Clubs Mountain":
                Sprite mc = Resources.Load<Sprite>("UnitCards/M_C");
                image.sprite = mc;
                break;
            case "Hearts Mountain":
                Sprite mh = Resources.Load<Sprite>("UnitCards/M_H");
                image.sprite = mh;
                break;
            case "Spades Mountain":
                Sprite ms = Resources.Load<Sprite>("UnitCards/M_S");
                image.sprite = ms;
                break;
            // 로티플    
            case "Diamonds Royal Straight Flush":
                Sprite rsfd = Resources.Load<Sprite>("UnitCards/RF_D");
                image.sprite = rsfd;
                break;
            case "Clubs Royal Straight Flush":
                Sprite rsfc = Resources.Load<Sprite>("UnitCards/RF_C");
                image.sprite = rsfc;
                break;
            case "Hearts Royal Straight Flush":
                Sprite rsfh = Resources.Load<Sprite>("UnitCards/RF_H");
                image.sprite = rsfh;
                break;
            case "Spades Royal Straight Flush":
                Sprite rsfs = Resources.Load<Sprite>("UnitCards/RF_S");
                image.sprite = rsfs;
                break;
        }
    }
}
