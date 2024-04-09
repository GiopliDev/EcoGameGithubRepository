using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAction : MonoBehaviour
{
    private Pianta plant;
    public SpriteRenderer spritePlant;
    // Start is called before the first frame update
    public void startGrowth(Pianta plant)
    {
        Debug.Log("Comincia a creshes");
        this.plant = plant;
        changePhase(0);
        do
        {
            StartCoroutine(growPlant());
        } while (plant.IDgrowth != 3);
    }
    public void changePhase(int fase)
    {
        spritePlant.sprite = plant.fases[fase];
    }
    
    IEnumerator growPlant()
    {
        Debug.Log("CRESCEEE");
        yield return new WaitForSeconds(plant.progressNeeded);
        plant.IDgrowth++;
        changePhase(plant.IDgrowth);
    }
}
