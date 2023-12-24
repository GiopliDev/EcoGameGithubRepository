using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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