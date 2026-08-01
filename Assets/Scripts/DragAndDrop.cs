using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IEndDragHandler, IPointerEnterHandler,
    IDragHandler, IPointerExitHandler, IBeginDragHandler
{
    //Private Variables
    private Canvas canvas;
    private Image image;
    private RectTransform rectTransform;

    private bool isPointerOver = false;
    private bool isDragging = false;

    void Start()
    {
        //Set private variables for use. Canvas is parent canvas, Image + RectTransform are on self.
        canvas = GetComponentInParent<Canvas>();
        image = gameObject.GetComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && (isPointerOver || isDragging))
        {
            rectTransform.Rotate(0f, 0f, 90f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Set rectTransform to follow mouse, with adjustment for scale of Canvas
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        // Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        // Debug.Log("OnPointerExit");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       // Debug.Log("EndDrag");
      // image.raycastTarget = true;
      isDragging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("BeginDrag");
        //image.raycastTarget = false;
        rectTransform.SetAsLastSibling();
        isDragging = true;
    }
}
