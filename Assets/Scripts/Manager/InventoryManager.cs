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
    /// �κ��丮 UI �� ���� ������ ����Ʈ ����. ������ 0 ������ �������� ���� ������ ����Ʈ���� �����Ѵ�.
    /// </summary>
    public void UpdateSlot()
    {
        //������ 0 ������ �������� ���� ������ ����Ʈ���� �����Ѵ�.
        for (int j = 0; j < items.Count;)
        {
            if (items[j].Quantity <= 0) { items.RemoveAt(j); }
            else { j++; }
        }

        int i = 0;
        //������� ���Կ� �߰��� �������� ������ ������ �ѵ��� ������ ����Ʈ�� �ѵ� ������ �� ���� �߰��Ѵ�.
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].Item = items[i];
        }
        //�κ��丮�� ������ ���� ���Ҵٸ� �� ĭ���� ä���.
        for (; i < slots.Length; i++)
        {
            slots[i].Item = null;
            slots[i].UpdateQuantity();
        }
    }

    /// <summary>
    /// ������ �߰�. �κ��丮�� �̹� �����ϴ� ������ �������̶�� ������ ����.
    /// </summary>
    public void AddItem(Item item)
    {
        if (item.Quantity <= 0)
        {
            Debug.Log("�������� ������ 1 �� �̻��̾�� �մϴ�!!!");
            return;
        }

        //�κ��丮�� �����ϴ� �������̶��
        for (int i = 0; i < slots.Length ; i++)
        {
            if(slots[i].Item.ItemSO.itemName == item.ItemSO.itemName)
            {
                slots[i].SetQuantity(slots[i].Item.Quantity + item.Quantity);
                return;
            }
        }

        //�κ��丮�� �������� �ʴ� �������̰�, �κ��丮 ������ �����ִٸ�
        if (items.Count < slots.Length)
        {
            items.Add(item);
            UpdateSlot();
        }
        else
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�!!!");
        }
    }

    /// <summary>
    /// �ش� �������� ���� ��ȯ. ���� �������̶�� 0 ��ȯ.
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
    /// ������ ������, ���� �� �˾�â�� ��� �� ����� �޼ҵ�. ��쿡 ���� �ı��� ���� ����
    /// </summary>
    /// <param name="okCallback"></param>
    /// <param name="text"></param>
    /// <param name="lable"></param>
    public void ShowPopup(Action okCallback, string text, string lable)
    {
        //popup.ShowPopup(okCallback, text, lable);
    }
}
