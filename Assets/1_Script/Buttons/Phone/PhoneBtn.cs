using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneBtn : MonoBehaviour
{
    GameObject menu;
    GameObject content;

    private void Start()
    {
        menu = transform.parent.GetChild(1).gameObject;
        content = transform.parent.GetChild(2).gameObject;

        GetComponent<Button>().onClick.AddListener(() => Menu());
    }

    void Menu()
    {
        content.SetActive(false);

        if (!menu.activeSelf) menu.SetActive(true);
        else menu.SetActive(false);
    }
}
