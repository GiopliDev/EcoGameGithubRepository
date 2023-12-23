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
        this.almanac.CreateGameObjects();
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
public class Almanac
{
    public CollectionElement[] Collection { get; set; }
    public TutorialInfoElement[] Tutorial { get; set; }
    public MissionInfoElement[] Mission { get; set; }

    public void CreateGameObjects()
    {
        foreach (var item in this.Collection)
        {
            item.CreateGameObject();
        }
        foreach (var item in this.Tutorial)
        {
            item.CreateGameObject();
        }
        foreach (var item in this.Mission)
        {
            item.CreateGameObject();
        }
    }
    public override string ToString()
    {
        string coll = "", tut = "", miss = "";
        foreach (var item in this.Collection)
        {
            coll += '\t' + item.ToString();
        }
        foreach (var item in this.Tutorial)
        {
            tut += '\t' + item.ToString();
        }
        foreach (var item in this.Mission)
        {
            miss += '\t' + item.ToString();
        }
        return $"Collection:\n{coll}\nTutorial:\n{tut}\nMission:\n{miss}";
    }
}
public class CollectionElement
{
    public string ID { get; set; }
    /// <summary>
    /// Indirizzo immagine dell'elemento
    /// </summary>
    public string Image { get; set; }
    /// <summary>
    /// Informazioni dell'elemento
    /// </summary>
    public string Info { get; set; }
    /// <summary>
    /// Nome dell'elemento
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Sezione dove è collocato l'elemento
    /// </summary>
    public string Section { get; set; }
    /// <summary>
    /// E' stato sbloccato?
    /// </summary>
    public bool Unlocked { get; set; }
    public GameObject Element { get; set; }
    public override string ToString()
    {
        return $"ID: {ID}, Image: {Image}, Info: {Info}, Name: {Name}, Section: {Section}, Unlocked: {Unlocked}";
    }
    public void CreateGameObject()
    {

    }
}
public class TutorialInfoElement
{
    /// <summary>
    /// Descrizione
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// Indirizzo dell'immagine
    /// </summary>
    public string Image { get; set; }
    /// <summary>
    /// ID della missione per poter visualizzarla
    /// </summary>
    public int[] Requires { get; set; }
    public GameObject Element { get; set; }
    public override string ToString()
    {
        return $"Text: {Text}, Image: {Image}, Requires: {JSONParser.To(Requires)}";
    }
    public void CreateGameObject()
    {

    }
}
public class MissionInfoElement
{
    public int ID { get; set; }
    public string Title { get; set; }
    /// <summary>
    /// Valore intero per l'importanza della missione,
    /// <example>
    /// 1 => Principale,
    /// 2 => Secondaria...
    /// </example>
    /// </summary>
    public int Tier { get; set; }
    public string Description { get; set; }
    /// <summary>
    /// ID della missione per poter visualizzarla
    /// </summary>
    public int[] Requires { get; set; }
    /// <summary>
    /// Se è completata
    /// </summary>
    public bool IsDone { get; set; }
    public GameObject Element { get; set; }
    public override string ToString()
    {
        return $"ID: {ID}, Title: {Title}, Tier: {Tier}, Description: {Description}, Requires: {JSONParser.To(Requires)}, IsDone: {IsDone}";
    }
    public void CreateGameObject()
    {

    }
}