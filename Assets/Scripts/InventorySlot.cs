using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image image;
    //[SerializeField] PlayerSO player;
    [SerializeField] private Text quantityText;

    private Item item;

    public Item Item
    {
        get { return item; }
        set
        {
            item = value;

            //�� ������ �ƴ϶�� �̹��� ǥ��
            if (item != null)
            {
                image.sprite = item.ItemSO.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            //�� �����̶��...?
            else
            {
                //image.color = new Color(1, 1, 1, 0);
            }

            UpdateQuantity();
        }
    }

    public void SetQuantity(int quantity)
    {
        item.Quantity = quantity;
        UpdateQuantity();

        //���� ������ 0�� �Ǿ��ٸ�, �κ��丮 UI�� ��ġ�� Item �� �����Ѵ�.
        if(item.Quantity <= 0)
        {
            InventoryManager.instance.UpdateSlot();
        }
    }

    /// <summary>
    /// ������ ���� ǥ�� �ؽ�Ʈ ����. item �� null �̰ų� ������ 0 ���϶�� �ؽ�Ʈ ��Ȱ��ȭ.
    /// </summary>
    public void UpdateQuantity()
    {
        if (item == null || item.Quantity <= 0)
        {
            quantityText.gameObject.SetActive(false);
            return;
        }

        quantityText.text = item.Quantity.ToString();
        quantityText.gameObject.SetActive(true);
    }

    public void OnClickSlot()
    {
        if (item != null)
        {
            //�κ��丮 �ϴܿ� ���� ������ ���� ��� �� ��ȣ�ۿ�, ������ ��ư Ȱ��ȭ
            InventoryInfo.instance.UpdateInfo(this);


            //string popupText = "";
            //string popupLable = GetLableText();

            //if(item.ItemSO is EquipmentSO equipmentSO)
            //if (equipmentSO.isEquiped)
            //{
            //    popupText = "��� �����Ͻðڽ��ϱ�?";

            //    InventoryManager.instance.ShowPopup(() => Dequip(), popupText, popupLable);
            //}
            //else
            //{
            //    popupText = "��� �����Ͻðڽ��ϱ�?";
            //    Inventory.instance.ShowPopup(() => Equip(), popupText, popupLable);
            //}
        }
    }

    private void Discard()
    {
        
    }

    private void Use()
    {
        if(item.ItemSO is UsableSO usable)
        {
            //�ķ�ǰ ī�װ��� �������̶��
            if(usable is EdibleSO edible)
            {
                Debug.Log("is Edible Item!");

                //�÷��̾��� ü��, ������, ����, ���׹̳� ���� ������Ű�� �κ�.
                
                //�ش� �������� ���� 1 ����, �̿� ���� ������ ��û�ϴ� �κ�.
                item.Quantity -= 1;
                UpdateQuantity();
            }
            //�ܼ� �Ҹ�ǰ ī�װ��� �������̶��
            else
            {
                Debug.Log("is Usable Item!");
            }
        }
    }

    //���ǰ ���� �߰� �� ����� ����
    //private void Equip()
    //{
    //    player.atk += item.atk;
    //    player.def += item.def;
    //    item.isEquiped = true;
    //}

    //private void Dequip()
    //{
    //    player.atk -= item.atk;
    //    player.def -= item.def;
    //    item.isEquiped = false;
    //}

    //private string GetPopupLableText()
    //{
    //    string popupLable = "";

    //    if (item.atk != 0)
    //    {
    //        if (item.atk > 0)
    //        {
    //            popupLable += "+ ";
    //        }
    //        popupLable += $"{item.atk} ATK";
    //    }
    //    if (item.def != 0)
    //    {
    //        if (item.def > 0)
    //        {
    //            popupLable += "+ ";
    //        }
    //        popupLable += $"{item.def} DEF";
    //    }
    //    if (item.con != 0)
    //    {
    //        if (item.con > 0)
    //        {
    //            popupLable += "+ ";
    //        }
    //        popupLable += $"{item.con} CON";
    //    }

    //    return popupLable;
    //}
}
