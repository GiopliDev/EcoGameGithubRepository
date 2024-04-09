using UnityEngine;
public class Almanac
{
    public CollectionElement[] Collection { get; set; }
    public TutorialInfoElement[] Tutorial { get; set; }
    public MissionInfoElement[] Mission { get; set; }

    public void CreateGameObjects(GameObject father, GameObject tinyElement, GameObject longElement)
    {
        foreach (var item in Collection) {
            item.ElementAsGameObject = Object.Instantiate(tinyElement, Vector3.zero, Quaternion.identity);
        }
        foreach (var item in Tutorial)
        {
            item.ElementAsGameObject = Object.Instantiate(longElement, Vector3.zero, Quaternion.identity);
        }
        foreach (var item in Mission)
        {
            item.ElementAsGameObject = Object.Instantiate(longElement, Vector3.zero, Quaternion.identity);
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