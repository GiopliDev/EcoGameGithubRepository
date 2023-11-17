using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AlmanacManager : MonoBehaviour
{
    public static string JSON_DIR = "./Assets/almanac.json";
    Almanac almanac;
    bool isShown = false;

    void Start()
    {
        string JSON_DATA = File.ReadAllText(AlmanacManager.JSON_DIR);
        Debug.Log(JSON_DATA.Length);
        this.almanac = JsonUtility.FromJson<Almanac>(JSON_DATA);
        //Debug.Log(this.almanac.Collection[0]);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(isShown) this.HideAlmanac();
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

struct Almanac
{
    public CollectionElement[] Collection { get; set; }
}
struct CollectionElement
{
    public string ID { get; set; }
    public string Image { get; set; } // ./img/pianta.png
    public string Info { get; set; }
    public string Name { get; set; }
    public string Section { get; set; }
    public bool Unlocked { get; set; }
}