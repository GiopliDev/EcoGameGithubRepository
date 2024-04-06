using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaseScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject toolbar;
    public bool isColliding = false;
    public GameObject lastCollision;
    void Start()
    {
        toolbar = GameObject.Find("Toolbar");
    }

    void Update()
    {
        
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
