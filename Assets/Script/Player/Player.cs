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
    public GameObject almanac;

    [Header("Main Tools")]
    private int toolNumber = 3;
    public int water = 100;
    public GameObject[] tools;
    public int equippedToolId = -1;

    [Header("Inventory Links")]
    public InventoryManager inventoryManager;

    private float whenLastHit = 0f;
    void Start()
    {
        hp = maxHp;
        healthBar = GameObject.Find("HealthBar");
        RefreshBars();
        lastCollision = this.GetComponent<Collider2D>();
        tools = new GameObject[toolNumber];
        tools[0] = GameObject.Find("WateringCan");
        tools[1] = GameObject.Find("Hoe");
        tools[2] = GameObject.Find("Shovel");
        almanac.SetActive(true);
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
            if (lastCollision.gameObject.tag == "SceneObject")
            {
                pickUpManager();
            }
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
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map.mapOpened == true)
            {
                map.closeMap();
            }
            else
            {
                map.openMap();
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            almanac.GetComponent<AlmanacManager>().ToggleAlmanac();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            almanac.GetComponent<AlmanacManager>().HideAlmanac();
        }
    }
    private void pickUpManager()
    {
        //e non ho niente in mano
        if (hasObjectInHand == false && isColliding)
        {
                //controllo se sto toccando un oggetto di scena che ha la bool "isGrabbable" = true
                if (lastCollision.gameObject.GetComponent<sceneObjectManager>().isGrabbable)
                {
                    hasObjectInHand = true;

                    //assegno l'oggetto che ho in mano
                    objectInHand = lastCollision.gameObject;

                    objectInHand.GetComponent<sceneObjectManager>().objectPicked();
                }
        }
        else if (objectInHand.tag == "SceneObject")
        {
                hasObjectInHand = false;
                objectInHand.GetComponent<sceneObjectManager>().releaseObject();
                objectInHand = null;
        }
    }
    public void plant()
    {
       Instantiate(Resources.Load("Piante/woodPlant"));
       Debug.Log("Piantata");
        
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

    void RefreshBars()
    {
        Slider bar = healthBar.GetComponent<Slider>();
        bar.maxValue = maxHp;
        bar.value = hp;
    }

    private void CheckEnemy(Collision2D coll)
    {
        if (coll.gameObject.tag != "Enemy") return;
        if (this.whenLastHit + 1f > Time.realtimeSinceStartup) return;
        this.whenLastHit = Time.realtimeSinceStartup;
        this.hp -= 1; // HEALTH TO BE REDUCED BY SOME CONSTANT OTHERWHERE
        this.RefreshBars();
        if (this.hp <= 0)
        {
            Debug.Log("Player is dead (Player::OnCollisionEnter2D)");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        lastCollision = collision.collider;
        CheckEnemy(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckEnemy(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
