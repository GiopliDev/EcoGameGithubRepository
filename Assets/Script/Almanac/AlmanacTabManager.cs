using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacTabManager : MonoBehaviour
{
    public AlmanacManager almanac;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUpAsButton()
    {
        switch (this.name)
        {
            case "CollectionTab":
                almanac.Select("collection");
                break;
            case "TutorialTab":
                almanac.Select("tutorial");
                break;
            case "MissionTab":
                almanac.Select("mission");
                break;
            case "ExitAlmanac":
                almanac.HideAlmanac();
                break;
            default:
                throw new NotImplementedException($"Cannot find (name={this.name})");
        }
    }
}
