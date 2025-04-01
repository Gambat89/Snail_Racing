using UnityEngine;
using UnityEngine.UI;

public class ThirdChoiceBtn : MonoBehaviour
{
    [SerializeField] int snailNum, choiceNum;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.ChoiceSnail(transform, snailNum, choiceNum));
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.DeActiveOtherMark(ButtonManager.instance.thirdChoiceSnail, gameObject));
    }
}
