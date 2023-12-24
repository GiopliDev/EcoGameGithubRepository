using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AlmanacHelper
{
    public static GameObject CreateGameObject(string scope, string name, Vector2 size, float fatherSize, int index)
    {
        GameObject element = new(
            $"AlmanacCell{scope}{name}",
            typeof(BoxCollider2D),
            typeof(AlmanacCellManager));

        BoxCollider2D coll = element.GetComponent<BoxCollider2D>();
        coll.size = size;
        coll.autoTiling = true;

        GameObject inner = new($"");

        element.transform.position = new Vector3(fatherSize % index, (int)(fatherSize / index));

        return element;
    }
}
