using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MentManager : MonoBehaviour
{
    public static MentManager instance;

    List<string> startMentList = new List<string>();        // ���̽� ���� ��Ʈ ����Ʈ
    List<string> startFixedMentList = new List<string>();   // ���� ���̽� ���� ��Ʈ ����Ʈ
    List<string> arrivedMentList = new List<string>();      // ���̽� ���� ��Ʈ ����Ʈ

    int arrivedMentCount = 0;           // ���̽� ���� ��Ʈ ��

    public TextMeshProUGUI mentText;    // ��Ʈ ǥ�� �ؽ�Ʈ

    int round = 1;  // ���� ����

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartMentMaker();
        FixedStartMentMaker();
        mentText.text = string.Format("...{0}��° ���� �غ���", round);
    }

    /// <summary>
    /// ���̽� ���� ��Ʈ ���� �Լ�
    /// </summary>
    void StartMentMaker()
    {
        // ���̽� ���� ��Ʈ �ʱ�ȭ
        startMentList.Clear();

        // 40% Ȯ���� ��Ʈ �߰�
        if (GameManager.instance.Probability(40))
        {
            startMentList.Add("�����Ⱑ ��������~");

            // 60% Ȯ���� ��Ʈ �߰�
            if (GameManager.instance.Probability(60))
            {
                startMentList.Add("�̹��� ���� �� �����ðڴµ���?");

                // 30% Ȯ���� ��Ʈ �߰�
                if (GameManager.instance.Probability(30)) startMentList.Add("ũ�� �ǽø� �� ����������~ ����!");
            }
        }

        startMentList.Add(string.Format("��! {0}��° ������ ���� �����մϴ�!", round));

        // 70% Ȯ���� ��Ʈ �߰�
        if (GameManager.instance.Probability(70)) startMentList.Add("�����̵� �غ�!");
    }

    /// <summary>
    /// ���� ���̽� ���� ��Ʈ ���� �Լ�
    /// </summary>
    void FixedStartMentMaker()
    {
        startFixedMentList.Add("3");
        startFixedMentList.Add("2");
        startFixedMentList.Add("1");
        startFixedMentList.Add("���~!");
    }
    
    /// <summary>
    /// ���̽� ���� ��Ʈ ǥ�� �Լ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartMent()
    {
        // ���̽��� ���� ������ ��
        if (GameManager.instance.gameState == GameManager.GameState.Done)
        {
            // ���̽� ���� ���·� ����
            GameManager.instance.gameState = GameManager.GameState.Run;

            // ���̽� �ʱ�ȭ
            GameManager.instance.GameInit();

            // ���� ��ư ��Ȱ��ȭ
            ButtonManager.instance.ButtonControl(false);

            // ���̽� ���� ��Ʈ ǥ��
            for (int i = 0; i < startMentList.Count; i++)
            {
                mentText.text = startMentList[i];
                yield return new WaitForSecondsRealtime(2.5f);
            }

            // ���� ���̽� ���� ��Ʈ ǥ��
            for (int i = 0; i < startFixedMentList.Count; i++)
            {
                mentText.text = startFixedMentList[i];
                yield return new WaitForSecondsRealtime(1f);
            }

            // ���̽� ����
            GameManager.instance.GameStart();
        }
    }

    /// <summary>
    /// ���̽� ���� ��Ʈ ���� �Լ�
    /// </summary>
    public void ArriveMentMaker()
    {
        switch (GameManager.instance.arrivedSnails.Count)
        {
            case (1):
                ArriveMentForm(1, "�� ������ �������ϴ�! �����մϴ�!");
                break;

            case (2):
                ArriveMentForm(2, "2� ���Ѱ̴ϴ�! ���߽��ϴ�!");
                break;

            case (3):
                ArriveMentForm(3, "�׷��� �������̳׿�~ �����ϴ�!");
                break;

            case (4):
                ArriveMentForm(4, "������ �������ϴ�! ��Ÿ������~");
                break;

            case (5):
                ArriveMentForm(5, "��~ �ƽ��׿�~ ������ �� ���հ̴ϴ�!");

                FixedArrivedMentMaker();
                break;
        }
    }

    /// <summary>
    /// ���̽� ���� ��Ʈ �߰� �Լ�
    /// </summary>
    /// <param name="rank">������ ���</param>
    /// <param name="ment">�߰��� ��Ʈ</param>
    void ArriveMentForm(int rank, string ment)
    {
        // �ش� ����� ���� ��Ʈ �߰�
        arrivedMentList.Add(string.Format("{0}���� [{1}]�Դϴ�!", rank, GameManager.instance.arrivedSnails[rank-1].snailName));
        arrivedMentCount++;

        // 70% Ȯ���� �߰� ��Ʈ �߰�
        if (GameManager.instance.Probability(70)) 
        {
            arrivedMentList.Add(ment);
            arrivedMentCount++;
        }
    }

    /// <summary>
    /// ���� ���̽� ���� ��Ʈ �߰� �Լ�
    /// </summary>
    void FixedArrivedMentMaker()
    {
        arrivedMentList.Add("���� ������ �������ϴ�!");
        arrivedMentList.Add("��� ������ �����߽��ϴ�!");
        arrivedMentList.Add(string.Format("{0}��° �����̾����ϴ�! �����մϴ�!", round));

        round++;
        arrivedMentList.Add(string.Format("{0}��° ���� �غ���...", round));

        arrivedMentCount += 4;

        StartMentMaker();
    }

    /// <summary>
    /// ���̽� ���� ��Ʈ ǥ�� �Լ�
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

        // ���̽� ���� ��Ʈ �� �ʱ�ȭ
        arrivedMentCount = 0;
        arrivedMentList.Clear();

        // ���̽� ���� ���·� ����
        GameManager.instance.gameState = GameManager.GameState.Done;
        GambleManager.instance.GameDone();

        // ���� ��ư Ȱ��ȭ
        ButtonManager.instance.ButtonControl(true);
    }
}