using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Snail[] snails;              // 경기장에 있는 달팽이들
    public Sprite[] snailImages;        // 달팽이 이미지
    public List<Snail> arrivedSnails;   // 도착한 달팽이

    public Dictionary<Snail, int> snailNumDictionary = new Dictionary<Snail, int>();    // 달팽이 번호 Dictionary

    public GameObject[] flags;          // 경기장 깃발
    public Sprite[] flagImages;         // 경기장 깃발 이미지

    public Canvas bookCanvas;           // 마권 캔버스
    public Canvas billCanvas;           // 영수증 캔버스
    public Canvas phoneCanvas;          // 휴대폰 캔버스

    // 레이싱 상태
    public enum GameState { Run, Done } 
    public GameState gameState = GameState.Done;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SnailNumInit();
    }

    private void Update()
    {
        UIActive();
    }

    /// <summary>
    /// 달팽이 번호 부여 함수(Dictionary 초기화)
    /// </summary>
    void SnailNumInit()
    {
        int snailNum = 0;
        foreach (Snail snail in snails)
        {
            snailNumDictionary.Add(snail, snailNum++);
        }
    }

    /// <summary>
    /// 달팽이 레이싱 시작 함수
    /// </summary>
    public void GameStart()
    {
        foreach (Snail snail in snails)
        {
            snail.StartRun();
        }
    }

    /// <summary>
    /// 레이싱 초기화 함수(달팽이 원위치, 깃발 비활성화)
    /// </summary>
    public void GameInit()
    {
        foreach (Snail snail in snails)
        {
            snail.SnailInit();
        }

        foreach (GameObject flag in flags)
        {
            flag.SetActive(false);
        }

        arrivedSnails.Clear();
    }

    /// <summary>
    /// UI 활성화/비활성화 함수
    /// </summary>
    void UIActive()
    {
        // Q 누르면 마권 활성화
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!bookCanvas.gameObject.activeSelf)
            {
                bookCanvas.gameObject.SetActive(true);
                bookCanvas.sortingOrder = billCanvas.sortingOrder + 1;
            }
            else
            {
                bookCanvas.gameObject.SetActive(false);
                bookCanvas.sortingOrder = 0;
            }
        }

        // W 누르면 영수증 활성화
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!billCanvas.gameObject.activeSelf)
            {
                billCanvas.gameObject.SetActive(true);
                billCanvas.sortingOrder = bookCanvas.sortingOrder + 1;
            }
            else
            {
                billCanvas.gameObject.SetActive(false);
                billCanvas.sortingOrder = 0;
            }
        }

        // E 누르면 휴대폰 활성화
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!phoneCanvas.gameObject.activeSelf)
            {
                phoneCanvas.gameObject.SetActive(true);
            }
            else
            {
                phoneCanvas.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 달팽이가 도착한 후 등수 깃발 활성화 함수
    /// </summary>
    public void FlagOn()
    {
         flags[snailNumDictionary[arrivedSnails[arrivedSnails.Count-1]]].GetComponent<SpriteRenderer>().sprite = flagImages[arrivedSnails.Count-1];
         flags[snailNumDictionary[arrivedSnails[arrivedSnails.Count-1]]].SetActive(true);
    }

    /// <summary>
    /// 확률 함수
    /// </summary>
    /// <param name="persentage">확률</param>
    /// <returns></returns>
    public bool Probability(int persentage)
    {
        int num = Random.Range(0, 100);

        if (num < persentage) return true;
        else return false;
    }
}
