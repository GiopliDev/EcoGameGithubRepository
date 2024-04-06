using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Pianta")]
public class Pianta : ScriptableObject

{
    private float whenLastGet = 0f;
    public string nome;
    public Sprite[] fasi = new Sprite[4]; //icona,fase1,fase2,fase3
    public int IDCrescita=0;
    public int progressNeeded;
    public int progress;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        //0.06 = 60 millisecondi
        if (this.whenLastGet + 1f <= Time.realtimeSinceStartup)
        {
            this.whenLastGet = Time.realtimeSinceStartup;
            if (progress < progressNeeded)
            {
                progress++;
            }
        }

        if (progress == progressNeeded && IDCrescita < 3) //< 3 perche le fasi massime sono 4
        {
            IDCrescita++;
            changePhase(IDCrescita);
        }

    }

    public void changePhase(int fase)
    {
        this.GetComponent<SpriteRenderer>().sprite = fasi[fase];
    }
    public void startGrowth()
    {
        changePhase(0);
    }

    
}
