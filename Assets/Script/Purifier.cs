using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purifier : MonoBehaviour
{
    public GameObject purifierPanel;

    public bool playerIsClose;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!purifierPanel.activeInHierarchy)
            {
                purifierPanel.SetActive(true);
            }
            else
            {
                purifierPanel.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            purifierPanel.SetActive(false);
        }
    }
}
