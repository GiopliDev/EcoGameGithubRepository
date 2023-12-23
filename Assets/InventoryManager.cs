using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;
    public GameObject InventoryItemPrefab;
    public void AddItem(Item item) { 
        //controlla per uno slot vuoto poi inserisce 
        for (int i = 0; i < InventorySlots.Length; i++) { 
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemSlot=slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot==null)
            {
                SpawnItem(item, slot);
                return;
            }
        }
    }
    public void SpawnItem(Item item,InventorySlot slot) { 
        
        GameObject newItemGO =Instantiate(InventoryItemPrefab,slot.transform);
        InventoryItem inventoryItem= newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }
}
