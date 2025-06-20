using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Enemy;
using TMPro;


public class PokerChecker
{
    public enum HandRank
    {
        None
    }

    private static string GetCardNumberName(int number)
    {
        return number switch
        {
            1 => "2",
            2 => "3",
            3 => "4",
            4 => "5",
            5 => "6",
            6 => "7",
            7 => "8",
            8 => "9",
            9 => "10",
            10 => "JACK",
            11 => "QUEEN",
            12 => "KING",
            13 => "ACE",
            _ => throw new System.Exception("Invalid card number."),
        };
    }

    public static string GetSuitName(CardShape shape)
    {
        switch (shape)
        {
            case CardShape.Club:
                return "Clubs";
            case CardShape.Diamond:
                return "Diamonds";
            case CardShape.Heart:
                return "Hearts";
            case CardShape.Spade:
                return "Spades";
            default:
                throw new ArgumentException("Invalid card shape.");
        }
    }

    public static string GetPokerHand(List<Card> selecthands)
    {
        string handRank = "";

        if(selecthands.Count ==0)
            return HandRank.None.ToString();

        if (selecthands.Contains(null))
            return HandRank.None.ToString();

        var hand = selecthands.OrderByDescending(card => card.number).ToList();
        var groups = hand.GroupBy(card => card.number).OrderByDescending(group => group.Count()).ToArray();


        bool isFlush = hand.All(card => card.cardShape == hand[0].cardShape);
        bool isMountain = hand.Select(card => card.number).Intersect(new[] { 9, 10, 11, 12, 13 }).Count() == 5 && !isFlush;
        bool isStraight = true;

        for (int i = 0; i < hand.Count - 1; i++)
        {
            if (hand[i].number - hand[i + 1].number != 1)
            {
                isStraight = false;
                break;
            }
        }

        bool isRoyalStraightFlush = hand.OrderBy(card => card.number).Select(card => card.number).SequenceEqual(new[] { 9, 10, 11, 12, 13 }) && isFlush;

        switch (groups[0].Count())
        {
            case 4:
                handRank = $"Four of a Kind: {GetCardNumberName(groups[0].Key)} High";
                break;
            case 3 when groups.Length == 2 && groups[1].Count() == 2:
                handRank = $"Full House: {GetCardNumberName(groups[0].Key)} High";
                break;
            case 3:
                handRank = $"Three of a Kind: {GetCardNumberName(groups[0].Key)} High";
                break;
            case 2 when groups.Length == 3 && groups[1].Count() == 2 && groups[2].Count() == 1:
                var highPairCardNumber = groups[0].Key > groups[1].Key ? groups[0].Key : groups[1].Key;
                var lowPairCardNumber = groups[0].Key < groups[1].Key ? groups[0].Key : groups[1].Key;
                var highPairCardName = GetCardNumberName(highPairCardNumber);
                var TopCardNumber = groups[2].Key;
                handRank = $"Two Pair: {highPairCardName} High";
                break;
            case 2:
                var pairCardNumber = groups[0].Key;
                var pairCardName = GetCardNumberName(pairCardNumber);
                handRank = $"One Pair: {pairCardName} High";
                break;
            default:
                if (isRoyalStraightFlush)
                {
                    handRank = $"{GetSuitName(hand[0].cardShape)} Royal Straight Flush";
                }
                else if (isMountain)
                {
                    handRank = $"{GetSuitName(hand[0].cardShape)} Mountain";
                }
                else if (isFlush && isStraight)
                {
                    handRank = $"Straight Flush: {GetSuitName(hand[4].cardShape)} Flush";
                }
                else if (isFlush)
                {
                    handRank = $"Flush: {GetSuitName(hand[0].cardShape)} High";
                }
                else if (isStraight)
                {
                    handRank = $"Straight: {GetCardNumberName(hand[0].number)} High";
                }
                else
                {
                    handRank = $"High Card: {GetCardNumberName(hand[0].number)} High";
                }
                break;
        }
   
        return handRank;
    }
}
