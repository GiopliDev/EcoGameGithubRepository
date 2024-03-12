using System.Collections.Generic;
using UnityEngine;

public class AlmanacManager : MonoBehaviour
{
    private PlayerMovement playerMV;
    public Almanac almanac;
    public GameObject almanacContainer, almanacTabs, almanacBody;
    public GameObject almanacDescription, almanacCollection;

    public const string JSON_DIR = "./Assets/almanac.json";
    public bool isShown = false;
    
    void Start()
    {
        string JSON_DATA = System.IO.File.ReadAllText(JSON_DIR);

        this.almanac = JSONParser.FromAsObject<Almanac>(JSON_DATA);
        this.playerMV = GameObject.Find("Player").GetComponent<PlayerMovement>();
        this.almanacContainer = this.gameObject;
        var tmp = GetAllGameObjectChildren(this.almanacContainer);
        this.almanacTabs = tmp["AlmanacTabs"];
        this.almanacBody = tmp["AlmanacBody"];
        
        tmp = GetAllGameObjectChildren(this.almanacBody);
        this.almanacContainer = tmp["AlmanacContainer"];
        this.almanacDescription = tmp["AlmanacDescription"];

        //this.almanacContainer.transform.position = new Vector3(0, 0, -9);
        this.almanac.CreateGameObjects(this.almanacCollection);
        this.CollectionTabSelected();
        this.HideAlmanac();
        
    }
    void Update()
    {
        //ToggleAlmanac();  => Moved to Player::Update
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
    public void ShowAlmanac()
    {
        this.almanacContainer.SetActive(true);
        this.almanacTabs.SetActive(true);
        this.almanacBody.SetActive(true);
        this.isShown = true;
        this.playerMV.canMove = false;
        this.CollectionTabSelected();
    }
    public void HideAlmanac()
    {
        this.isShown = false;
        this.playerMV.canMove = true;
        this.HideAllElements();
        this.almanacBody.SetActive(false);
        this.almanacTabs.SetActive(false);
        this.almanacContainer.SetActive(false);
    }
    /// <summary>
    /// If isShown it hides, otherwise it shows
    /// </summary>
    public void ToggleAlmanac()
    {
        if (isShown) this.HideAlmanac();
        else this.ShowAlmanac();
    }
    public void CollectionTabSelected()
    {
        this.HideAllElements();
        this.almanacBody.GetComponent<SpriteRenderer>().color = new Color(0.8679245f, 0.2824849f, 0.2824849f);
        foreach (var element in this.almanac.Collection)
        {
            //element.ElementAsGameObject.SetActive(true);
        }
    }
    public void TutorialTabSelected()
    {
        this.HideAllElements();
        this.almanacBody.GetComponent<SpriteRenderer>().color = new Color(0.8666667f, 0.7383271f, 0.2823529f);
        foreach (var element in this.almanac.Tutorial)
        {
            //element.ElementAsGameObject.SetActive(true);
        }
    }
    public void MissionTabSelected()
    {
        this.HideAllElements();
        this.almanacBody.GetComponent<SpriteRenderer>().color = new Color(0.2823529f, 0.7346016f, 0.8666667f);
        foreach (var element in this.almanac.Mission)
        {
            //element.ElementAsGameObject.SetActive(true);
        }
    }
    void HideAllElements()
    {
        foreach (var element in this.almanac.Collection)
        {
            //element.ElementAsGameObject.SetActive(false);
        }
        foreach (var element in this.almanac.Tutorial)
        {
            //element.ElementAsGameObject.SetActive(false);
        }
        foreach (var element in this.almanac.Mission)
        {
            //element.ElementAsGameObject.SetActive(false);
        }
    }

}