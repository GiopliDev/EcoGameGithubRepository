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
            StartCoroutine(MoveObject(collision.gameObject.transform.position));
        }
    }

    IEnumerator MoveObject(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition); // Calcola la distanza tra l'oggetto e la posizione del giocatore
        float step = moveSpeed * Time.deltaTime; // Calcola la quantità di movimento per frame

        while (transform.position != targetPosition)
        {
            // Sposta l'oggetto verso la posizione del giocatore in modo graduale
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            yield return null; // Attendere fino al prossimo frame
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        inventoryManager.AddItem(item);
        Destroy(gameObject);
    }
}
