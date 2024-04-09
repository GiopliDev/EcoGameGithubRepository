using UnityEngine;

public class AlmanacCellManager : MonoBehaviour
{
    public AlmanacManager almanac;
    void OnMouseUpAsButton()
    {
        this.almanac.CellClick(this.gameObject);
    }
}
