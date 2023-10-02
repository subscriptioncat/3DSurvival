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

    private const string EquipableText = "Equip / Dequip";
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
        DiscardItem(item);
    }

    /// <summary>
    /// 해당 아이템을 일정 반경 내의 랜덤한 위치에 생성한다. 
    /// </summary>
    /// <param name="item"></param>
    public void DiscardItem(ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
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

        SetInteractBtn(selectedItem);

        //for(int i = 0; i < selectedItem.item.conuma)

    }

    private void ClearSelectItemWindow()
    {
        selectedItem = null;
        itemNameText.text = string.Empty;
        itemLabelText.text = string.Empty;

        InteractBtn.gameObject.SetActive(false);
        DiscardBtn.gameObject.SetActive(false);
    }

    public void OnInteractButton()
    {

    }

    public void OnDiscardButton()
    {
        DiscardItem(selectedItem.item);
    }

    public void RemoveItem(ItemData item)
    {

    }

    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }

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
                    InteractBtn.GetComponentInChildren<Text>().text = EquipableText;

                    if (selectedItemIndex < uiSlot.Length)
                    {
                        if (itemSlot.item.type == ItemType.Equipable)
                        {
                            if (uiSlot[selectedItemIndex].equipped)
                            {
                                InteractBtn.onClick.AddListener(Dequip);
                            }
                            else
                            {
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
    }

    private void Dequip()
    {
    }

    private void Consume()
    {

    }
}
