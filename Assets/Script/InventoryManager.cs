using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;
    public InventorySlot[] craftingSlots;
    public InventorySlot[] purifierSlots;
    public GameObject InventoryItemPrefab;
    public int SelectedSlot=-1;

    void ChangeSelectedSlot(int newValue) {
        if (SelectedSlot >= 0)
        {
            InventorySlots[SelectedSlot].Deselect();
        }
        InventorySlots[newValue].Select();
        SelectedSlot = newValue;
        //Debug.Log("Slot Selezionato: " + SelectedSlot + ", Oggetto nello Slot: " + InventorySlots[SelectedSlot].item.itemName);
        
    }
    public void Start()
    {
        ChangeSelectedSlot(0);
    }

    public void Update()
    {
        if (Input.inputString!=null) {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number>0 && number<7) { 
                ChangeSelectedSlot((int)number-1);
            }
        }
    }



    public bool AddItem(Item item)
    {
        //controlla per uno slot stackable
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            InventorySlot slotCraft = craftingSlots[i];
            InventoryItem itemSlotCraft = slotCraft.GetComponentInChildren<InventoryItem>();
            InventorySlot slotPurifier = purifierSlots[i];
            InventoryItem itemSlotPurifier = slotPurifier.GetComponentInChildren<InventoryItem>();
            if (itemSlot != null &&
                itemSlot.item ==item &&
                itemSlot.count<4
                ){
                itemSlot.count++;
                itemSlotCraft.count++;
                itemSlotPurifier.count++;
                itemSlot.RefreshCount();
                itemSlotCraft.RefreshCount();
                itemSlotPurifier.RefreshCount();
                return true;
            }
        }

        //controlla per uno slot vuoto poi inserisce 
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            InventorySlot slotCraft = craftingSlots[i];
            InventorySlot slotPurifier = purifierSlots[i];
            if (itemSlot == null)
            {
                SpawnItem(item, slotPurifier);
                SpawnItem(item, slotCraft);
                SpawnItem(item, slot);
                return true;
            }
        }
        return false;
    }
    public void SpawnItem(Item item, InventorySlot slot)
    {

        GameObject newItemGO = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
        slot.item = item;
        
    }
    
    public bool ToggleItem(Item item)
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            InventorySlot slotCraft = craftingSlots[i];
            InventoryItem itemSlotCraft = slotCraft.GetComponentInChildren<InventoryItem>();
            InventorySlot slotPurifier = purifierSlots[i];
            InventoryItem itemSlotPurifier = slotPurifier.GetComponentInChildren<InventoryItem>();
            if (itemSlot != null &&
                itemSlot.item == item &&
                itemSlot.count > 0
                )
            {
                itemSlot.count--;
                itemSlotCraft.count--;
                itemSlotPurifier.count--;
                if (itemSlot.count == 0)
                {
                    slot.item = null;
                    slotCraft.item = null;
                    slotPurifier.item = null;
                }
                itemSlot.RefreshCount();
                itemSlotCraft.RefreshCount();
                itemSlotPurifier.RefreshCount();
                return true;
            }
        }
        return false;
    }
}
