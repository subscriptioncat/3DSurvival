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

        quantityText.gameObject.SetActive(true);
        image.gameObject.SetActive(true);

        equippedText.gameObject.SetActive(equipped);
    }

    public void Clear()
    {
        currentSlot = null;

        quantityText.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnClickSlot()
    {
        Debug.Log("InventorySlot Clicked!!!");
        InventoryManager.instance.SelectItem(index);
    }
}
