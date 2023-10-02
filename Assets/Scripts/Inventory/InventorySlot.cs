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
            //빈 슬롯이라면...?
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
            //인벤토리 하단에 현재 아이템 정보 출력 및 상호작용, 버리기 버튼 활성화
            InventoryInfo.instance.UpdateInfo(this);

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
}
