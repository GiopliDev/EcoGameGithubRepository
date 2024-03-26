using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public Item item;
    public InventoryManager inventoryManager;

    public float moveSpeed; // Velocità di movimento dell'oggetto

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveObject(collision));
        }
    }

    IEnumerator MoveObject(Collider2D collision)
    {
        float step = moveSpeed * Time.deltaTime; // Calcola la quantità di movimento per frame

        while (transform.position != collision.gameObject.transform.position)
        {
            // Sposta l'oggetto verso la posizione del giocatore in modo graduale
            transform.position = Vector3.MoveTowards(transform.position, collision.gameObject.transform.position, step);
            yield return null; // Attendere fino al prossimo frame
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        inventoryManager.AddItem(item);
        Destroy(gameObject);
    }
}
