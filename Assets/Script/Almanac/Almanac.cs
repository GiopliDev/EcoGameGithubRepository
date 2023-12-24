using UnityEngine;
public class Almanac
{
    public CollectionElement[] Collection { get; set; }
    public TutorialInfoElement[] Tutorial { get; set; }
    public MissionInfoElement[] Mission { get; set; }

    public void CreateGameObjects(float fatherWidth)
    {
        Vector2 size = new(fatherWidth / 8, 100);
        for (int i = 0; i < this.Collection.Length; i++)
        {
            this.Collection[i].Element = AlmanacHelper.CreateGameObject(
                nameof(this.Collection), //La funzione nameof() prende sempre il nome del parametro, in questo caso Collection
                                         //Torna utile per dare un errore se è stato cambiato il nome
                this.Collection[i].Name, 
                size, 
                fatherWidth, 
                i);
        }
        size = new Vector2(fatherWidth, 100);
        for (int i = 0; i < this.Tutorial.Length; i++)
        {
            this.Tutorial[i].Element = AlmanacHelper.CreateGameObject(
                nameof(this.Tutorial), 
                this.Tutorial[i].Title, 
                size, 
                fatherWidth,
                i);
        }
        for (int i = 0; i < this.Mission.Length; i++)
        {
            this.Mission[i].Element = AlmanacHelper.CreateGameObject(
                nameof(this.Mission), 
                this.Mission[i].Title, 
                size,
                fatherWidth,
                i);
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