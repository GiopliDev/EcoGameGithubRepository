using UnityEngine;

public class AlmanacManager : MonoBehaviour
{
    private PlayerMovement player;

    [Header("Almanacco")]
    public static string JSON_DIR = "./Assets/almanac.json";
    public Almanac almanac = new();
    public bool isShown = false;

    void Start()
    {
        string JSON_DATA = System.IO.File.ReadAllText(AlmanacManager.JSON_DIR);
        this.almanac = JSONParser.FromAsObject<Almanac>(JSON_DATA);
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
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
        player.canMove = false;
        Debug.Log("Shown Almanac");
    }
    void HideAlmanac()
    {
        this.isShown = false;
        player.canMove = true;
        Debug.Log("Hidden Almanac");
    }
}
public class Almanac
{
    public CollectionElement[] Collection { get; set; }
}
public class CollectionElement
{
    public string ID { get; set; }
    public string Image { get; set; } // ./img/pianta.png
    public string Info { get; set; }
    public string Name { get; set; }
    public string Section { get; set; }
    public bool Unlocked { get; set; }
}