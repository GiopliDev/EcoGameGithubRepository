using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    public bool mapOpened = false;
    public GameObject playerIcon;
    public Transform playerPos;

    public float proportion = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mapOpened)
        {
            playerIcon.transform.localPosition = new Vector3(playerPos.position.x / proportion, playerPos.position.y / proportion);
        }    
    }

    public void openMap()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        playerIcon.GetComponent<SpriteRenderer>().enabled = true;
        mapOpened = true;
    }

    public void closeMap()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        playerIcon.GetComponent<SpriteRenderer>().enabled = false;
        mapOpened = false;
    }
}
