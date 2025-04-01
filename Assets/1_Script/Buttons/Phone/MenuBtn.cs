using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuBtn : MonoBehaviour
{
    protected GameObject phone;
    protected GameObject menu;
    protected GameObject content;

    protected TextMeshProUGUI contentText;

    protected int menuIndex = 1;
    protected int contentIndex = 2;
    

    void Start()
    {
        phone = transform.parent.parent.gameObject;
        menu = phone.transform.GetChild(menuIndex).gameObject;
        content = phone.transform.GetChild(contentIndex).gameObject;
        contentText = content.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        GetComponent<Button>().onClick.AddListener(() => Content());
    }

    protected virtual void Content()
    {
        
    }
}
