using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
}