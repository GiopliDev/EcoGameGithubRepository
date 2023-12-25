using UnityEngine;

public static class AlmanacHelper
{
    public static GameObject CreateGameObject(string scope, string name, Vector2 size, GameObject father, int index)
    {
        name = name.Replace(" ", "");
        scope = scope.Replace(" ", "");

        Bounds b = father.GetComponent<BoxCollider2D>().bounds;
        float fatherSize = b.max.x - b.min.x;
        int numOfElementInRow = (int)(fatherSize / size.x);

        GameObject element = new(
            $"AlmanacCell{scope}{name}",
            typeof(BoxCollider2D),
            typeof(AlmanacCellManager));

        BoxCollider2D coll = element.GetComponent<BoxCollider2D>();
        coll.size = size;
        coll.autoTiling = true;

        GameObject inner = new($"AlmanacCell{scope}{name}Inner");

        element.transform.position = new Vector3(index % numOfElementInRow, (int)(index / numOfElementInRow));
        inner.transform.parent = element.transform;
        element.transform.parent = father.transform;

        element.SetActive(false);
        return element;
    }
}
