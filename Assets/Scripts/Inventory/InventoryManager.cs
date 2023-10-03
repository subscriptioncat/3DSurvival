using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private int currentEquipIndex;

    private PlayerController controller;
    private PlayerConditions condition;

    private const string EquipText = "Equip";
    private const string DequipText = "Dequip";
    private const string ConsumableText = "Consume";

    [SerializeField] private InventorySlotUI[] uiSlot;
    [SerializeField] private InventorySlot[] slots;

    [SerializeField] private GameObject inventoryWindow;
    [SerializeField] private Transform dropPosition;

    [Header("Selected Item")]
    private InventorySlot selectedItem;
    private int selectedItemIndex;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemLabelText;
    [SerializeField] private Button InteractBtn;
    [SerializeField] private Button DiscardBtn;

    [Header("Events")]
    [SerializeField] UnityEvent onOpenInventory;
    [SerializeField] UnityEvent onCloseInventory;

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

        inventoryWindow.SetActive(false);
        slots = new InventorySlot[uiSlot.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
            uiSlot[i].index = i;
            uiSlot[i].Clear();
        }

        ClearSelectItemWindow();
    }

    public void OnInventoryBtn(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
            controller.ToggleCursor(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
            controller.ToggleCursor(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData item)
    {
        if (item.isStackable)
        {
            InventorySlot slotToStackTo = GetItemStack(item);
            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        InventorySlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        //위의 조건을 만족시키지 못 한다면(인벤토리가 가득 찼다면) 해당 아이템을 뱉어낸다.
        ThrowItem(item);
    }

    /// <summary>
    /// 인벤토리에 해당 아이템의 수량이 충분한지의 여부를 반환한다.
    /// </summary>
    public bool IsEnough(ItemData itemData, int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null && slots[i].item.itemName == itemData.itemName)
            {
                amount -= slots[i].quantity;
                if(amount <= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 인벤토리에서 해당 아이템을 지정한 수량만큼 제거한다. 별도의 확인 과정 없이 수행되므로 조심해서 사용할 것.
    /// </summary>
    public void RemoveMaterials(ItemData itemData, int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.itemName == itemData.itemName)
            {

                if (slots[i].quantity >= amount)
                {
                    slots[i].quantity -= amount;
                    amount = 0;
                }
                else
                {
                    amount -= slots[i].quantity;
                }

                if (slots[i].quantity <= 0)
                {
                    if (uiSlot[i].equipped)
                    {
                        Dequip();
                    }

                    slots[i].item = null;
                    ClearSelectItemWindow();
                    UpdateUI();
                }

                if (amount <= 0)
                {
                    return;
                }
            }
        }

        if(amount > 0)
        {
            Debug.Log("RemoveMaterials Error!!! 인벤토리의 아이템 수량이 부족합니다!!!");
        }
    }

    /// <summary>
    /// 해당 아이템을 일정 반경 내의 랜덤한 위치에 생성한다. 
    /// </summary>
    public void ThrowItem(ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    public void RemoveItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            if (uiSlot[selectedItemIndex].equipped)
            {
                Dequip();
            }

            selectedItem.item = null;
            ClearSelectItemWindow();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                uiSlot[i].Set(slots[i]);
            }
            else
            {
                uiSlot[i].Clear();
            }
        }
    }

    private InventorySlot GetItemStack(ItemData item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == item && slots[i].quantity < item.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    private InventorySlot GetEmptySlot()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) { return; }

        selectedItem = slots[index];
        selectedItemIndex = index;

        itemNameText.text = selectedItem.item.itemName;
        itemLabelText.text = selectedItem.item.lable;

        itemNameText.gameObject.SetActive(true);
        itemLabelText.gameObject.SetActive(true);

        SetInteractBtn(selectedItem);
        DiscardBtn.gameObject.SetActive(true);
    }

    private void ClearSelectItemWindow()
    {
        selectedItem = null;
        itemNameText.text = string.Empty;
        itemLabelText.text = string.Empty;

        InteractBtn.gameObject.SetActive(false);
        DiscardBtn.gameObject.SetActive(false);
    }

    public void OnDiscardButton()
    {
        ThrowItem(selectedItem.item);
        RemoveItem();
    }

    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }

    /// <summary>
    /// 아이템 상호작용 버튼 세팅. 해당 아이템 타입에 대응하는 메소드를 연결한다.
    /// </summary>
    private void SetInteractBtn(InventorySlot itemSlot)
    {
        InteractBtn.onClick.RemoveAllListeners();

        switch (itemSlot.item.type)
        {
            case ItemType.Resource:
                {
                    InteractBtn.gameObject.SetActive(false);
                    break;
                }
            case ItemType.Equipable:
                {
                    if (selectedItemIndex < uiSlot.Length)
                    {
                        if (itemSlot.item.type == ItemType.Equipable)
                        {
                            if (uiSlot[selectedItemIndex].equipped)
                            {
                                InteractBtn.GetComponentInChildren<Text>().text = DequipText;
                                InteractBtn.onClick.AddListener(Dequip);
                            }
                            else
                            {
                                InteractBtn.GetComponentInChildren<Text>().text = EquipText;
                                InteractBtn.onClick.AddListener(Equip);
                            }
                        }
                        else
                        {
                            Debug.Log("EquipmentInteraction Error!!!");
                        }
                    }
                    else
                    {
                        Debug.Log("EquipmentInteraction Error!!!");
                    }

                    InteractBtn.gameObject.SetActive(true);
                    break;
                }
            case ItemType.Consumable:
                {
                    InteractBtn.GetComponentInChildren<Text>().text = ConsumableText;
                    InteractBtn.onClick.AddListener(Consume);
                    InteractBtn.gameObject.SetActive(true);
                    break;
                }
            default:
                {
                    InteractBtn.gameObject.SetActive(false);
                    break;
                }
        }
    }

    private void Equip()
    {
        uiSlot[selectedItemIndex].equipped = true;
        uiSlot[selectedItemIndex].SetEquipped(true);
        InteractBtn.GetComponentInChildren<Text>().text = DequipText;
        InteractBtn.onClick.AddListener(Dequip);
    }

    private void Dequip()
    {
        uiSlot[selectedItemIndex].equipped = false;
        uiSlot[selectedItemIndex].SetEquipped(false);
        InteractBtn.GetComponentInChildren<Text>().text = EquipText;
        InteractBtn.onClick.AddListener(Equip);
    }

    private void Consume()
    {
        if(selectedItem.item.type == ItemType.Consumable)
        {
            ConsumableData con;

            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                con = selectedItem.item.consumables[i];    
                switch (con.type)
                {
                    case ConsumableType.Hunger:
                        {
                            condition.Eat(con.value);
                            break;
                        }
                    case ConsumableType.Thirst:
                        {
                            condition.Drink(con.value);
                            break;
                        }
                    case ConsumableType.Health:
                        {
                            condition.Heal(con.value);
                            break;
                        }
                    case ConsumableType.Stemina:
                        {
                            Debug.Log($"Restored {con.value} Stemina");
                            break;
                        }
                    case ConsumableType.Heat:
                        {
                            condition.Heat(con.value);
                            break;
                        }
                    default:
                        {
                            InteractBtn.gameObject.SetActive(false);
                            break;
                        }
                }
            }
        }

        RemoveItem();
    }


}
