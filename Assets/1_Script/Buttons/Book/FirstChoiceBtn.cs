using UnityEngine;
using UnityEngine.UI;

public class FirstChoiceBtn : MonoBehaviour
{
    [SerializeField] int snailNum, choiceNum;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.ChoiceSnail(transform, snailNum, choiceNum));
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.DeActiveOtherMark(ButtonManager.instance.firstChoiceSnail, gameObject));
    }
}
