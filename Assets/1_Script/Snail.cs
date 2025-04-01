using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public string snailName;    // 달팽이 이름

    float goalPosition = 8f;    // 골인 지점 위치
    float startPosition = -8f;  // 시작 지점 위치
    float moveFrame = 4 * 100;  // 움직일 프레임

    bool isArrived = false;     // 도착 상태

    void Start()
    {
        SnailInit();
    }

    /// <summary>
    /// 달팽이 레이싱 출발
    /// </summary>
    public void StartRun()
    {
        StartCoroutine(Run());
    }

    /// <summary>
    /// 달팽이 시작 위치로 초기화
    /// </summary>
    public void SnailInit()
    {
        transform.position = new Vector2(startPosition, transform.position.y);
        isArrived = false;
    }

    /// <summary>
    /// 달팽이 레이싱 출발 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator Run()
    {
        float nextMove = Random.Range(-0.5f, 3f);               // 다음 이동할 랜덤 거리
        float nextPosition = transform.position.x + nextMove;   // 이동할 위치 좌표
        
        // 이동할 위치에 도착할 때 까지
        while (transform.position.x < nextPosition)
        {
            // moveFrame만큼 전진 실행
            transform.position += new Vector3(nextMove / moveFrame, 0, 0);
            yield return new WaitForSecondsRealtime(1 / moveFrame);

            // 골인 위치에 도착했을 시
            if (transform.position.x > goalPosition)
            {
                // 도착 상태로 변경
                isArrived = true;
                break;
            }
        }

        // 도착 상태일 시
        if (isArrived)
        {
            // 달팽이 위치를 골인 위치로 변경
            transform.position = new Vector3(goalPosition, transform.position.y, 0);
            // 도착 달팽이 리스트에 이 달팽이 추가
            GameManager.instance.arrivedSnails.Add(gameObject.GetComponent<Snail>());
            // 레이싱 종료 멘트 추가
            MentManager.instance.ArriveMentMaker();
            // 해당 등수 깃발 활성화
            GameManager.instance.FlagOn();

            // 첫번째로 도착했을 때 레이싱 종료 멘트 표시
            if (GameManager.instance.arrivedSnails.Count == 1)
            {
                StartCoroutine(MentManager.instance.ArrivedMent());
            }
        }
        // 미도착 상태일 시
        else 
        {
            // 다시 다음 위치로 이동
            StartCoroutine(Run()); 
        }
    }
}
