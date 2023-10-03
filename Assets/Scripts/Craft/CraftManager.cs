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
    [SerializeField] private CraftSO[] recipes;
    [SerializeField] private CraftSlot[] slots;

    [SerializeField] private GameObject craftWindow;

    [Header("Selected Item")]
    private CraftSlot selectedItem;
    private int selectedItemIndex;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemLabelText;
    [SerializeField] private Text itemAmountText;
    [SerializeField] private Text itemRecipeText;
    [SerializeField] private Button craftBtn;

    [Header("Inventory/Crafting UI")]
    [SerializeField] private Button InventoryBtn;
    [SerializeField] private Button CraftingBtn;

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

        AddRecipes(recipes);

        ClearSelectItemWindow();
    }

    public void OnCraftingBtnClicked()
    {
        InventoryBtn.interactable = true;
        CraftingBtn.interactable = false;
        InventoryBtn.gameObject.SetActive(true);
        CraftingBtn.gameObject.SetActive(true);

        craftWindow.SetActive(true);
        InventoryManager.instance.SetWindow(false);
    }

    public void SetWindow(bool isOpen)
    {
        craftWindow.SetActive(isOpen);
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

        InventoryBtn.interactable = true;
        CraftingBtn.interactable = false;
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
        Debug.Log("UsingMaterials : CraftManager");
        InventoryManager inventoryManager = InventoryManager.instance;

        for (int i = 0; i < recipe.Length; i++)
        {
            inventoryManager.RemoveMaterials(recipe[i].itemData, recipe[i].amount);
        }
    }

    public void SelectItem(int index)
    {
        Debug.Log("CraftingItemSelected");
        if (slots[index].item == null) { return; }

        selectedItem = slots[index];
        selectedItemIndex = index;

        SetCraft(selectedItem);
    }

    private void ClearSelectItemWindow()
    {
        selectedItem = null;
        itemNameText.text = string.Empty;
        itemLabelText.text = string.Empty;
        itemAmountText.text = string.Empty;
        itemRecipeText.text = string.Empty;

        craftBtn.gameObject.SetActive(false);
    }

    public void OnCraftButton()
    {
        CraftItem();
        SetCraft(selectedItem);
    }

    /// <summary>
    /// 아이템 제작 세팅. 선택된 레시피에 대한 정보 표시, 재료가 충분하다면 제작 버튼 활성화, 아니라면 별도의 텍스트를 출력하고 비활성화.
    /// </summary>
    private void SetCraft(CraftSlot itemSlot)
    {
        string recipeStr = GetRecipeText(itemSlot);

        itemNameText.text = selectedItem.item.Item.itemName;
        itemLabelText.text = selectedItem.item.Item.lable;
        itemAmountText.text = "x " + selectedItem.item.Amount.ToString();
        itemRecipeText.text = recipeStr;

        itemNameText.gameObject.SetActive(true);
        itemLabelText.gameObject.SetActive(true);
        itemAmountText.gameObject.SetActive(true);
        itemRecipeText.gameObject.SetActive(true);

        //재료가 충분하다면
        if (IsEnough(itemSlot.item.Materials))
        {
            craftBtn.GetComponentInChildren<Text>().text = EnoughText;
            craftBtn.interactable = true;
        }
        else
        {
            craftBtn.GetComponentInChildren<Text>().text = NotEnoughText;
            craftBtn.interactable = false;
        }

        craftBtn.gameObject.SetActive(true);
    }

    private string GetRecipeText(CraftSlot craftSlot)
    {
        string recipeText = "";
        CraftMaterial[] craftMaterials = craftSlot.item.Materials;

        if (craftMaterials.Length > 0)
        {
            InventoryManager inventory = InventoryManager.instance;
            int remain = 0;

            for (int i = 0; i < craftMaterials.Length; i++)
            {
                remain = inventory.GetRemain(craftMaterials[i].itemData);
                recipeText += $"(<b>{remain}</b>/{craftMaterials[i].amount}) {craftMaterials[i].itemData.itemName}\n";
            }
        }
        else { recipeText = "필요 재료 없음"; }

        return recipeText;
    }

    private void AddRecipes(CraftSO[] crafts)
    {
        CraftSlot craftSlot = new CraftSlot();

        for (int i = 0; i < crafts.Length; i++)
        {
            if (i >= slots.Length) { return; }

            craftSlot.item = crafts[i];
            craftSlot.quantity = crafts[i].Amount;
            slots[i] = craftSlot;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                uiSlot[i].Set(slots[i]);
            }
            else
            {
                uiSlot[i].Clear();
            }
        }
    }
}
