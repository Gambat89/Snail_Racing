using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GambleManager : MonoBehaviour
{
    public static GambleManager instance;

    public int playerMoney = 300;   // 플레이어 돈
    public int betMoney = 0;        // 베팅한 돈

    public List<Bill> bills;        // 영수증 리스트

    public TMP_InputField betMoneyText;     // 베팅금액 입력 텍스트필드

    public Snail[] choiceSnailArray = new Snail[3];     // 선택한 달팽이 저장 배열

    // 게임 종류
    public enum GameKind { None, Win, Show, Place, Quinella, Exacta, QuinellaPlace, QuinellaTrebles, Trifecta }
    public GameKind gameKind = GameKind.None;

    // 영수증 순서 번호
    int billNum = 4;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 레이싱 종료 후 영수증 정산 함수
    /// </summary>
    public void GameDone()
    {
        // 영수증 순서 초기화
        billNum = 4;

        // 영수증 정산(당첨 시 돈++)
        foreach (Bill bill in bills)
        {
            // 활성화 된 영수증만 정산
            if (bill.gameObject.activeSelf)
            {
                bill.CalcMoney();
                bill.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 베팅 버튼 함수
    /// </summary>
    public void Bet()
    {
        // 배팅금액이 1보다 작거나 보유금액보다 크거나 영수증을 5개 뽑았으면 작동안함
        if (int.Parse(betMoneyText.text) < 1 || int.Parse(betMoneyText.text) > playerMoney || billNum == -1) return;

        // 다음에 출력할 영수증 정하기
        Bill bill = bills[billNum];
        bill.gameObject.SetActive(true);

        // 선택 게임에 따라 영수증 내용 표시
        switch (gameKind)
        {
            case GameKind.None:
                SetBillText(bill, "미선택", "");
                break;

            case GameKind.Win:
                SetBillText(bill, "단승", "|\n|\n1\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                bill.timeText.text = "X5";
                break;

            case GameKind.Show:
                SetBillText(bill, "연승", "|\n|\n1\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);

                bill.timeText.text = "X2";
                break;

            case GameKind.Place:
                SetBillText(bill, "연승", "|\n|\n1\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);

                bill.timeText.text = "X3";
                break;

            case GameKind.Quinella:
                SetBillText(bill, "복승", "|\n|\n12\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);

                bill.timeText.text = "X10";
                break;

            case GameKind.Exacta:
                SetBillText(bill, "쌍승", "|\n1\n|\n2\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);

                bill.timeText.text = "X20";
                break;

            case GameKind.QuinellaPlace:
                SetBillText(bill, "복연승", "|\n|\n12\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);

                bill.timeText.text = "X4";
                break;

            case GameKind.QuinellaTrebles:
                SetBillText(bill, "삼복승", "|\n|\n123\n|\n|");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);
                SetBillImage(bill, 3, choiceSnailArray[2]);

                bill.timeText.text = "X20";
                break;

            case GameKind.Trifecta:
                SetBillText(bill, "삼쌍승", "1\n|\n2\n|\n3");

                SetBillImage(bill, 1, choiceSnailArray[0]);
                SetBillImage(bill, 2, choiceSnailArray[1]);
                SetBillImage(bill, 3, choiceSnailArray[2]);

                bill.timeText.text = "X60";
                break;
        }

        // 다음 영수증으로 넘어감
        billNum--;

        // 현재 보유금액에서 구매금액 차감
        betMoney = int.Parse(betMoneyText.text);
        playerMoney -= betMoney;

        // 영수증에 배팅금액 표시
        bill.billMoney = betMoney;
        bill.billMoneyText.text = string.Format("{0}$", betMoney);
        betMoney = 0;

        // 게임종류 초기화
        bill.billGame = gameKind;

        // 선택 달팽이 초기화
        SetBillSnail(bill);
    }

    /// <summary>
    /// 영수증 텍스트 표시 함수
    /// </summary>
    /// <param name="bill">표시할 영수증</param>
    /// <param name="gameKind">베팅 게임 종류</param>
    /// <param name="gameCondition">베팅 게임 조건</param>
    void SetBillText(Bill bill, string gameKind, string gameCondition)
    {
        bill.gameKindText.text = gameKind;
        bill.gameConditionText.text = gameCondition;
    }

    /// <summary>
    /// 영수증 달팽이 이미지 표시 함수
    /// </summary>
    /// <param name="bill">표시할 영수증</param>
    /// <param name="imageNum">표시할 이미지 순서</param>
    /// <param name="choiceSnail">선택한 달팽이</param>
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
    /// 선택한 달팽이 리스트에 저장
    /// </summary>
    /// <param name="bill">저장할 영수증</param>
    void SetBillSnail(Bill bill)
    {
        bill.choiceSnailArray[0] = this.choiceSnailArray[0];
        bill.choiceSnailArray[1] = this.choiceSnailArray[1];
        bill.choiceSnailArray[2] = this.choiceSnailArray[2];
    }
}