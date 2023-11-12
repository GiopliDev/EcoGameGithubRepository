using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    // Variabili associate ai componenti
    Rigidbody2D rb;
    public bool isColliding;
    public Collider2D lastCollision;

    public Transform lookingDirection;

    public bool hasObjectPickedUp = false;
    public GameObject objectInHand;

    public Tilemap world;

    public float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movimento del player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement(horizontal, vertical);

        //gestione dell'oggetto che porta con se il player
        Vector3 tilePos = world.WorldToCell(lookingDirection.position);
        tilePos.z = 0;
        tilePos.x += 0.5f;
        tilePos.y += 0.5f;
        pickUpManager();
        
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

    public void movement(float horizontal,float vertical)
    {
        if (horizontal < 0 || horizontal > 0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            if (horizontal < 0)
            {
                lookingDirection.localPosition = new Vector2(-1.25f, -0.1f);
            }
            else
            {
                lookingDirection.localPosition = new Vector2(1.25f, -0.1f);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (vertical < 0 || vertical > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            if (vertical < 0)
            {
                lookingDirection.localPosition = new Vector2(0f, -0.6f);
            }
            else
            {
                lookingDirection.localPosition = new Vector2(0f, 0.6f);
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    public void pickUpManager() //il tile che si occupa di trasportare gli oggetti 
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
}
