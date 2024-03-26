using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using Unity.VisualScripting;
using Unity.Properties;

public class ExtendsFogCircle : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public bool playerIsClose;
    public float wordSpeed;
    public VisualEffect fog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                expandFogCircle();
                dialoguePanel.SetActive(false);
                dialogueText.text = "";
                index = 0;
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }

    IEnumerator Typing()
    {

        foreach (char letter in dialogue[index].ToCharArray())
        {
            if(playerIsClose)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
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
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            index = 0;
        }
    }

    private void expandFogCircle()
    {
        fog.SetFloat("CircleSize", fog.GetFloat("CircleSize")+2);
    }    
}
