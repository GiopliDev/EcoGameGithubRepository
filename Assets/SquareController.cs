using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform otherSquare;
    public Text t;
    void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("entra???");
        t.enabled = true;
        t.text = "entra???";
        if (Input.GetKeyDown(KeyCode.E)) {
            if (other.CompareTag("Player"))
            {
                other.transform.position = new Vector2(otherSquare.position.x, otherSquare.position.y);
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("non entra!!!");
        t.text = "NON ENTRAA!!!";
        //t.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
