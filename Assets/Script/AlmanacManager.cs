using UnityEngine;

public class AlmanacManager : MonoBehaviour
{
    private PlayerMovement playerMV;
    public Almanac almanac;
    public GameObject almanacContainer, almanacTabs, almanacBody;

    public const string JSON_DIR = "./Assets/almanac.json";
    public bool isShown = false;

    void Start()
    {
        string JSON_DATA = System.IO.File.ReadAllText(JSON_DIR);
        this.almanac = JSONParser.FromAsObject<Almanac>(JSON_DATA);
        this.playerMV = GameObject.Find("Player").GetComponent<PlayerMovement>();

        this.almanacContainer = GameObject.Find("Almanac");
        this.almanacContainer.transform.position = new Vector3(0, 0, -9);
        this.almanacTabs = GameObject.Find("AlmanacTabs");
        this.almanacBody = GameObject.Find("AlmanacBody");
        //////// TODO: GameObject.Find("ExitAlmanac").OnClick(HideAlmanac);
        this.HideAlmanac();
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
        this.playerMV.canMove = false;
        this.almanacBody.SetActive(true);
        this.almanacTabs.SetActive(true);
        Debug.Log("Shown Almanac");
    }
    void HideAlmanac()
    {
        this.isShown = false;
        this.playerMV.canMove = true;
        this.almanacBody.SetActive(false);
        this.almanacTabs.SetActive(false);
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