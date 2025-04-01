using UnityEngine;
using UnityEngine.UI;

public class SecondChoiceBtn : MonoBehaviour
{
    [SerializeField] int snailNum, choiceNum;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.ChoiceSnail(transform, snailNum, choiceNum));
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.DeActiveOtherMark(ButtonManager.instance.secondChoiceSnail, gameObject));
    }
}
