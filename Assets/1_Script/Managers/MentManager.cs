using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MentManager : MonoBehaviour
{
    public static MentManager instance;

    List<string> startMentList = new List<string>();        // 레이싱 시작 멘트 리스트
    List<string> startFixedMentList = new List<string>();   // 고정 레이싱 시작 멘트 리스트
    List<string> arrivedMentList = new List<string>();      // 레이싱 종료 멘트 리스트

    int arrivedMentCount = 0;           // 레이싱 종료 멘트 수

    public TextMeshProUGUI mentText;    // 멘트 표시 텍스트

    int round = 1;  // 현재 라운드

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartMentMaker();
        FixedStartMentMaker();
        mentText.text = string.Format("...{0}번째 게임 준비중", round);
    }

    /// <summary>
    /// 레이싱 시작 멘트 생성 함수
    /// </summary>
    void StartMentMaker()
    {
        // 레이싱 시작 멘트 초기화
        startMentList.Clear();

        // 40% 확률로 멘트 추가
        if (GameManager.instance.Probability(40))
        {
            startMentList.Add("분위기가 좋은데요~");

            // 60% 확률로 멘트 추가
            if (GameManager.instance.Probability(60))
            {
                startMentList.Add("이번엔 맞출 수 있으시겠는데요?");

                // 30% 확률로 멘트 추가
                if (GameManager.instance.Probability(30)) startMentList.Add("크게 되시면 저 잊지마세요~ 하하!");
            }
        }

        startMentList.Add(string.Format("자! {0}번째 달팽이 게임 시작합니다!", round));

        // 70% 확률로 멘트 추가
        if (GameManager.instance.Probability(70)) startMentList.Add("달팽이들 준비!");
    }

    /// <summary>
    /// 고정 레이싱 시작 멘트 생성 함수
    /// </summary>
    void FixedStartMentMaker()
    {
        startFixedMentList.Add("3");
        startFixedMentList.Add("2");
        startFixedMentList.Add("1");
        startFixedMentList.Add("출발~!");
    }
    
    /// <summary>
    /// 레이싱 시작 멘트 표시 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartMent()
    {
        // 레이싱이 끝난 상태일 시
        if (GameManager.instance.gameState == GameManager.GameState.Done)
        {
            // 레이싱 시작 상태로 변경
            GameManager.instance.gameState = GameManager.GameState.Run;

            // 레이싱 초기화
            GameManager.instance.GameInit();

            // 마권 버튼 비활성화
            ButtonManager.instance.ButtonControl(false);

            // 레이싱 시작 멘트 표시
            for (int i = 0; i < startMentList.Count; i++)
            {
                mentText.text = startMentList[i];
                yield return new WaitForSecondsRealtime(2.5f);
            }

            // 고정 레이싱 시작 멘트 표시
            for (int i = 0; i < startFixedMentList.Count; i++)
            {
                mentText.text = startFixedMentList[i];
                yield return new WaitForSecondsRealtime(1f);
            }

            // 레이싱 시작
            GameManager.instance.GameStart();
        }
    }

    /// <summary>
    /// 레이싱 종료 멘트 생성 함수
    /// </summary>
    public void ArriveMentMaker()
    {
        switch (GameManager.instance.arrivedSnails.Count)
        {
            case (1):
                ArriveMentForm(1, "아 굉장히 빨랐습니다! 축하합니다!");
                break;

            case (2):
                ArriveMentForm(2, "2등도 잘한겁니다! 잘했습니다!");
                break;

            case (3):
                ArriveMentForm(3, "그래도 순위권이네요~ 좋습니다!");
                break;

            case (4):
                ArriveMentForm(4, "간발의 차였습니다! 안타깝군요~");
                break;

            case (5):
                ArriveMentForm(5, "아~ 아쉽네요~ 다음엔 더 잘합겁니다!");

                FixedArrivedMentMaker();
                break;
        }
    }

    /// <summary>
    /// 레이싱 종료 멘트 추가 함수
    /// </summary>
    /// <param name="rank">달팽이 등수</param>
    /// <param name="ment">추가할 멘트</param>
    void ArriveMentForm(int rank, string ment)
    {
        // 해당 등수에 따른 멘트 추가
        arrivedMentList.Add(string.Format("{0}등은 [{1}]입니다!", rank, GameManager.instance.arrivedSnails[rank-1].snailName));
        arrivedMentCount++;

        // 70% 확률로 추가 멘트 추가
        if (GameManager.instance.Probability(70)) 
        {
            arrivedMentList.Add(ment);
            arrivedMentCount++;
        }
    }

    /// <summary>
    /// 고정 레이싱 종료 멘트 추가 함수
    /// </summary>
    void FixedArrivedMentMaker()
    {
        arrivedMentList.Add("이제 게임이 끝났습니다!");
        arrivedMentList.Add("모든 달팽이 수고했습니다!");
        arrivedMentList.Add(string.Format("{0}번째 게임이었습니다! 감사합니다!", round));

        round++;
        arrivedMentList.Add(string.Format("{0}번째 게임 준비중...", round));

        arrivedMentCount += 4;

        StartMentMaker();
    }

    /// <summary>
    /// 레이싱 종료 멘트 표시 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator ArrivedMent()
    {
        int i = 0;
        while (i < arrivedMentCount)
        {
            mentText.text = arrivedMentList[i];
            yield return new WaitForSecondsRealtime(1.2f);

            if (i+1 == arrivedMentCount)
            {
                if (GameManager.instance.arrivedSnails.Count == GameManager.instance.snails.Length) i++;
                else yield return new WaitForSecondsRealtime(0.001f);
            }
            else
            {
                i++;
            }
        }

        // 레이싱 종료 멘트 수 초기화
        arrivedMentCount = 0;
        arrivedMentList.Clear();

        // 레이싱 종료 상태로 변경
        GameManager.instance.gameState = GameManager.GameState.Done;
        GambleManager.instance.GameDone();

        // 마권 버튼 활성화
        ButtonManager.instance.ButtonControl(true);
    }
}