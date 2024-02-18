using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectionElement : AbstractElement
{
    /// <summary>
    /// Indirizzo immagine dell'elemento
    /// </summary>
    public string Image { get; set; }
    /// <summary>
    /// Sezione dove è collocato l'elemento
    /// </summary>
    public string Section { get; set; }
    /// <summary>
    /// E' stato sbloccato?
    /// </summary>
    public bool Unlocked { get; set; }
    public override string ToString()
    {
        return $"";
    }
#nullable enable
    public override object ToDeserializedJSON()
    {
        return new Dictionary<string, object?>()
        {
            { nameof(this.Info), this.Info },
            { nameof(this.Requires), this.Requires },
            { nameof(this.Title), this.Title },
         
            { nameof(this.Section), this.Section },
            { nameof(this.Image), this.Image },
            { nameof(this.Unlocked), this.Unlocked }
        };
    }
#nullable restore
}