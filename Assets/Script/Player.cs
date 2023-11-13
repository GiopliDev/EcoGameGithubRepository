using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Collision and Rb Settings")]
    public bool isColliding;
    public Collider2D lastCollision;

    [Header("PickUp Manager")]
    public bool hasObjectPickedUp = false;
    public GameObject objectInHand;

    [Header("Tilemap")]
    public Tilemap world;

    [Header("Health Settings")]
    public float maxHp = 10f;
    public float hp;
    public float hpRegen = 0.5f;

    [Header("Player Bars")]
    public GameObject healthBar;

    void Start()
    {
        lastCollision = new Collider2D();
        hp = maxHp;
        healthBar = GameObject.Find("HealthBar");
        healthBar.GetComponent<Slider>().maxValue = maxHp;
        healthBar.GetComponent<Slider>().value = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastCollision.gameObject.tag == "Enemy" && isColliding)
        {
            hp -= 10 * Time.deltaTime;
            healthBar.GetComponent<Slider>().value = hp;
        }
        pickUpManager();
    }

    private void pickUpManager() //il tile che si occupa di trasportare gli oggetti 
    {    //se premo E
        if (Input.GetKeyDown(KeyCode.E))
        {
            //e non ho niente in mano
            if (hasObjectPickedUp == false)
            {
                //controllo se sto toccando un oggetto di scena che ha la bool "isGrabbable" = true
                if (lastCollision.gameObject.GetComponent<sceneObjectManager>().isGrabbable && isColliding)
                {
                    hasObjectPickedUp = true;

                    //assegno l'oggetto che ho in mano
                    objectInHand = lastCollision.gameObject;


                    objectInHand.GetComponent<sceneObjectManager>().inPlayerHand();

                }
            }
            else //quando ho premuto E,se avevo gia qualcosa in mano
            {

                if (objectInHand.GetComponent<sceneObjectManager>().releaseObject())
                {
                    hasObjectPickedUp = false;
                }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        lastCollision = collision.collider;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
