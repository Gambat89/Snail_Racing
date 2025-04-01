using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GambleManager : MonoBehaviour
{
    public static GambleManager instance;

    public int playerMoney = 300;   // �÷��̾� ��
    public int betMoney = 0;        // ������ ��

    public List<Bill> bills;        // ������ ����Ʈ

    public TMP_InputField betMoneyText;     // ���ñݾ� �Է� �ؽ�Ʈ�ʵ�

    public Snail[] choiceSnailArray = new Snail[3];     // ������ ������ ���� �迭

    // ���� ����
    public enum GameKind { None, Win, Show, Place, Quinella, Exacta, QuinellaPlace, QuinellaTrebles, Trifecta }
    public GameKind gameKind = GameKind.None;

    // ������ ���� ��ȣ
    int billNum = 4;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// ���̽� ���� �� ������ ���� �Լ�
    /// </summary>
    public void GameDone()
    {
        // ������ ���� �ʱ�ȭ
        billNum = 4;

        // ������ ����(��÷ �� ��++)
        foreach (Bill bill in bills)
        {
            // Ȱ��ȭ �� �������� ����
            if (bill.gameObject.activeSelf)
            {
                bill.CalcMoney();
                bill.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ���� ��ư �Լ�
    /// </summary>
    public void Bet()
    {
        // ���ñݾ��� 1���� �۰ų� �����ݾ׺��� ũ�ų� �������� 5�� �̾����� �۵�����
        if (int.Parse(betMoneyText.text) < 1 || int.Parse(betMoneyText.text) > playerMoney || billNum == -1) return;

        // ������ ����� ������ ���ϱ�
        Bill bill = bills[billNum];
        bill.gameObject.SetActive(true);

        // ���� ���ӿ� ���� ������ ���� ǥ��
        switch (gameKind)
        {
            case GameKind.None:
                SetBillText(bill, "�̼���", "");
                break;

            case GameKind.Win:
                SetBillText(bill, "�ܽ�", "|\n|\n1\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                bill.timeText.text = "X5";
                break;

            case GameKind.Show:
                SetBillText(bill, "����", "|\n|\n1\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);

                bill.timeText.text = "X2";
                break;

            case GameKind.Place:
                SetBillText(bill, "����", "|\n|\n1\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);

                bill.timeText.text = "X3";
                break;

            case GameKind.Quinella:
                SetBillText(bill, "����", "|\n|\n12\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);

                bill.timeText.text = "X10";
                break;

            case GameKind.Exacta:
                SetBillText(bill, "�ֽ�", "|\n1\n|\n2\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);

                bill.timeText.text = "X20";
                break;

            case GameKind.QuinellaPlace:
                SetBillText(bill, "������", "|\n|\n12\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);

                bill.timeText.text = "X4";
                break;

            case GameKind.QuinellaTrebles:
                SetBillText(bill, "�ﺹ��", "|\n|\n123\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);
                SetBillImage(bill, 3, choiceSnailArray[2]);

                bill.timeText.text = "X20";
                break;

            case GameKind.Trifecta:
                SetBillText(bill, "��ֽ�", "1\n|\n2\n|\n3");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);
                SetBillImage(bill, 3, choiceSnailArray[2]);

                bill.timeText.text = "X60";
                break;
        }

        // ���� ���������� �Ѿ
        billNum--;

        // ���� �����ݾ׿��� ���űݾ� ����
        betMoney = int.Parse(betMoneyText.text);
        playerMoney -= betMoney;

        // �������� ���ñݾ� ǥ��
        bill.billMoney = betMoney;
        bill.billMoneyText.text = string.Format("{0}$", betMoney);
        betMoney = 0;

        // �������� �ʱ�ȭ
        bill.billGame = gameKind;

        // ���� ������ �ʱ�ȭ
        SetBillSnail(bill);
    }

    /// <summary>
    /// ������ �ؽ�Ʈ ǥ�� �Լ�
    /// </summary>
    /// <param name="bill">ǥ���� ������</param>
    /// <param name="gameKind">���� ���� ����</param>
    /// <param name="gameCondition">���� ���� ����</param>
    void SetBillText(Bill bill, string gameKind, string gameCondition)
    {
        bill.gameKindText.text = gameKind;
        bill.gameConditionText.text = gameCondition;
    }

    /// <summary>
    /// ������ ������ �̹��� ǥ�� �Լ�
    /// </summary>
    /// <param name="bill">ǥ���� ������</param>
    /// <param name="imageNum">ǥ���� �̹��� ����</param>
    /// <param name="choiceSnail">������ ������</param>
    void SetBillImage(Bill bill, int imageNum, Snail choiceSnail)
    {
        switch (imageNum)
        {
            case 1:
                bill.snailImage[0].gameObject.SetActive(true);
                bill.snailImage[0].sprite = GameManager.instance.snailImages[GameManager.instance.snailNumDictionary[choiceSnail]];
                break;

            case 2:
                bill.snailImage[1].gameObject.SetActive(true);
                bill.snailImage[1].sprite = GameManager.instance.snailImages[GameManager.instance.snailNumDictionary[choiceSnail]];
                break;

            case 3:
                bill.snailImage[2].gameObject.SetActive(true);
                bill.snailImage[2].sprite = GameManager.instance.snailImages[GameManager.instance.snailNumDictionary[choiceSnail]];
                break;
        }
    }

    /// <summary>
    /// ������ ������ ����Ʈ�� ����
    /// </summary>
    /// <param name="bill">������ ������</param>
    void SetBillSnail(Bill bill)
    {
        bill.choiceSnailArray[0] = this.choiceSnailArray[0];
        bill.choiceSnailArray[1] = this.choiceSnailArray[1];
        bill.choiceSnailArray[2] = this.choiceSnailArray[2];
    }
}