using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;  
    public Item[] itemsToPickup;

    //o stick(0) o sasso(1) o semi(2)
    public void PickupItem(int id) {
        inventoryManager.AddItem(itemsToPickup[id]);    
    }
}
