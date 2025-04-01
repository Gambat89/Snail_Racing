using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Snail[] snails;              // ����忡 �ִ� �����̵�
    public Sprite[] snailImages;        // ������ �̹���
    public List<Snail> arrivedSnails;   // ������ ������

    public Dictionary<Snail, int> snailNumDictionary = new Dictionary<Snail, int>();    // ������ ��ȣ Dictionary

    public GameObject[] flags;          // ����� ���
    public Sprite[] flagImages;         // ����� ��� �̹���

    public Canvas bookCanvas;           // ���� ĵ����
    public Canvas billCanvas;           // ������ ĵ����
    public Canvas phoneCanvas;          // �޴��� ĵ����

    // ���̽� ����
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
    /// ������ ��ȣ �ο� �Լ�(Dictionary �ʱ�ȭ)
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
    /// ������ ���̽� ���� �Լ�
    /// </summary>
    public void GameStart()
    {
        foreach (Snail snail in snails)
        {
            snail.StartRun();
        }
    }

    /// <summary>
    /// ���̽� �ʱ�ȭ �Լ�(������ ����ġ, ��� ��Ȱ��ȭ)
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
    /// UI Ȱ��ȭ/��Ȱ��ȭ �Լ�
    /// </summary>
    void UIActive()
    {
        // Q ������ ���� Ȱ��ȭ
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

        // W ������ ������ Ȱ��ȭ
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

        // E ������ �޴��� Ȱ��ȭ
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
    /// �����̰� ������ �� ��� ��� Ȱ��ȭ �Լ�
    /// </summary>
    public void FlagOn()
    {
         flags[snailNumDictionary[arrivedSnails[arrivedSnails.Count-1]]].GetComponent<SpriteRenderer>().sprite = flagImages[arrivedSnails.Count-1];
         flags[snailNumDictionary[arrivedSnails[arrivedSnails.Count-1]]].SetActive(true);
    }

    /// <summary>
    /// Ȯ�� �Լ�
    /// </summary>
    /// <param name="persentage">Ȯ��</param>
    /// <returns></returns>
    public bool Probability(int persentage)
    {
        int num = Random.Range(0, 100);

        if (num < persentage) return true;
        else return false;
    }
}
