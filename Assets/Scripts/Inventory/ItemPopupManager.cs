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

        DiscardProcess = okCallback;
        this.max = inventorySlot.quantity;
        SetPlusMinusBtn();

        discardPopup.SetActive(true);
    }

    private void SetPlusMinusBtn()
    {
        if(int.TryParse(discardAmountText.text, out int amt))
        {
            discardPlusBtn.interactable = true;
            discardMinusBtn.interactable = true;

            if (amt >= max) { discardPlusBtn.interactable = false; }
            
            if(amt <= 1) { discardMinusBtn.interactable = false; }
        }
    }

    private void IncreseAmount()
    {
        if (int.TryParse(discardAmountText.text, out int discardAmt))
        {
            if (discardAmt + 1 <= max) 
            { 
                discardAmt += 1;  
                discardAmountText.text = discardAmt.ToString();
                discardMinusBtn.interactable = true;
            }
            
            //현재 설정된 버릴 수량이 보유 수량 이상이라면 + 버튼 선택이 불가능하도록 변경
            if (discardAmt >= max) { discardPlusBtn.interactable = false; }
        }
    }

    private void DecreseAmount()
    {
        if (int.TryParse(discardAmountText.text, out int discardAmt))
        {
            if (discardAmt - 1 >= 1) 
            { 
                discardAmt -= 1; 
                discardAmountText.text = discardAmt.ToString();
                discardPlusBtn.interactable = true;
            }

            //현재 설정된 버릴 수량이 최저 수량(1개) 이하라면 - 버튼 선택이 불가능하도록 변경
            if (discardAmt <= 1) { discardMinusBtn.interactable = false; }
        }
    }
}
