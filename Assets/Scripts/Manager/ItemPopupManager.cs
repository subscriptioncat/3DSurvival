using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopupManager : MonoBehaviour
{
    public static ItemPopupManager instance;

    [Header("Interact Popup")]
    [SerializeField] private GameObject interactPopup;
    [SerializeField] private Text interactPopupText;
    [SerializeField] private Text interactPopupLable;
    [SerializeField] private Button interactYesBtn;
    [SerializeField] private Button interactNoBtn;

    [Header("Discard Popup")]
    [SerializeField] private GameObject discardPopup;
    [SerializeField] private Text discardPopupText;
    [SerializeField] private Text discardPopupLable;
    [SerializeField] private Button discardYesBtn;
    [SerializeField] private Button discardNoBtn;

    private event Action StartProcess;

    private void Awake()
    {
        if (instance == null) { instance = this; }

        //������ ��ȣ�ۿ� �˾� �ʱ�ȭ
        interactYesBtn.onClick.AddListener(HidePopup);
        interactYesBtn.onClick.AddListener(() => StartProcess?.Invoke());

        interactNoBtn.onClick.AddListener(HidePopup);

        //������ ������ �˾� �ʱ�ȭ
        discardYesBtn.onClick.AddListener(HidePopup);
        discardYesBtn.onClick.AddListener(() => StartProcess?.Invoke());

        discardNoBtn.onClick.AddListener(HidePopup);
    }

    private void HidePopup() { interactPopup.SetActive(false); discardPopup.SetActive(false); }

    public void ShowInteractPopup(Action okCallback, string text, string lable)
    {
        interactPopupText.text = text;
        interactPopupLable.text = lable;
        interactPopup.SetActive(true);
        StartProcess = okCallback;
    }
    public void ShowDiscardPopup(Action okCallback, string text, string lable)
    {
        discardPopupText.text = text;
        discardPopupLable.text = lable;
        discardPopup.SetActive(true);
        StartProcess = okCallback;
    }
}
