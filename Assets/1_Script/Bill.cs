using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GambleManager;

public class Bill : MonoBehaviour
{
    public TextMeshProUGUI gameKindText;        // ���� ���� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI gameConditionText;   // ���� �¸� ���� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI billMoneyText;       // ���� �ݾ� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI timeText;            // ���� ǥ�� �ؽ�Ʈ

    public Image[] snailImage = new Image[3];   // ������ ������ ǥ�� �̹���

    public Snail[] choiceSnailArray = new Snail[3];     // ���� ������ ���� �迭

    public GameKind billGame;   // ���� ����
    public int billMoney;       // ���� �ݾ�

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
    /// ���� ��� ��� �Լ�
    /// </summary>
    /// <param name="choiceKind">������ ���� ���� ����</param>
    /// <returns></returns>
    int GameResult(GameKind choiceKind)
    {
        switch (choiceKind)
        {
            // ���� ���� ������ ��
            case GameKind.None:
                return 0;

            // �ܽ�
            case GameKind.Win:
                // �����̸� 1���� ����� 1�� �����̿� ���� ��
                if (!choiceSnailArray[0] || choiceSnailArray[1] || choiceSnailArray[2]) return 0;
                else
                {
                    if (GameManager.instance.arrivedSnails[0] == choiceSnailArray[0])
                    {
                        return 5;
                    }
                    else return 0;
                }

            // ����(3)
            case GameKind.Show:
                // �����̸� 1���� ����� 3�� �ȿ� ���� ��
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

            // ����(2)
            case GameKind.Place:
                // �����̸� 1���� ����� 2�� �ȿ� ���� ��
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

            // ����
            case GameKind.Quinella:
                // �����̸� 2���� ����� ��� 2�� �ȿ� ���� ��
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

            // �ֽ�
            case GameKind.Exacta:
                // 1, 2�� �����̸� ����� �� �����̰� 1, 2�� ������� ���� ��
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

            // ������
            case GameKind.QuinellaPlace:
                // �����̸� 2���� ����� 2���� �� 3�� �ȿ� ���� ��
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

            // �ﺹ��
            case GameKind.QuinellaTrebles:
                // �����̸� 3���� ����� 3���� �� 3�� �ȿ� ���� ��
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

            // ��ֽ�
            case GameKind.Trifecta:
                // 1, 2, 3�� �����̸� ����� �� �����̰� 1, 2, 3�� ������� ���� ��
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
    /// ���� ��� ���� �Լ�
    /// </summary>
    public void CalcMoney()
    {
        instance.playerMoney += billMoney * GameResult(billGame);
    }
}
