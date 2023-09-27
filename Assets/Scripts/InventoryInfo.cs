using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfo : MonoBehaviour
{
    public static InventoryInfo instance;
    private ItemSO item;

    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemLabelText;
    [SerializeField] private Button InteractBtn;
    [SerializeField] private Button DiscardBtn;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        itemNameText.gameObject.SetActive(false);
        itemLabelText.gameObject.SetActive(false);
        InteractBtn.gameObject.SetActive(false);
        DiscardBtn.gameObject.SetActive(false);
    }

    /// <summary>
    /// 해당 아이템 슬롯에 대한 정보 출력 및 상호작용 버튼 활성화
    /// </summary>
    public void UpdateInfo(InventorySlot inventorySlot)
    {
        this.item = inventorySlot.Item.ItemSO;

        itemNameText.text = item.itemName;
        itemLabelText.text = item.lable;

        string interactText = "";

        if(item is UsableSO usable)
        {
            //식료품이라면
            if(item is EdibleSO edible)
            {
                interactText = "Intake";
            }
            //단순 소모품이라면
            else
            {
                interactText = "Use";
            }
        }
        //장비품이라면
        else if(item is EquipmentSO equipment)
        {
            interactText = "Equip / Dequip";
        }
        //단순 재료 아이템이라면
        else
        {
            interactText = "none";
        }

        if (interactText != "none") { 
            InteractBtn.GetComponentInChildren<Text>().text = interactText; 
            InteractBtn.gameObject.SetActive(true); 
        }
        else
        {
            InteractBtn.gameObject.SetActive(false);
        }
    }
    //TODO
    //인벤토리 정보 창에서 클릭한 버튼에 따라 해당하는 팝업 창을 띄우도록 하는 부분을 작업해야 함.

    /// <summary>
    /// 아이템 버리기, 제작, 사용 시 팝업창을 띄울 때 사용할 메소드. 경우에 따라 파기할 수도 있음
    /// </summary>
    /// <param name="okCallback"></param>
    /// <param name="text"></param>
    /// <param name="lable"></param>
    public void ShowInteractPopup(Action okCallback, string text, string lable)
    {
        ItemPopupManager.instance.ShowInteractPopup(okCallback, text, lable);
    }
}
