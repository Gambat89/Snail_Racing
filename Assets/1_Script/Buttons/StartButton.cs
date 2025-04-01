using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => StartCoroutine(MentManager.instance.StartMent()));
    }
}
