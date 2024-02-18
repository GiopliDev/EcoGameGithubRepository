using UnityEngine;

public class AlmanacCellManager : MonoBehaviour
{
    private static GameObject lastActiveGameObject = null;
    void OnMouseUpAsButton()
    {
        if(lastActiveGameObject != null) lastActiveGameObject.SetActive(false);
        lastActiveGameObject = this.GetComponent<GameObject>();
        lastActiveGameObject.SetActive(true);
    }
}
