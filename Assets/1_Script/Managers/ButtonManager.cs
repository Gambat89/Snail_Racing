using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    public List<Sprite> checkMarkList;

    public GameObject gameKindButtonObject;     // 게임 종류 선택 버튼
    public GameObject firstChoiceSnailObject;   // 첫 번째 달팽이 선택 버튼
    public GameObject secondChoiceSnailObject;  // 두 번째 달팽이 선택 버튼
    public GameObject thirdChoiceSnailObject;   // 세 번째 달팽이 선택 버튼

    public List<Button> gameKindButtons = new List<Button>();       // 게임 종류 선택 버튼 리스트
    public List<Button> firstChoiceSnail = new List<Button>();      // 첫 번째 달팽이 선택 버튼 리스트
    public List<Button> secondChoiceSnail = new List<Button>();     // 두 번째 달팽이 선택 버튼 리스트
    public List<Button> thirdChoiceSnail = new List<Button>();      // 세 번째 달팽이 선택 버튼 리스트


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ListInit(gameKindButtonObject, gameKindButtons);
        ListInit(firstChoiceSnailObject, firstChoiceSnail);
        ListInit(secondChoiceSnailObject, secondChoiceSnail);
        ListInit(thirdChoiceSnailObject, thirdChoiceSnail);
    }

    /// <summary>
    /// Button 모아놓은 오브젝트에서 자식의 Button Component를 해당 List에 초기화
    /// </summary>
    /// <param name="buttonObject">자식으로 Button 모아놓은 오브젝트</param>
    /// <param name="buttonList">초기화 시킬 List</param>
    void ListInit(GameObject buttonObject, List<Button> buttonList)
    {
        foreach (Button button in buttonObject.transform.GetComponentsInChildren<Button>())
        {
            buttonList.Add(button);
        }
    }

    /// <summary>
    /// 마권 Button 활성화 비활성화 컨트롤 함수(레이싱 시작 시 버튼 비활성화, 끝날 시 활성화)
    /// </summary>
    /// <param name="state">바꿀 상태</param>
    public void ButtonControl(bool state)
    {
        foreach(Button button in gameKindButtons)
        {
            button.enabled = state;
        }

        foreach (Button button in firstChoiceSnail)
        {
            button.enabled = state;
        }

        foreach (Button button in secondChoiceSnail)
        {
            button.enabled = state;
        }

        foreach (Button button in thirdChoiceSnail)
        {
            button.enabled = state;
        }
    }

    /// <summary>
    /// 게임 종류 바꾸는 함수
    /// </summary>
    /// <param name="kind">바꿀 게임 종류</param>
    public void ChangeGameKind(GambleManager.GameKind kind)
    {
        GambleManager.instance.gameKind = kind;
    }

    /// <summary>
    /// 선택 Button 클릭 시 마크 표시 활성화/비활성화 함수
    /// </summary>
    /// <param name="transform"></param>
    public void CheckMark(Transform transform)
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GambleManager.instance.gameKind = GambleManager.GameKind.None;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);

            int checkIndex = Random.Range(0, checkMarkList.Count);
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = checkMarkList[checkIndex];
        }
    }

    /// <summary>
    /// 달팽이 선택/선택해제 시 체크표시 활성화/비활성화 함수
    /// </summary>
    /// <param name="transform">선택/선택해제한 Button Transform</param>
    /// <param name="snailNum">선택한 달팽이의 번호</param>
    /// <param name="choiceNum">몇번째 달팽이로 선택했는지</param>
    public void ChoiceSnail(Transform transform, int snailNum, int choiceNum)
    {
        // 게임 종류를 선택했을 시
        if (GambleManager.instance.gameKind != GambleManager.GameKind.None)
        {
            // 선택되어있는 상태일 시
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                // 체크표시 해제
                transform.GetChild(0).gameObject.SetActive(false);

                // 해당 순서 null로 교체
                switch (choiceNum)
                {
                    case 1:
                        GambleManager.instance.choiceSnailArray[0] = null;
                        break;

                    case 2:
                        GambleManager.instance.choiceSnailArray[1] = null;
                        break;

                    case 3:
                        GambleManager.instance.choiceSnailArray[2] = null;
                        break;
                }
                
            }
            // 선택되어있지 않은 상태일 시
            else
            {
                // 체크표시
                transform.GetChild(0).gameObject.SetActive(true);

                // 해당 순서 선택한 달팽이로 교체
                switch (choiceNum)
                {
                    case 1:
                        GambleManager.instance.choiceSnailArray[0] = GameManager.instance.snails[snailNum];
                        break;

                    case 2:
                        GambleManager.instance.choiceSnailArray[1] = GameManager.instance.snails[snailNum];
                        break;

                    case 3:
                        GambleManager.instance.choiceSnailArray[2] = GameManager.instance.snails[snailNum];
                        break;
                }

                // 체크표시 종류 중 하나 표시
                int checkIndex = Random.Range(0, checkMarkList.Count);
                transform.GetChild(0).gameObject.GetComponent<Image>().sprite = checkMarkList[checkIndex];
            }
        }
    }

    /// <summary>
    /// Button 선택 시 다른 Button 체크표시 비활성화
    /// </summary>
    /// <param name="buttonList">버튼 모음 리스트</param>
    /// <param name="gameObject">체크한 Button GameObject</param>
    public void DeActiveOtherMark(List<Button> buttonList, GameObject gameObject)
    {
        foreach (Button button in buttonList)
        {
            if (button != gameObject.GetComponent<Button>())
            {
                button.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}