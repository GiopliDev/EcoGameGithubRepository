using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacTabManager : MonoBehaviour
{
    private static AlmanacManager almanac = null;
    // Start is called before the first frame update
    void Start()
    {
        if(almanac == null) almanac = GameObject.Find("Almanac").GetComponent<AlmanacManager>();
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
                almanac.CollectionTabSelected();
                break;
            case "TutorialTab":
                almanac.TutorialTabSelected();
                break;
            case "MissionTab":
                almanac.MissionTabSelected();
                break;
            case "ExitAlmanac":
                almanac.HideAlmanac();
                break;
            default:
                throw new NotImplementedException($"Cannot find (name={this.name})");
        }
    }
}
