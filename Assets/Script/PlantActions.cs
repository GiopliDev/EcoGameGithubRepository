using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorPlant : MonoBehaviour
{
    public Pianta pianta;
    // Start is called before the first frame update
    public void changePhase(int fase)
    {
        this.GetComponent<SpriteRenderer>().sprite = pianta.fasi[fase];
    }
    public void startGrowth()
    {
        changePhase(0);
        while (pianta.IDCrescita != 3)
        {
            cresciPianta();
        }
    }
    IEnumerator cresciPianta()
    {
        yield return new WaitForSeconds(pianta.progressNeeded);
        pianta.IDCrescita++;
        changePhase(pianta.IDCrescita);
    }
}
