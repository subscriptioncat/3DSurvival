using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfo : MonoBehaviour
{
    public static InventoryInfo instance;
    private InventorySlot inventorySlot;

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
        //this.inventorySlot = inventorySlot;
        //Item item = this.inventorySlot.Item;

        //itemNameText.text = item.ItemSO.itemName;
        //itemLabelText.text = item.ItemSO.lable;

        //string interactText = "";

        //DiscardBtn.gameObject.SetActive(true);

        //if(item.ItemSO is UsableSO usable)
        //{
        //    interactText = "Use";
        //}
        ////장비품이라면
        //else if(item.ItemSO is EquipmentSO equipment)
        //{
        //    interactText = "Equip / Dequip";

        //    //장착한 상태일 때에는 버리기 불가능.
        //    if (equipment.isEquiped)
        //    {
        //        DiscardBtn.gameObject.SetActive(false);
        //    }
        //}
        ////단순 재료 아이템이라면
        //else
        //{
        //    interactText = "none";
        //}

        //if (interactText != "none") { 
        //    InteractBtn.GetComponentInChildren<Text>().text = interactText; 
        //    InteractBtn.gameObject.SetActive(true); 
        //}
        //else
        //{
        //    InteractBtn.gameObject.SetActive(false);
        //}
    }

    //TODO
    //인벤토리 정보 창에서 클릭한 버튼에 따라 해당하는 팝업 창을 띄우도록 하는 부분을 작업해야 함.
    //DiscartItem() 작업까지 완료해야 함.

    private void DiscardItem()
    {
        //Item item = inventorySlot.Item;
        //string popupText = "버리시겠습니까?";
        //string popupLabel = item.ItemSO.itemName;
        //int max = item.Quantity;

        //Action<int> action = Discard;

        //ItemPopupManager.instance.ShowDiscardPopup(action, popupText, popupLabel, max);
    }

    private void Discard(int amount)
    {
        //Item item = inventorySlot.Item;

        //inventorySlot.SetQuantity(item.Quantity - amount);
    }

    private void InteractItem()
    {
        //Item item = inventorySlot.Item;
        //string popupText = "";
        //string popupLabel = item.ItemSO.itemName;

        //Action action;

        //if (item.ItemSO is UsableSO usable)
        //{
        //    popupText = "사용하시겠습니까?";
        //    action = Use;
        //}
        ////장비품이라면 장착, 탈착 메소드 붙여주기
        //else if (item.ItemSO is EquipmentSO equipment)
        //{
        //    if (equipment.isEquiped)
        //    {
        //        popupText = "탈착하시겠습니까?";
        //        action = Dequip;
        //    }
        //    else
        //    {
        //        popupText = "장착하시겠습니까?";
        //        action = Equip;
        //    }
        //}
        //else
        //{
        //    popupText = "IteractItem Error";
        //    action = null;
        //}
        //ItemPopupManager.instance.ShowInteractPopup(action, popupText, popupLabel);
    }

    private void Use()
    {
        //Item item = inventorySlot.Item;

        //if (item.ItemSO is UsableSO usable)
        //{
        //    //식료품 카테고리의 아이템이라면
        //    if (usable is EdibleSO edible)
        //    {
        //        Debug.Log("is Edible Item!");

        //        //플레이어의 체력, 포만감, 수분, 스테미너 등을 증감시키는 부분.

        //        //해당 아이템의 수량 1 감소, 이에 대한 갱신을 요청하는 부분.
        //        inventorySlot.SetQuantity(item.Quantity -1);
        //    }
        //    //단순 소모품 카테고리의 아이템이라면
        //    else
        //    {
        //        Debug.Log("is Usable Item!");
        //    }
        //}
    }

    //장비품 관련 추가 시 사용할 예정
    private void Equip() { }
    private void Dequip() { }
}
