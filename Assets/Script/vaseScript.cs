using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isColliding = false;
    private GameObject lastCollision;
    public PlantAction plant;
    public PlantManager plantManager;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F) && isColliding && plantManager.seedInInventory()) {
            plant.startGrowth(plantManager.plant());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Collide");
            isColliding = true;
            lastCollision = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
}
