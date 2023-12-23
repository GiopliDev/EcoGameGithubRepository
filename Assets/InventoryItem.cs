using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    public Image image;

    [HideInInspector] public Item item;
    [HideInInspector] public Transform parentAfterDrag;

    private RectTransform canvasRectTransform;
    private Camera canvasCamera;



    public void InitializeItem(Item newItem) { 
        item= newItem;
        image.sprite= newItem.image;
    
    }

    private void Start()
    {
        //InitializeItem(item);
        // Get the RectTransform of the Canvas
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // Get the Camera of the Canvas
        canvasCamera = GetComponentInParent<Canvas>().worldCamera;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        Vector3 currentPosition = transform.position;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.parent.parent);
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, 1);
        transform.position = newPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to world space
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, mousePosition, canvasCamera, out Vector3 worldPosition);
        
        // Set the world position
        transform.position = worldPosition;
        


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);

    }
}