using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

   [SerializeField] private ItemsSO Items;
    public static bool mouseButtonReleased;
    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        if (image != null) image.raycastTarget = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
        mouseButtonReleased = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        if (image != null) image.raycastTarget = true;
        mouseButtonReleased = true;

        // If we dropped on another UI element, eventData.pointerEnter will be that object (or a child).
        if (eventData != null && eventData.pointerEnter != null)
        {
            var targetItem = Items.FindItemDragFrom(eventData.pointerEnter);
            if (targetItem != null && targetItem != this)
            {
                Items.TryCombineWith(this, targetItem);
            }
        }
    }

}
