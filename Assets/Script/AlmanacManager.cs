using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacManager : MonoBehaviour
{
    public static string JSON_DIR = "./Assets/almanac.json";
    Almanac almanac;
    bool isShown = false;
    void Start()
    {
        string JSON_DATA = System.IO.File.ReadAllText(AlmanacManager.JSON_DIR);
        //Debug.Log(JSON_DATA);
        this.almanac = JSONParser.FromAsObject<Almanac>(JSON_DATA);
        Debug.Log(this.almanac.Collection[1].ID);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isShown) this.HideAlmanac();
            else this.ShowAlmanac();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && this.isShown) this.HideAlmanac();
    }
    void ShowAlmanac()
    {
        this.isShown = true;
    }
    void HideAlmanac()
    {
        this.isShown = false;
    }
}
class Almanac
{
    public CollectionElement[] Collection { get; set; }
}
class CollectionElement
{
    public string ID { get; set; }
    public string Image { get; set; } // ./img/pianta.png
    public string Info { get; set; }
    public string Name { get; set; }
    public string Section { get; set; }
    public bool Unlocked { get; set; }
}