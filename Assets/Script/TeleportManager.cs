using UnityEngine;
using UnityEngine.UI;

public class TeleportManager : MonoBehaviour
{
    public GameObject teleportTo;
    public Text text;
    public void Teleport(GameObject sender)
    {
        sender.transform.position = teleportTo.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.gameObject.SetActive(true);
        text.text = "E";
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        text.text = "";
        text.gameObject.SetActive(false);
    }
}
