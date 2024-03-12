using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pozzoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isColliding = false;
    public Collider2D lastCollision;
    public TMP_Text lblWater;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && lastCollision.gameObject.tag == "Player")
        {
            Player script = lastCollision.gameObject.GetComponent<Player>();
            if (Input.GetKey(KeyCode.E))
            {
                if (script.water + (int)(100 * Time.deltaTime) <= 100)
                {
                    script.water += (int)(100*Time.deltaTime);
                    lblWater.text = script.water + "%";
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
