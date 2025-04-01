using UnityEngine;
using UnityEngine.UI;

public class GameKindBtn : MonoBehaviour
{
    [SerializeField] GambleManager.GameKind gameKind;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.ChangeGameKind(gameKind));
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.CheckMark(transform));
        GetComponent<Button>().onClick.AddListener(() => ButtonManager.instance.DeActiveOtherMark(ButtonManager.instance.gameKindButtons, gameObject));
    }
}
