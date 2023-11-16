using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.JSON;
using System.IO;

public class AlmanacManager : MonoBehaviour
{
    public static string JSON_DIR = "";
    Almanac almanac;
    // Start is called before the first frame update
    void Start()
    {
        Almanac? tempAl = JsonSerializer.Deserialize<Almanac>(
            File.ReadAllText(JSON_DIR)
        );
        if (tempAl != null) { throw new FileNotFoundException(tempAl.ToString()); }
        this.almanac = tempAl;
    }

    // Update is called once per frame
    void Update()
    {
        
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