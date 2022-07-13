using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform InventoryParent; // Main Inventory
    public Transform HotbarParent; // Hotbar
    InventoryManager inventory; // Inventory instance
    InventorySlot[] slots;
    InventorySlot[] hotbarSlots;
    int totalSpace; // Sum of hotbar and inventory slots
    public GameObject Player;
    PlayerLook playerLook;

    void Start()
    {
        inventory = InventoryManager.instance;
        slots = InventoryParent.GetComponentsInChildren<InventorySlot>();
        hotbarSlots = HotbarParent.GetComponentsInChildren<InventorySlot>();
        totalSpace = slots.Length + hotbarSlots.Length;
        playerLook = Player.GetComponent<PlayerLook>();
}

    public void EnableRemoveButton()
    {
        foreach (InventorySlot slot in hotbarSlots)
        {
            if (slot.isFilled && playerLook.isInventoryActive == true)
            {
                slot.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                slot.transform.GetChild(2).gameObject.SetActive(false);
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.isFilled)
            {
                slot.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                slot.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }


    public void UpdateUI() // Adds items to inventory screen
    {
        int j = 0;

        for (int i = 0; i < totalSpace; i++)
        {
            if (i < inventory.items.Count && i < inventory.HotbarSpace)
            {
                if (!hotbarSlots[i].isFilled || (hotbarSlots[i].item.amount <= hotbarSlots[i].item.stackLimit))
                {
                    hotbarSlots[i].AddItem(inventory.items[i]);
                    EnableRemoveButton();
                }
            }

            if (i < inventory.items.Count && j < inventory.InventorySpace && i > inventory.HotbarSpace)
            {
                if (!slots[i].isFilled || (slots[i].item.amount <= slots[i].item.stackLimit))
                {
                    slots[i].AddItem(inventory.items[i]);
                    EnableRemoveButton();
                    j++;
                }
            }
        }
    }
}