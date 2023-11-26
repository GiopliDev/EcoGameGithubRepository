using UnityEngine;

public class AlmanacManager : MonoBehaviour
{
    private PlayerMovement playerMV;
    public Almanac almanac = new();
    public GameObject almanacContainer;

    [Header("Almanacco")]
    public const string JSON_DIR = "./Assets/almanac.json";
    public bool isShown = false;

    void Start()
    {
        string JSON_DATA = System.IO.File.ReadAllText(JSON_DIR);
        this.almanac = JSONParser.FromAsObject<Almanac>(JSON_DATA);
        playerMV = GameObject.Find("Player").GetComponent<PlayerMovement>();
        almanacContainer = GameObject.Find("Almanac");
        almanacContainer.SetActive(false);
    }
    void Update()
    {
        almanacContainer.transform.position = playerMV.transform.position;
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
        playerMV.canMove = false;
        almanacContainer.SetActive(true);
        Debug.Log("Shown Almanac");
    }
    void HideAlmanac()
    {
        this.isShown = false;
        almanacContainer.SetActive(false);
        playerMV.canMove = true;
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