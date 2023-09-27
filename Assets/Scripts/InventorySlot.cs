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

            //빈 슬롯이 아니라면 이미지 표시
            if (item != null)
            {
                image.sprite = item.ItemSO.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            //빈 슬롯이라면 이미지를 표시하지 않음
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

        //남은 수량이 0이 되었다면, 인벤토리 UI와 배치된 Item 을 갱신한다.
        if(item.Quantity <= 0)
        {
            InventoryManager.instance.UpdateSlot();
        }
    }

    /// <summary>
    /// 아이템 수량 표시 텍스트 갱신. item 이 null 이거나 수량이 0 이하라면 텍스트 비활성화.
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
            //string popupText = "";
            //string popupLable = GetLableText();

            //if(item.ItemSO is EquipmentSO equipmentSO)
            //if (equipmentSO.isEquiped)
            //{
            //    popupText = "장비를 해제하시겠습니까?";

            //    InventoryManager.instance.ShowPopup(() => Dequip(), popupText, popupLable);
            //}
            //else
            //{
            //    popupText = "장비를 장착하시겠습니까?";
            //    Inventory.instance.ShowPopup(() => Equip(), popupText, popupLable);
            //}
        }
    }

    private void Equip()
    {
        //player.atk += item.atk;
        //player.def += item.def;
        //item.isEquiped = true;
    }

    private void Dequip()
    {
        //player.atk -= item.atk;
        //player.def -= item.def;
        //item.isEquiped = false;
    }

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
