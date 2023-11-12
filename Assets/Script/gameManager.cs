using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class gameManager : MonoBehaviour
{
    public bool overlayActive = true;
    public Vector3 overlayPos;
    public Tilemap world;
    public GameObject tileOverlay;
    public GameObject objectInHand;
    public Color normalColor;
    void Start()
    {
        normalColor = tileOverlay.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (overlayActive)
        {
            tileOverlayManager(overlayPos);
        }
    }

    public void tileOverlayManager(Vector3 pos)
    {
        Vector3 tilePos = world.WorldToCell(pos);
        tilePos.z = 0;
        tilePos.x += 0.5f;
        tilePos.y += 0.5f;
        tileOverlay.transform.position = tilePos;
        
    }

    public void tileOverlayStart()
    {
        overlayActive = true;
        tileOverlay.GetComponent<SpriteRenderer>().enabled = true;
        
    }

    public void tileOverlayEnd()
    {
        overlayActive = false;
        tileOverlay.GetComponent<SpriteRenderer>().enabled = false;
    }

    public IEnumerator invalidPlacementBlink()
    {
        tileOverlay.GetComponent<SpriteRenderer>().color = new Color(255,0,0,0.69f);
        yield return new WaitForSeconds(0.5f);
        tileOverlay.GetComponent<SpriteRenderer>().color = normalColor;
    }
}
