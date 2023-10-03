using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlotUI : MonoBehaviour
{
    [SerializeField] private Button IconBtn;
    [SerializeField] private Image image;

    private CraftSlot currentSlot;

    public int index;

    private void Awake()
    {

    }

    public void Set(CraftSlot slot)
    {
        currentSlot = slot;
        image.sprite = slot.item.Item.icon;
        image.gameObject.SetActive(true);
    }

    public void Clear()
    {
        currentSlot = null;
        image.gameObject.SetActive(false);
    }

    public void OnClickSlot()
    {
        CraftManager.instance.SelectItem(index);
    }
}
