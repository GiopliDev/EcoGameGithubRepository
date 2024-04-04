using System.Collections.Generic;
using UnityEngine;

public class AlmanacManager : MonoBehaviour
{
    private PlayerMovement playerMV;
    private Almanac almanac;
    
    private GameObject canvas, almanacTabs, almanacBody;
    private GameObject almanacDescription, almanacCollection;

    private AlmanacSection mission, tutorial, collection;
    private Dictionary<string, AlmanacSection> sections;

    public const string JSON_DIR = "./Assets/almanac.json";
    public bool isShown = false;

    void Start()
    {
        string JSON_DATA = System.IO.File.ReadAllText(JSON_DIR);
        this.almanac = JSONParser.FromAsObject<Almanac>(JSON_DATA);

        this.playerMV = GameObject.Find("Player").GetComponent<PlayerMovement>();
        this.canvas = this.gameObject.transform.GetChild(0).gameObject;
        
        var tmp = GetAllGameObjectChildren(this.canvas);
        this.almanacTabs = tmp["AlmanacTabs"];
        this.almanacBody = tmp["AlmanacBody"];
        
        var tabs = GetAllGameObjectChildren(this.almanacTabs);
        tmp = GetAllGameObjectChildren(this.almanacBody);
        this.almanacDescription = tmp["AlmanacDescription"];
        this.mission = new AlmanacSection(tmp["AlmanacMissionArea"], tabs["MissionTab"]);
        this.collection = new AlmanacSection(tmp["AlmanacCollectionArea"], tabs["CollectionTab"]);
        this.tutorial = new AlmanacSection(tmp["AlmanacTutorialArea"], tabs["TutorialTab"]);

        this.sections = new()
        {
            { nameof(mission), mission },
            { nameof(collection), collection },
            { nameof(tutorial), tutorial }
        };

        //this.almanacContainer.transform.position = new Vector3(0, 0, -9);
        this.almanac.CreateGameObjects(this.almanacCollection);
        this.Select(nameof(this.collection));
    }
    private Dictionary<string, GameObject> GetAllGameObjectChildren(GameObject parent)
    {
        Dictionary<string, GameObject> values = new();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject val = parent.transform.GetChild(i).gameObject;
            values.Add(val.name, val);
        }
        return values;
    } 
    /// <summary>
    /// Makes almanac visible
    /// </summary>
    /// <param name="TabToOpen">
    ///     Which tab it opens
    /// </param>
    public void ShowAlmanac(string TabToOpen)
    {
        this.almanacBody.SetActive(true);
        this.almanacTabs.SetActive(true);
        this.isShown = true;
        this.Select(TabToOpen);
        this.playerMV.canMove = false;
    }
    public void HideAlmanac()
    {
        this.playerMV.canMove = true;
        this.HideAllElements(); // probabilemente opzionale
        this.isShown = false;
        this.almanacBody.SetActive(false);
        this.almanacTabs.SetActive(false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="search"></param>
    public void Select(string search)
    {
        foreach (var item in this.sections)
        {
            if(item.Key == search)
            {
               // Debug.Log($"Selected; Key: {item.Key  }; search: {search}");
                item.Value.Select(this.almanacBody);
            }
            else
            {
               // Debug.Log($"Hide; Key: {item.Key}; search: {search}");
                item.Value.Hide();
            }
        }
    }
    /// <summary>
    /// If it's shown it hides, otherwise it shows
    /// 
    /// </summary>
    /// <param name="TabToOpen">
    ///     Optional parameter. Decides if it is being opened what tab to open
    /// </param>
    public void ToggleAlmanac(string tabToOpen = "collection")
    {
        if (isShown) this.HideAlmanac();
        else this.ShowAlmanac(tabToOpen);
    }
    void HideAllElements()
    {
        foreach (var item in sections)
        {
            item.Value.Hide();
        }
    }

}

public class AlmanacSection
{
    public GameObject collectionArea;
    public GameObject container;
    public GameObject[] elements;
    public GameObject tab;

    private bool hidden= false;

    public AlmanacSection(GameObject container, GameObject tab)
    {
        this.collectionArea = container;
        Transform t = container.transform.GetChild(0) //Scroll
                                    .GetChild(0); //Container
        this.container = t.gameObject;
        this.elements = new GameObject[t.childCount];
        for (int i = 0; i < this.elements.Length; i++)
        {
            this.elements[i] = t.GetChild(i).gameObject;
        }
        this.tab = tab;
        this.Hide();
    }
    public void Select(GameObject body)
    {
        if (!this.hidden) return;
        this.hidden = false;
        body.GetComponent<SpriteRenderer>().color = this.tab.GetComponent<SpriteRenderer>().color;
        this.collectionArea.SetActive(true);
      //  Debug.Log("Shown: " + this.tab.name);
    }
    public void Hide()
    {
        if (this.hidden) return;
        this.hidden = true;
        this.collectionArea.SetActive(false);
      //  Debug.Log("Hidden: " + this.tab.name);
    }
}