using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;
    public InventorySlot[] craftingSlots;
    public GameObject InventoryItemPrefab;
    int SelectedSlot=-1;

    void ChangeSelectedSlot(int newValue) {
        if (SelectedSlot >= 0)
        {
            InventorySlots[SelectedSlot].Deselect();
        }
        InventorySlots[newValue].Select();
        SelectedSlot = newValue;
        
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
            if (itemSlot != null &&
                itemSlot.item ==item &&
                itemSlot.count<4
                ){
                itemSlot.count++;
                itemSlotCraft.count++;
                itemSlot.RefreshCount();
                itemSlotCraft.RefreshCount();

                return true;
            }
        }

        //controlla per uno slot vuoto poi inserisce 
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            InventorySlot slotCraft = craftingSlots[i];
            if (itemSlot == null)
            {
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
}
