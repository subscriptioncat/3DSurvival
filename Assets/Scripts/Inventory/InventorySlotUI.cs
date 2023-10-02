using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Button IconBtn;
    [SerializeField] private Image image;

    [SerializeField] private Text quantityText;
    [SerializeField] private Text equippedText;

    private InventorySlot currentSlot; 
     
    public int index;
    public bool equipped;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        equippedText.gameObject.SetActive(equipped);
    }

    public void Set(InventorySlot slot)
    {
        currentSlot = slot;
        image.sprite = slot.item.icon;
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;
        image.gameObject.SetActive(true);
        equippedText.gameObject.SetActive(equipped);
    }

    public void Clear()
    {
        currentSlot = null;
        image.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonClick()
    {

    }

    public void SetQuantity(int quantity)
    {
        //item.Quantity = quantity;
        //UpdateQuantity();

        ////남은 수량이 0이 되었다면, 인벤토리 UI와 배치된 Item 을 갱신한다.
        //if(item.Quantity <= 0)
        //{
        //    InventoryManager.instance.UpdateSlot();
        //}
    }

    /// <summary>
    /// 아이템 수량 표시 텍스트 갱신. item 이 null 이거나 수량이 0 이하라면 텍스트 비활성화.
    /// </summary>
    public void UpdateQuantity()
    {
        //if (item == null || item.Quantity <= 0)
        //{
        //    quantityText.gameObject.SetActive(false);
        //    return;
        //}

        //quantityText.text = item.Quantity.ToString();
        //quantityText.gameObject.SetActive(true);
    }

    public void OnClickSlot()
    {
        //if (item != null)
        //{
        //    인벤토리 하단에 현재 아이템 정보 출력 및 상호작용, 버리기 버튼 활성화
        //    InventoryInfo.instance.UpdateInfo(this);

        //    string popupText = "";
        //    string popupLable = GetLableText();

        //    if (item.ItemSO is EquipmentSO equipmentSO)
        //        if (equipmentSO.isEquiped)
        //        {
        //            popupText = "장비를 해제하시겠습니까?";

        //            InventoryManager.instance.ShowPopup(() => Dequip(), popupText, popupLable);
        //        }
        //        else
        //        {
        //            popupText = "장비를 장착하시겠습니까?";
        //            Inventory.instance.ShowPopup(() => Equip(), popupText, popupLable);
        //        }
        //}
    }

    private void SetItem(ItemData item)
    {
        //빈 슬롯이 아니라면 이미지 표시
        if (item != null)
        {
            image.sprite = item.icon;
            image.color = new Color(1, 1, 1, 1);

            if (item.isStackable)
            {
                quantityText.gameObject.SetActive(false);
            }
            else
            {
                quantityText.gameObject.SetActive(true);
            }

        }
        //빈 슬롯이라면...?
        else
        {
            //image.color = new Color(1, 1, 1, 0);
        }

        UpdateQuantity();
    }
}
