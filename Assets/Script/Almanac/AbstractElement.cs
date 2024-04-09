using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractElement
{
    /// <summary>
    /// Titolo o nome dell'elemento
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Informazioni dell'elemento
    /// </summary>
    public string Info { get; set; }
    /// <summary>
    /// ID della missione per poter visualizzarla
    /// </summary>
    public int[] Requires { get; set; }
    /// <summary>
    /// Rappresentazione GameObject dell'elemento.
    /// L'elemento dovra essere un PreFab
    /// </summary>
    public GameObject ElementAsGameObject { get; set; }
    /// <returns>
    /// Rappresentazione della classe come stringa leggibile
    /// </returns>
    public abstract override string ToString();
    /// <summary>
    /// Rende la classe deserializzata dal metodo  
    /// </summary>
    /// <returns>
    /// Stringa rappresentata come JSON
    /// </returns>
    /// <see cref="ToDeserializedJSON"/>
    /// <exception cref="System.Exception"></exception>
    public string ToSerializedJSON()
    {
        return JSONParser.To(ToDeserializedJSON()) ?? throw new System.Exception("Serializing returned null");
    }
    /// <returns>
    /// Rappresentazione di oggetto JSON
    /// </returns>
    public abstract object ToDeserializedJSON();

    public abstract GameObject GenerateGameObject(GameObject prefab);
}
