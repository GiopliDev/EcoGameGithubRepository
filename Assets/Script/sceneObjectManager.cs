using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class sceneObjectManager : MonoBehaviour
{
    public bool occupiesTiles;
    public bool isGrabbable;
    public bool isInPlayerHand = false;
    public bool isColliding = false;

    public gameManager gm;
    private Tilemap world;

    private GameObject player;
    private GameObject lookingDirection;
    private GameObject lastCollision;

    public void inPlayerHand()
    {
        gameObject.layer = LayerMask.NameToLayer("ignorePlayerCollision");
        isInPlayerHand = true;
        transform.position = lookingDirection.transform.position;
        gm.overlayPos = transform.position;
        gm.tileOverlayStart();
    }

    public bool releaseObject()
    {
        if (isColliding)
        {
            gm.invalidPlacementBlink();
            Debug.Log(lastCollision.name + " dice: Nuh uh");
            return false;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            Vector3 tilePos = world.WorldToCell(transform.position);
            tilePos.x += 0.5f;
            tilePos.y += 0.5f;
            tilePos.z = 0;
            transform.position = tilePos;
            isInPlayerHand = false;
            gm.tileOverlayEnd();
            return true;
        }

    }

    public void Start()
    {
        player = GameObject.Find("Player");
        lookingDirection = GameObject.Find("lookingDirection");
        world = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }
    public void Update()
    {
        if (isInPlayerHand)
        {
            inPlayerHand();  
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        lastCollision = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

}
