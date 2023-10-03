using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    public static CraftManager instance;

    private int currentEquipIndex;

    private PlayerController controller;
    private PlayerConditions condition;

    private const string NotEnoughText = "재료 부족";
    private const string EnoughText = "Craft";

    [SerializeField] private CraftSlotUI[] uiSlot;
    [SerializeField] private CraftSlot[] slots;

    [SerializeField] private GameObject craftWindow;
    [SerializeField] private Transform dropPosition;

    [Header("Selected Item")]
    private CraftSlot selectedItem;
    private int selectedItemIndex;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemLabelText;
    [SerializeField] private Text itemAmountText;
    [SerializeField] private Button craftBtn;

    [Header("Events")]
    [SerializeField] UnityEvent onOpenCraft;
    [SerializeField] UnityEvent onCloseCraft;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();

        craftWindow.SetActive(false);
        slots = new CraftSlot[uiSlot.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new CraftSlot();
            uiSlot[i].index = i;
            uiSlot[i].Clear();
        }

        ClearSelectItemWindow();
    }

    public void OnCraftBtn()
    {
        Toggle();
    }

    public void Toggle()
    {
        if (craftWindow.activeInHierarchy)
        {
            craftWindow.SetActive(false);
            onCloseCraft?.Invoke();
            controller.ToggleCursor(false);
        }
        else
        {
            craftWindow.SetActive(true);
            onOpenCraft?.Invoke();
            controller.ToggleCursor(true);
        }
    }

    public bool IsOpen()
    {
        return craftWindow.activeInHierarchy;
    }

    /// <summary>
    /// 실질적인 제작을 담당하는 메소드. 재료의 소모와 제작만 이루어지므로, 재료의 충만 여부는 다른 쪽에서 확인해야 한다.
    /// </summary>
    public void CraftItem()
    {
        CraftMaterial[] recipe = selectedItem.item.Materials;
        ItemData item = selectedItem.item.Item;
        int amount = selectedItem.quantity;

        RemoveMaterials(recipe);

        for (int i = 0; i < amount; i++)
        {
            InventoryManager.instance.AddItem(item);
        }
    }

    /// <summary>
    /// 인벤토리에 해당 레시피에 소모되는 아이템이 충분한지의 여부를 반환한다.
    /// </summary>
    private bool IsEnough(CraftMaterial[] recipe)
    {
        InventoryManager inventoryManager = InventoryManager.instance;

        //레시피의 재료들 중 하나라도 부족하다면 false
        for (int i = 0; i < recipe.Length; i++)
        {
            if (!inventoryManager.IsEnough(recipe[i].itemData, recipe[i].amount))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 인벤토리에서 레시피에 포함된 아이템들을 제거한다. 별도의 확인 과정 없이 수행되므로 IsEnough 로 판별한 뒤에 사용할 것.
    /// </summary>
    private void RemoveMaterials(CraftMaterial[] recipe)
    {
        InventoryManager inventoryManager = InventoryManager.instance;

        for (int i = 0; i < recipe.Length; i++)
        {
            inventoryManager.RemoveMaterials(recipe[i].itemData, recipe[i].amount);
        }
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) { return; }

        selectedItem = slots[index];
        selectedItemIndex = index;

        itemNameText.text = selectedItem.item.Item.itemName;
        itemLabelText.text = selectedItem.item.Item.lable;
        itemAmountText.text = selectedItem.item.Amount.ToString();

        itemNameText.gameObject.SetActive(true);
        itemLabelText.gameObject.SetActive(true);
        itemAmountText.gameObject.SetActive(true);

        SetCraftBtn(selectedItem);
    }

    private void ClearSelectItemWindow()
    {
        selectedItem = null;
        itemNameText.text = string.Empty;
        itemLabelText.text = string.Empty;

        craftBtn.gameObject.SetActive(false);
    }

    public void OnCraftButton()
    {
        CraftItem();
        SetCraftBtn(selectedItem);
    }

    /// <summary>
    /// 아이템 제작 버튼 세팅. 선택된 레시피의 재료가 충분하다면 제작 버튼 활성화, 아니라면 별도의 텍스트를 출력하고 비활성화.
    /// </summary>
    private void SetCraftBtn(CraftSlot itemSlot)
    {
        //재료가 충분하다면
        if (IsEnough(itemSlot.item.Materials))
        {
            craftBtn.GetComponentInChildren<Text>().text = NotEnoughText;
        }
        else
        {
            craftBtn.GetComponentInChildren<Text>().text = EnoughText;
        }

        craftBtn.gameObject.SetActive(true);
    }
}
