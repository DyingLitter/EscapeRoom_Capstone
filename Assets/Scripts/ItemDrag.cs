using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image image;
    public bool CanBeOpened;
    [SerializeField] private ItemsSO Items;
    public static bool mouseButtonReleased;
    [HideInInspector] public Transform parentAfterDrag;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private TeenLevelManager TM;
    private bool isLeftDragging = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        TM = FindAnyObjectByType<TeenLevelManager>();

        // CanvasGroup guarantees mouse raycasts pass through to the slot underneath
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        isLeftDragging = true;

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
        if (!isLeftDragging) return;

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
        if (!isLeftDragging) return;
        isLeftDragging = false;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        if (Items == null)
        {
            Debug.LogWarning($"ItemDrag: Items is null on '{name}'");
            return;
        }

        if (Items.name != "Cookie Box O") return;

        TM.CanvasScreen.SetActive(!TM.CanvasScreen.activeSelf);
    }

}