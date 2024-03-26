using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;
    public Image customCursor;

    public Slot[] craftingSlots;

    public List<Item> itemList;
    public Recipe[] recipes;
    public Item[] recipesResults;
    public Slot resultSlot;
    public InventoryManager inventoryManager;
    public int countSlot;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) 
        {
            if (currentItem != null) 
            { 
                customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (Slot slot in craftingSlots) 
                {
                    var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mouseWorldPos.z = 0f;
                    float dist=Vector3.Distance(mouseWorldPos, slot.transform.position);
                    if (dist < shortestDistance && slot.item==null) 
                    { 
                        shortestDistance = dist;
                        nearestSlot = slot;

                    }
                    
                }
                if (nearestSlot != null)
                {
                    nearestSlot.gameObject.SetActive(true);
                    nearestSlot.GetComponent<Image>().sprite = currentItem.sprite;
                    nearestSlot.item = currentItem;
                    itemList[nearestSlot.index] = currentItem;
                }

                currentItem = null;

                CheckForCreatedRecipes();
            }
        }
    }
    void CheckForCreatedRecipes() {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach (Item item in itemList) {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else {
                currentRecipeString += "null";
            }
        }
        for (int i = 0; i < recipes.Length; i++) {
            if (recipes[i].recipeText == currentRecipeString) { 
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipesResults[i].sprite;
                recipes[i].isThere = true;
                //bisogna aggiungere lo slot alla parte delle ricette
                resultSlot.item = recipesResults[i];
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        countSlot--;
        inventoryManager.AddItem(slot.item);
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
        
    }

    public void onMouseDownItem(InventorySlot slot) 
    {
        Item item = slot.item;
        Debug.Log(slot.item);
        if (currentItem == null && slot.item!=null && countSlot!=9) {
            currentItem = item;
            countSlot++;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.sprite;
            inventoryManager.ToggleItem(slot.item);
        }
    }
}
