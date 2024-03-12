using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public Item item;

    public InventoryManager inventoryManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            var playerPosition=Camera.main.ScreenToWorldPoint(collision.transform.position);
            this.gameObject.transform.Translate(playerPosition);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");
        inventoryManager.AddItem(item);
        Destroy(gameObject);
    }
}
