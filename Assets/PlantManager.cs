using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] seeds;
    public Pianta[] plants;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool seedInInventory() {
        Debug.Log(inventoryManager.getObjectInHand());
        return inventoryManager.getObjectInHand().type == ItemType.Seed;
    }
    public Pianta plant() {
        int count = 0;
        foreach (Item seed in seeds)
        {
            if(inventoryManager.getObjectInHand().itemName== seed.itemName)
            {
                return plants[count];
            }
            count++;
        }
        return null;
    }
}
