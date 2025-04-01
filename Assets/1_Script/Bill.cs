using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GambleManager;

public class Bill : MonoBehaviour
{
    public TextMeshProUGUI gameKindText;        // 게임 종류 표시 텍스트
    public TextMeshProUGUI gameConditionText;   // 게임 승리 조건 표시 텍스트
    public TextMeshProUGUI billMoneyText;       // 베팅 금액 표시 텍스트
    public TextMeshProUGUI timeText;            // 배율 표시 텍스트

    public Image[] snailImage = new Image[3];   // 선택한 달팽이 표시 이미지

    public Snail[] choiceSnailArray = new Snail[3];     // 선택 달팽이 저장 배열

    public GameKind billGame;   // 선택 게임
    public int billMoney;       // 베팅 금액

    private void Start()
    {
        gameKindText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        gameConditionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        billMoneyText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        timeText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        snailImage[0] = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        snailImage[1] = transform.GetChild(2).GetChild(1).GetComponent<Image>();
        snailImage[2] = transform.GetChild(2).GetChild(2).GetComponent<Image>();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// 베팅 결과 계산 함수
    /// </summary>
    /// <param name="choiceKind">선택한 베팅 게임 종류</param>
    /// <returns></returns>
    int GameResult(GameKind choiceKind)
    {
        switch (choiceKind)
        {
            // 게임 선택 안했을 시
            case GameKind.None:
                return 0;

            // 단승
            case GameKind.Win:
                // 달팽이를 1마리 골랐고 1등 달팽이와 같을 때
                if (!choiceSnailArray[0] || choiceSnailArray[1] || choiceSnailArray[2]) return 0;
                else
                {
                    if (GameManager.instance.arrivedSnails[0] == choiceSnailArray[0])
                    {
                        return 5;
                    }
                    else return 0;
                }

            // 연승(3)
            case GameKind.Show:
                // 달팽이를 1마리 골랐고 3등 안에 있을 때
                if (!choiceSnailArray[0] || choiceSnailArray[1] || choiceSnailArray[2]) return 0;
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (GameManager.instance.arrivedSnails[i] == choiceSnailArray[0])
                        {
                            return 2;
                        }
                    }
                    return 0;
                }

            // 연승(2)
            case GameKind.Place:
                // 달팽이를 1마리 골랐고 2등 안에 있을 때
                if (!choiceSnailArray[0] || choiceSnailArray[1] || choiceSnailArray[2]) return 0;
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (GameManager.instance.arrivedSnails[i] == choiceSnailArray[0])
                        {
                            return 3;
                        }
                    }
                    return 0;
                }

            // 복승
            case GameKind.Quinella:
                // 달팽이를 2마리 골랐고 모두 2등 안에 있을 때
                if (!choiceSnailArray[0] || !choiceSnailArray[1] || choiceSnailArray[2]) return 0;
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (GameManager.instance.arrivedSnails[i] == choiceSnailArray[0])
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                if (GameManager.instance.arrivedSnails[j] == choiceSnailArray[1])
                                {
                                    return 10;
                                }
                            }
                        }
                    }
                    return 0;
                }

            // 쌍승
            case GameKind.Exacta:
                // 1, 2번 달팽이를 골랐고 고른 달팽이가 1, 2등 순서대로 왔을 때
                if (!choiceSnailArray[0] || !choiceSnailArray[1] || choiceSnailArray[2]) return 0;
                else
                {
                    if (GameManager.instance.arrivedSnails[0] == choiceSnailArray[0])
                    {
                        if (GameManager.instance.arrivedSnails[1] == choiceSnailArray[1])
                        {
                            return 20;
                        }
                        else return 0;
                    }
                    else return 0;
                }

            // 복연승
            case GameKind.QuinellaPlace:
                // 달팽이를 2마리 골랐고 2마리 다 3등 안에 있을 때
                if (!choiceSnailArray[0] || !choiceSnailArray[1] || choiceSnailArray[2]) return 0;
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (GameManager.instance.arrivedSnails[i] == choiceSnailArray[0])
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (GameManager.instance.arrivedSnails[j] == choiceSnailArray[1])
                                {
                                    return 4;
                                }
                            }
                        }
                    }
                    return 0;
                }

            // 삼복승
            case GameKind.QuinellaTrebles:
                // 달팽이를 3마리 골랐고 3마리 다 3등 안에 있을 때
                if (!choiceSnailArray[0] || !choiceSnailArray[1] || !choiceSnailArray[2]) return 0;
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (GameManager.instance.arrivedSnails[i] == choiceSnailArray[0])
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (GameManager.instance.arrivedSnails[j] == choiceSnailArray[1])
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        if (GameManager.instance.arrivedSnails[k] == choiceSnailArray[3])
                                        {
                                            return 20;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return 0;
                }

            // 삼쌍승
            case GameKind.Trifecta:
                // 1, 2, 3번 달팽이를 골랐고 고른 달팽이가 1, 2, 3등 순서대로 왔을 때
                if (!choiceSnailArray[0] || !choiceSnailArray[1] || !choiceSnailArray[2]) return 0;
                else
                {
                    if (GameManager.instance.arrivedSnails[0] == choiceSnailArray[0])
                    {
                        if (GameManager.instance.arrivedSnails[1] == choiceSnailArray[1])
                        {
                            if (GameManager.instance.arrivedSnails[2] == choiceSnailArray[2])
                            {
                                return 60;
                            }
                            else return 0;
                        }
                        else return 0;
                    }
                    else return 0;
                }

            default:
                return 0;
        }
    }

    /// <summary>
    /// 베팅 결과 정산 함수
    /// </summary>
    public void CalcMoney()
    {
        instance.playerMoney += billMoney * GameResult(billGame);
    }
}
