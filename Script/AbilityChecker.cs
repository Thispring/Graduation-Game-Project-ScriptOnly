using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityChecker
{
    
    // 배치 시 발동되는 능력
    public static void SetinAbilityCheck(List<int> fieldList)
    {
        int playerValue = fieldList[0];
        int enemyValue = fieldList[1];

    }

    // 전투 시 발동되는 능력
    public static void NormalAbilityCheck(List<int> fieldList)
    {
        int playerValue = fieldList[0];
        int enemyValue = fieldList[1];

        // One Pair 카드 능력 부여, 강화(자기자신)
        if (playerValue >= 90 && playerValue <= 210 && playerValue % 10 == 0)
        {
            float probability;
            switch (playerValue)
            {
                case 90:
                    probability = 0.1f;
                    break;
                case 100:
                    probability = 0.12f;
                    break;
                case 110:
                    probability = 0.14f;
                    break;
                case 120:
                    probability = 0.16f;
                    break;
                case 130:
                    probability = 0.18f;
                    break;
                case 140:
                    probability = 0.2f;
                    break;
                case 150:
                    probability = 0.22f;
                    break;
                case 160:
                    probability = 0.24f;
                    break;
                case 170:
                    probability = 0.26f;
                    break;
                case 180:
                    probability = 0.28f;
                    break;
                case 190:
                    probability = 0.30f;
                    break;
                case 200:
                    probability = 0.32f;
                    break;
                case 210:
                    probability = 0.34f;
                    break;                                       
                default:
                    return;
            }
            if (Random.value < probability)
            {
                playerValue += 50;
                Debug.Log("강화성공");
            }
            else
            {
                Debug.Log("강화실패");
            }
        }
        if (enemyValue >= 90 && enemyValue <= 210 && enemyValue % 10 == 0)
        {
            float probability;
            switch (enemyValue)
            {
                case 90:
                    probability = 0.1f;
                    break;
                case 100:
                    probability = 0.12f;
                    break;
                case 110:
                    probability = 0.14f;
                    break;
                case 120:
                    probability = 0.16f;
                    break;
                case 130:
                    probability = 0.18f;
                    break;
                case 140:
                    probability = 0.2f;
                    break;
                case 150:
                    probability = 0.22f;
                    break;
                case 160:
                    probability = 0.24f;
                    break;
                case 170:
                    probability = 0.26f;
                    break;
                case 180:
                    probability = 0.28f;
                    break;
                case 190:
                    probability = 0.30f;
                    break;
                case 200:
                    probability = 0.32f;
                    break;
                case 210:
                    probability = 0.34f;
                    break;                                       
                default:
                    return;
            }
            if (Random.value < probability)
            {
                enemyValue += 50;
                Debug.Log("적 강화성공");
            }
            else
            {
                Debug.Log("적 강화실패");
            }
        }

        // High 카드 능력 부여, 회피
        if (playerValue >= 10 && playerValue <= 80 && playerValue % 10 == 0)
        {
            float probability;
            switch (playerValue)
            {
                case 10:
                    probability = 0.1f;
                    break;
                case 20:
                    probability = 0.14f;
                    break;
                case 30:
                    probability = 0.18f;
                    break;
                case 40:
                    probability = 0.22f;
                    break;
                case 50:
                    probability = 0.26f;
                    break;
                case 60:
                    probability = 0.3f;
                    break;
                case 70:
                    probability = 0.34f;
                    break;
                case 80:
                    probability = 0.38f;
                    break;                                    
                default:
                    return;
            }
            if (Random.value < probability)
            {
                playerValue = enemyValue;
                Debug.Log("회피성공");
            }
            else
            {
                Debug.Log("회피실패");
            }
        }
        if (enemyValue >= 10 && enemyValue <= 80 && enemyValue % 10 == 0)
        {
            float probability;
            switch (enemyValue)
            {
                case 10:
                    probability = 0.1f;
                    break;
                case 20:
                    probability = 0.14f;
                    break;
                case 30:
                    probability = 0.18f;
                    break;
                case 40:
                    probability = 0.22f;
                    break;
                case 50:
                    probability = 0.26f;
                    break;
                case 60:
                    probability = 0.3f;
                    break;
                case 70:
                    probability = 0.34f;
                    break;
                case 80:
                    probability = 0.38f;
                    break;                                    
                default:
                    return;
            }
            if (Random.value < probability)
            {
                enemyValue = playerValue;
                Debug.Log("적 회피성공");
            }
            else
            {
                Debug.Log("적 회피실패");
            }
        }
    }
}