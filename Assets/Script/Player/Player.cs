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

    private float whenLastHit = 0f;
    void Start()
    {
        hp = maxHp;
        healthBar = GameObject.Find("HealthBar"); // ma se il parametro è pubblico, perché prenderlo così
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
            almanac.transform.position = this.gameObject.transform.position;
            almanac.GetComponent<AlmanacManager>().ToggleAlmanac();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            almanac.GetComponent<AlmanacManager>().HideAlmanac();
        }
        if(lastCollision != null && lastCollision.gameObject != null)
            CheckEnemy(lastCollision.gameObject.tag);

    }
    private void pickUpManager()
    {
        //e non ho niente in mano
        if (hasObjectInHand == false && isColliding)
        {
            if (lastCollision.gameObject.name == "Vase")//E' necessario??
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
        }
        else //quando ho premuto E,se avevo gia qualcosa in mano
        {
            hasObjectInHand = false;
        }
    }
    public void plant()
    {
        //controlla se l'ultimo oggetto al quale si è avvicinato è un vaso
        if (isColliding && lastCollision.GetComponent<sceneObjectManager>().isPlantable)
        {
            Debug.Log("Entraaaa!!");
            //controlla se non è già stato piantato e se sta collidendo
            if (lastCollision.gameObject.name == "Vase")
            {
                //sposta il seme che ho in mano sulla pianta
                objectInHand.transform.position = lastCollision.GetComponentInChildren<Transform>().position;
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

    void RefreshBars()
    {
        Slider bar = healthBar.GetComponent<Slider>();
        bar.maxValue = maxHp;
        bar.value = hp;
    }

    private void CheckEnemy(string tag)
    {
        if (tag != "Enemy") return;
        if (this.whenLastHit + 1f > Time.realtimeSinceStartup) return;
        this.whenLastHit = Time.realtimeSinceStartup;
        this.hp -= 1; // HEALTH TO BE REDUCED BY SOME CONSTANT OTHERWHERE
        this.RefreshBars();
        if (this.hp <= 0)
        {
            Debug.Log("Player is dead (Player::CheckEnemy)");
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
