using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Pianta : MonoBehaviour
{
    public Sprite iconaInventario;
    public Sprite fase1;
    public Sprite fase2;
    public Sprite fase3;
    public int IDCrescita=0;
    public int progres;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
    }
    public void startGrowth()
    {
        progres = 0;
        this.GetComponent<SpriteRenderer>().sprite = fase1;
    }
}
