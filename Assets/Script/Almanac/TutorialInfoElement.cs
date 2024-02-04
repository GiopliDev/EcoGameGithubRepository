using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInfoElement : AbstractElement
{
    /// <summary>
    /// Indirizzo dell'immagine
    /// </summary>
    public string Image { get; set; }
    public override string ToString()
    {
        return $"";
    }
    public override object ToDeserializedJSON()
    {
        return new Dictionary<string, object>()
        {
            { nameof(this.Info), this.Info },
            { nameof(this.Requires), this.Requires },
            { nameof(this.Title), this.Title },

            { nameof(this.Image), this.Image }
        };
    }
}