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
    public bool hasObjectInHand = false;
    public GameObject objectInHand;
    public Transform handPos;

    [Header("Tilemap")]
    public Tilemap world;
    public mapManager map;

    [Header("Health Settings")]
    public float maxHp = 10f;
    public float hp;
    public float hpRegen = 0.5f;

    [Header("Player Bars")]
    public GameObject healthBar;

    [Header("Main Tools")]
    private int toolNumber = 3;
    public GameObject[] tools;
    public int equippedToolId = -1;

    void Start()
    {
        lastCollision = new Collider2D();
        hp = maxHp;
        healthBar = GameObject.Find("HealthBar");
        healthBar.GetComponent<Slider>().maxValue = maxHp;
        healthBar.GetComponent<Slider>().value = maxHp;
        lastCollision = this.GetComponent<Collider2D>();
        tools = new GameObject[toolNumber];
        tools[0] = GameObject.Find("WateringCan");
        tools[1] = GameObject.Find("Hoe");
        tools[2] = GameObject.Find("Shovel");
    }

    // Update is called once per frame
    void Update()
    {
        if (hasObjectInHand)
        {
            objectInHand.transform.position = handPos.transform.position;
        }
        //se premo E
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickUpManager();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            toggleTool(0); //annaffiatoio
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            toggleTool(1); //zappa
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            toggleTool(2); //pala
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hasObjectInHand)
            {
                if (objectInHand.gameObject.name == "Seme")
                {
                    plant();//pianta
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map.mapOpened == false)
            {
                map.openMap();
            }
            else
            {
                map.closeMap();
            }
        }
         

    }
    private void pickUpManager()
    {
        //e non ho niente in mano
        if (hasObjectInHand == false)
        {
            //controllo se sto toccando un oggetto di scena che ha la bool "isGrabbable" = true
            if (lastCollision.gameObject.GetComponent<sceneObjectManager>().isGrabbable && isColliding)
            {
                hasObjectInHand = true;

                //assegno l'oggetto che ho in mano
                objectInHand = lastCollision.gameObject;
                

                objectInHand.GetComponent<sceneObjectManager>().objectPicked();

            }
        }
        else //quando ho premuto E,se avevo gia qualcosa in mano
        {

            if (objectInHand.GetComponent<sceneObjectManager>().releaseObject())
            {
                hasObjectInHand = false;
            }

        }
    }
    public void plant() 
    {
        //controlla se l'ultimo oggetto al quale si è avvicinato è un vaso
        if (lastCollision.GetComponent<sceneObjectManager>().isPlantable && isColliding)
        {
            Debug.Log("Entraaaa!!");
            //controlla se non è già stato piantato e se sta collidendo
            if ( lastCollision.gameObject.name=="Vase")
            {
                //sposta il seme che ho in mano sulla pianta
                objectInHand.transform.position=lastCollision.GetComponentInChildren<Transform>().position;
                //il seme inizia a crescere
                Debug.Log(lastCollision.name + " vuole crescere");
                objectInHand.GetComponent<Pianta>().startGrowth();
                //viene eliminato ciò che ho in mano
                hasObjectInHand = false;
            }
        }
    }

    public void toggleTool(int toolId) //N.B: il player deve avere la mano libera per prender in mano l'utensile 
    {
        if (hasObjectInHand == false) //se non ho niente in mano
        {
            equippedToolId = toolId; //l'id del tool che andrò ad equipaggiare = toolId
            hasObjectInHand = true;
        }
        else if (equippedToolId != -1) //se invece ho qualcosa in mano ed è un utensile
        {

            //metto via l'utensile
            objectInHand.transform.position = GameObject.Find("toolPos").transform.position;
            objectInHand.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log(objectInHand.name + " messo via");


            if (equippedToolId == toolId) //se è lo stesso che avevo prima
            {
                equippedToolId = -1; //lo metto via
                hasObjectInHand = false;
            }
            else //se non lo è
            {
                equippedToolId = toolId; //lo equipaggio
            }
        }

        //questa condizione serve per settare gli oggetti di scena appena la funzione ha deciso cosa deve
        //avere in mano il player
        if (equippedToolId > -1) 
        {
            objectInHand = tools[equippedToolId];
            objectInHand.GetComponent<SpriteRenderer>().enabled = true;
            hasObjectInHand = true;
            Debug.Log(objectInHand.name + " equipaggiato");
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
