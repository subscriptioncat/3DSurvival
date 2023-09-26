using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopupManager : MonoBehaviour
{
    [Header("Equipment Popup")]
    [SerializeField] private GameObject popupObject;
    [SerializeField] private Text popupText;
    [SerializeField] private Text popupLable;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    private event Action StartProcess;

    private void Awake()
    {
        yesBtn.onClick.AddListener(HidePopup);
        yesBtn.onClick.AddListener(() => StartProcess?.Invoke());

        noBtn.onClick.AddListener(HidePopup);
    }

    private void HidePopup() { gameObject.SetActive(false); }

    public void ShowPopup(Action okCallback, string text, string lable)
    {
        popupText.text = text;
        popupLable.text = lable;
        popupObject.SetActive(true);
        StartProcess = okCallback;
    }
}
