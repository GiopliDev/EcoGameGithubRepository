using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;  
    public Item[] itemsToPickup;

    //o stick(0) o sasso(1) o semi(2)
    public void PickupItem(int id) {
        bool result =inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("item aggiunto");
        }
        else Debug.Log("item non aggiunto, inventario pieno");
    }
}
