using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetBtn : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GambleManager.instance.Bet());
    }
}