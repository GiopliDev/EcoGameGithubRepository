using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionInfoElement : AbstractElement
{
    /// <summary>
    /// Valore intero per l'importanza della missione,
    /// <code></code>
    /// <example>
    /// 1 => Principale,
    /// 2 => Secondaria...
    /// </example>
    /// </summary>
    public int Tier { get; set; }
    /// <summary>
    /// Se è completata
    /// </summary>
    public bool IsCompleted { get; set; }
    public override string ToString()
    {
        return "";
    }
#nullable enable
    public override object ToDeserializedJSON()
    {
        return new Dictionary<string, object?>()
        {
            { nameof(this.Info), this.Info },
            { nameof(this.Requires), this.Requires },
            { nameof(this.Title), this.Title },

            { nameof(this.Tier), this.Tier },
            { nameof(this.IsCompleted), this.IsCompleted }
        };
    }
#nullable restore
}