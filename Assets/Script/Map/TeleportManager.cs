using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    private float whenEntered = 0;
    private bool isTeleporting = false;
    private GameObject entity;

    public GameObject teleportTo;
    //    public float timeToTeleport = 6f;
    public float timeToStartTeleport = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTeleporting)
        {
            this.entity.transform.position = new Vector3(
                this.teleportTo.transform.position.x,
                this.teleportTo.transform.position.y,
                this.entity.transform.position.z
                );
            isTeleporting = false;
            return;
        }
        if (whenEntered == 0) return;
        if (whenEntered + timeToStartTeleport > Time.realtimeSinceStartup) return;
        //Quando collido da più di {timeToStartTeleport} secondi 
        isTeleporting = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        this.whenEntered = Time.realtimeSinceStartup;
        this.entity = collision.gameObject;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        whenEntered = 0;
    }
}
