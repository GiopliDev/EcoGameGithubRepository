using System;
using System.Threading.Tasks;
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
        //this.almanac.CreateGameObjects();
        this.almanacContainer = GameObject.Find("Almanac");
        this.almanacContainer.transform.position = new Vector3(0, 0, -9);
        this.almanacTabs = GameObject.Find("AlmanacTabs");
        this.almanacBody = GameObject.Find("AlmanacBody");
        this.CollectionTabSelected();
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
    public void ShowAlmanac()
    {
        this.isShown = true;
        this.playerMV.canMove = false;
        this.almanacBody.SetActive(true);
        this.almanacTabs.SetActive(true);
        this.CollectionTabSelected();
    }
    public void HideAlmanac()
    {
        this.isShown = false;
        this.playerMV.canMove = true;
        this.almanacBody.SetActive(false);
        this.almanacTabs.SetActive(false);
    }
    public void CollectionTabSelected()
    {
        this.HideAllElements();
        this.almanacBody.GetComponent<SpriteRenderer>().color = new Color(0.8679245f, 0.2824849f, 0.2824849f);
    }
    public void TutorialTabSelected()
    {
        this.HideAllElements();
        this.almanacBody.GetComponent<SpriteRenderer>().color = new Color(0.8666667f, 0.7383271f, 0.2823529f);
    }
    public void MissionTabSelected()
    {
        this.HideAllElements();
        this.almanacBody.GetComponent<SpriteRenderer>().color = new Color(0.2823529f, 0.7346016f, 0.8666667f);
    }
    void HideAllElements()
    {

    }

}