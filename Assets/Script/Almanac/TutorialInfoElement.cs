using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInfoElement
{
    public string Title;
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
}