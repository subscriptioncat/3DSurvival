using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> items;

    [SerializeField] private Transform slotParent;
    [SerializeField] private InventorySlot[] slots;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        UpdateSlot();
    }

    /// <summary>
    /// 인벤토리 UI 및 보유 아이템 리스트 갱신. 수량이 0 이하인 아이템은 보유 아이템 리스트에서 제거한다.
    /// </summary>
    public void UpdateSlot()
    {
        //수량이 0 이하인 아이템은 보유 아이템 리스트에서 제거한다.
        for (int j = 0; j < items.Count;)
        {
            if (items[j].Quantity <= 0) { items.RemoveAt(j); }
            else { j++; }
        }

        int i = 0;
        //현재까지 슬롯에 추가한 아이템의 갯수가 슬롯의 한도나 아이템 리스트의 한도 이하일 때 까지 추가한다.
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].Item = items[i];
        }
        //인벤토리의 공간이 아직 남았다면 빈 칸으로 채운다.
        for (; i < slots.Length; i++)
        {
            slots[i].Item = null;
            slots[i].UpdateQuantity();
        }
    }

    /// <summary>
    /// 아이템 추가. 인벤토리에 이미 존재하는 종류의 아이템이라면 갯수만 증가.
    /// </summary>
    public void AddItem(Item item)
    {
        if (item.Quantity <= 0)
        {
            Debug.Log("아이템의 갯수는 1 개 이상이어야 합니다!!!");
            return;
        }

        //인벤토리에 존재하는 아이템이라면
        for (int i = 0; i < slots.Length ; i++)
        {
            if(slots[i].Item.ItemSO.itemName == item.ItemSO.itemName)
            {
                slots[i].SetQuantity(slots[i].Item.Quantity + item.Quantity);
                return;
            }
        }

        //인벤토리에 존재하지 않는 아이템이고, 인벤토리 공간이 남아있다면
        if (items.Count < slots.Length)
        {
            items.Add(item);
            UpdateSlot();
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다!!!");
        }
    }

    /// <summary>
    /// 해당 아이템의 수량 반환. 없는 아이템이라면 0 반환.
    /// </summary>
    public int GetQuantity(string itemName)
    {
        for (int i = 0; i < items.Count ; i++)
        {
            if(items[i].ItemSO.itemName == itemName)
            {
                return items[i].Quantity;
            }
        }

        return 0;
    }

    /// <summary>
    /// 아이템 버리기, 제작 시 팝업창을 띄울 때 사용할 메소드. 경우에 따라 파기할 수도 있음
    /// </summary>
    /// <param name="okCallback"></param>
    /// <param name="text"></param>
    /// <param name="lable"></param>
    public void ShowPopup(Action okCallback, string text, string lable)
    {
        //popup.ShowPopup(okCallback, text, lable);
    }
}
