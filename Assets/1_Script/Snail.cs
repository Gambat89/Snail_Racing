using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public string snailName;    // ������ �̸�

    float goalPosition = 8f;    // ���� ���� ��ġ
    float startPosition = -8f;  // ���� ���� ��ġ
    float moveFrame = 4 * 100;  // ������ ������

    bool isArrived = false;     // ���� ����

    void Start()
    {
        SnailInit();
    }

    /// <summary>
    /// ������ ���̽� ���
    /// </summary>
    public void StartRun()
    {
        StartCoroutine(Run());
    }

    /// <summary>
    /// ������ ���� ��ġ�� �ʱ�ȭ
    /// </summary>
    public void SnailInit()
    {
        transform.position = new Vector2(startPosition, transform.position.y);
        isArrived = false;
    }

    /// <summary>
    /// ������ ���̽� ��� �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator Run()
    {
        float nextMove = Random.Range(-0.5f, 3f);               // ���� �̵��� ���� �Ÿ�
        float nextPosition = transform.position.x + nextMove;   // �̵��� ��ġ ��ǥ
        
        // �̵��� ��ġ�� ������ �� ����
        while (transform.position.x < nextPosition)
        {
            // moveFrame��ŭ ���� ����
            transform.position += new Vector3(nextMove / moveFrame, 0, 0);
            yield return new WaitForSecondsRealtime(1 / moveFrame);

            // ���� ��ġ�� �������� ��
            if (transform.position.x > goalPosition)
            {
                // ���� ���·� ����
                isArrived = true;
                break;
            }
        }

        // ���� ������ ��
        if (isArrived)
        {
            // ������ ��ġ�� ���� ��ġ�� ����
            transform.position = new Vector3(goalPosition, transform.position.y, 0);
            // ���� ������ ����Ʈ�� �� ������ �߰�
            GameManager.instance.arrivedSnails.Add(gameObject.GetComponent<Snail>());
            // ���̽� ���� ��Ʈ �߰�
            MentManager.instance.ArriveMentMaker();
            // �ش� ��� ��� Ȱ��ȭ
            GameManager.instance.FlagOn();

            // ù��°�� �������� �� ���̽� ���� ��Ʈ ǥ��
            if (GameManager.instance.arrivedSnails.Count == 1)
            {
                StartCoroutine(MentManager.instance.ArrivedMent());
            }
        }
        // �̵��� ������ ��
        else 
        {
            // �ٽ� ���� ��ġ�� �̵�
            StartCoroutine(Run()); 
        }
    }
}
