using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    public List<Sprite> checkMarkList;

    public GameObject gameKindButtonObject;     // ���� ���� ���� ��ư
    public GameObject firstChoiceSnailObject;   // ù ��° ������ ���� ��ư
    public GameObject secondChoiceSnailObject;  // �� ��° ������ ���� ��ư
    public GameObject thirdChoiceSnailObject;   // �� ��° ������ ���� ��ư

    public List<Button> gameKindButtons = new List<Button>();       // ���� ���� ���� ��ư ����Ʈ
    public List<Button> firstChoiceSnail = new List<Button>();      // ù ��° ������ ���� ��ư ����Ʈ
    public List<Button> secondChoiceSnail = new List<Button>();     // �� ��° ������ ���� ��ư ����Ʈ
    public List<Button> thirdChoiceSnail = new List<Button>();      // �� ��° ������ ���� ��ư ����Ʈ


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
    /// Button ��Ƴ��� ������Ʈ���� �ڽ��� Button Component�� �ش� List�� �ʱ�ȭ
    /// </summary>
    /// <param name="buttonObject">�ڽ����� Button ��Ƴ��� ������Ʈ</param>
    /// <param name="buttonList">�ʱ�ȭ ��ų List</param>
    void ListInit(GameObject buttonObject, List<Button> buttonList)
    {
        foreach (Button button in buttonObject.transform.GetComponentsInChildren<Button>())
        {
            buttonList.Add(button);
        }
    }

    /// <summary>
    /// ���� Button Ȱ��ȭ ��Ȱ��ȭ ��Ʈ�� �Լ�(���̽� ���� �� ��ư ��Ȱ��ȭ, ���� �� Ȱ��ȭ)
    /// </summary>
    /// <param name="state">�ٲ� ����</param>
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
    /// ���� ���� �ٲٴ� �Լ�
    /// </summary>
    /// <param name="kind">�ٲ� ���� ����</param>
    public void ChangeGameKind(GambleManager.GameKind kind)
    {
        GambleManager.instance.gameKind = kind;
    }

    /// <summary>
    /// ���� Button Ŭ�� �� ��ũ ǥ�� Ȱ��ȭ/��Ȱ��ȭ �Լ�
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
    /// ������ ����/�������� �� üũǥ�� Ȱ��ȭ/��Ȱ��ȭ �Լ�
    /// </summary>
    /// <param name="transform">����/���������� Button Transform</param>
    /// <param name="snailNum">������ �������� ��ȣ</param>
    /// <param name="choiceNum">���° �����̷� �����ߴ���</param>
    public void ChoiceSnail(Transform transform, int snailNum, int choiceNum)
    {
        // ���� ������ �������� ��
        if (GambleManager.instance.gameKind != GambleManager.GameKind.None)
        {
            // ���õǾ��ִ� ������ ��
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                // üũǥ�� ����
                transform.GetChild(0).gameObject.SetActive(false);

                // �ش� ���� null�� ��ü
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
            // ���õǾ����� ���� ������ ��
            else
            {
                // üũǥ��
                transform.GetChild(0).gameObject.SetActive(true);

                // �ش� ���� ������ �����̷� ��ü
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

                // üũǥ�� ���� �� �ϳ� ǥ��
                int checkIndex = Random.Range(0, checkMarkList.Count);
                transform.GetChild(0).gameObject.GetComponent<Image>().sprite = checkMarkList[checkIndex];
            }
        }
    }

    /// <summary>
    /// Button ���� �� �ٸ� Button üũǥ�� ��Ȱ��ȭ
    /// </summary>
    /// <param name="buttonList">��ư ���� ����Ʈ</param>
    /// <param name="gameObject">üũ�� Button GameObject</param>
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