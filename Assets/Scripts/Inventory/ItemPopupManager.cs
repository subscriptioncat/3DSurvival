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
    [SerializeField] private Text discardAmountText;
    [SerializeField] private Button discardPlusBtn;
    [SerializeField] private Button discardMinusBtn;
    [SerializeField] private Button discardYesBtn;
    [SerializeField] private Button discardNoBtn;
    private int max;

    private event Action IntereactProcess;
    private event Action<int> DiscardProcess;

    private void Awake()
    {
        if (instance == null) { instance = this; }

        //아이템 상호작용 팝업 초기화
        interactYesBtn.onClick.AddListener(HidePopup);
        interactYesBtn.onClick.AddListener(() => IntereactProcess?.Invoke());

        interactNoBtn.onClick.AddListener(HidePopup);

        //아이템 버리기 팝업 초기화
        discardYesBtn.onClick.AddListener(HidePopup);
        discardYesBtn.onClick.AddListener(() => DiscardProcess?.Invoke(int.Parse(discardAmountText.text)));

        discardNoBtn.onClick.AddListener(HidePopup);

        discardPlusBtn.onClick.AddListener(IncreseAmount);
        discardMinusBtn.onClick.AddListener(DecreseAmount);
    }

    //TODO
    //아이템 상호작용 팝업, 아이템 버리기 팝업 컨트롤러 클래스 분할하기, 아이템 버리기 팝업 컨트롤러 완성하기.
    //https://rito15.github.io/posts/unity-study-rpg-inventory/ 아이템 분할 부분 참조해서 만들기. +- 버튼 기능과 입력 상한치 지정이 남았음.

    private void HidePopup() { interactPopup.SetActive(false); discardPopup.SetActive(false); }

    public void ShowInteractPopup(Action okCallback, string text, string lable)
    {
        interactPopupText.text = text;
        interactPopupLable.text = lable;
        interactPopup.SetActive(true);
        IntereactProcess = okCallback;
    }

    public void ShowDiscardPopup(Action<int> okCallback, InventorySlot inventorySlot)
    {
        discardPopupText.text = "버리시겠습니까?";
        discardPopupLable.text = inventorySlot.item.itemName;
        discardAmountText.text = "1";
        discardPopup.SetActive(true);
        DiscardProcess = okCallback;
        this.max = inventorySlot.quantity;
    }

    private void IncreseAmount()
    {
        if (int.TryParse(discardAmountText.text, out int discardAmt))
        {
            if (discardAmt + 1 <= max) { discardAmt += 1;  discardAmountText.text = discardAmt.ToString(); }
        }
    }

    private void DecreseAmount()
    {
        if (int.TryParse(discardAmountText.text, out int discardAmt))
        {
            if (discardAmt - 1 >= 0) { discardAmt -= 1; discardAmountText.text = discardAmt.ToString(); }
        }
    }
}
