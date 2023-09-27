using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfo : MonoBehaviour
{
    public static InventoryInfo instance;
    private ItemSO item;

    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemLabelText;
    [SerializeField] private Button InteractBtn;
    [SerializeField] private Button DiscardBtn;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        itemNameText.gameObject.SetActive(false);
        itemLabelText.gameObject.SetActive(false);
        InteractBtn.gameObject.SetActive(false);
        DiscardBtn.gameObject.SetActive(false);
    }

    /// <summary>
    /// �ش� ������ ���Կ� ���� ���� ��� �� ��ȣ�ۿ� ��ư Ȱ��ȭ
    /// </summary>
    public void UpdateInfo(InventorySlot inventorySlot)
    {
        this.item = inventorySlot.Item.ItemSO;

        itemNameText.text = item.itemName;
        itemLabelText.text = item.lable;

        string interactText = "";

        if(item is UsableSO usable)
        {
            //�ķ�ǰ�̶��
            if(item is EdibleSO edible)
            {
                interactText = "Intake";
            }
            //�ܼ� �Ҹ�ǰ�̶��
            else
            {
                interactText = "Use";
            }
        }
        //���ǰ�̶��
        else if(item is EquipmentSO equipment)
        {
            interactText = "Equip / Dequip";
        }
        //�ܼ� ��� �������̶��
        else
        {
            interactText = "none";
        }

        if (interactText != "none") { 
            InteractBtn.GetComponentInChildren<Text>().text = interactText; 
            InteractBtn.gameObject.SetActive(true); 
        }
        else
        {
            InteractBtn.gameObject.SetActive(false);
        }
    }
    //TODO
    //�κ��丮 ���� â���� Ŭ���� ��ư�� ���� �ش��ϴ� �˾� â�� ��쵵�� �ϴ� �κ��� �۾��ؾ� ��.

    /// <summary>
    /// ������ ������, ����, ��� �� �˾�â�� ��� �� ����� �޼ҵ�. ��쿡 ���� �ı��� ���� ����
    /// </summary>
    /// <param name="okCallback"></param>
    /// <param name="text"></param>
    /// <param name="lable"></param>
    public void ShowInteractPopup(Action okCallback, string text, string lable)
    {
        ItemPopupManager.instance.ShowInteractPopup(okCallback, text, lable);
    }
}
