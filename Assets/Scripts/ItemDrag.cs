using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public bool CanBeOpened;
    private GameObject CanvasScreen;
    public UnityEvent onClick;

    [SerializeField] private ItemsSO Items;
    public static bool mouseButtonReleased;
    [HideInInspector] public Transform parentAfterDrag;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // CanvasGroup guarantees mouse raycasts pass through to the slot underneath
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;

        Canvas currentCanvas = GetComponentInParent<Canvas>();
        if (currentCanvas != null)
        {
            transform.SetParent(currentCanvas.rootCanvas.transform, true);
        }

        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
        if (image != null) image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mouseButtonReleased = false;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector3 worldPoint))
        {
            rectTransform.position = worldPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");

        canvasGroup.blocksRaycasts = true;
        if (image != null) image.raycastTarget = true;
        mouseButtonReleased = true;

        if (this == null || parentAfterDrag == null) return;

        transform.SetParent(parentAfterDrag, false);

        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;

        if (eventData != null && eventData.pointerEnter != null && Items != null)
        {
            var targetItem = Items.FindItemDragFrom(eventData.pointerEnter);
            if (targetItem != null && targetItem != this)
            {
                Items.TryCombineWith(this, targetItem);
            }
        }
    }

 
}