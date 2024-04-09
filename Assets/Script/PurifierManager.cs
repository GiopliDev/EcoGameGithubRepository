using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurifierManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Item currentItem;
    public Image customCursor;

    public Slot purifierSlot;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipesResults;
    public Slot resultSlot;
    public InventoryManager inventoryManager;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                if (purifierSlot.item == null)
                {
                    purifierSlot.gameObject.SetActive(true);
                    purifierSlot.GetComponent<Image>().sprite = currentItem.sprite;
                    purifierSlot.item = currentItem;
                    itemList[purifierSlot.index] = currentItem;
                }

                currentItem = null;

                CheckForCreatedRecipes();
            }
        }
    }
    void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach (Item item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }
        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                StartCoroutine(EseguiDopoAttesa(i));
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        purifierSlot = null;
        inventoryManager.AddItem(slot.item);
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();

    }
    IEnumerator EseguiDopoAttesa(int i)
    {
        yield return new WaitForSeconds(3f);
        resultSlot.gameObject.SetActive(true);
        resultSlot.GetComponent<Image>().sprite = recipesResults[i].sprite;
        resultSlot.item = recipesResults[i];

    }
        public void onMouseDownItem(InventorySlot slot)
    {
        Item item = slot.item;
        if (currentItem == null && slot.item != null && purifierSlot.item==null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.sprite;
            inventoryManager.ToggleItem(slot.item);
        }
    }
}
