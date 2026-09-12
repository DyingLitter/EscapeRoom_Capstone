using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

    [SerializeField] private ItemsSO Items;
    [HideInInspector] public Transform parentAfterDrag;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
        canvasGroup.blocksRaycasts = true;
        if (image != null) image.raycastTarget = true;

        if (parentAfterDrag == null) return;

        transform.SetParent(parentAfterDrag, false);
        ResetRectTransform(rectTransform);
    }

    public bool TryCombineWith(ItemDrag other, Transform targetSlot)
    {
        if (Items == null || Items.combinations == null) return false;

        string thisBase = GetBaseName(gameObject.name);
        string otherBase = GetBaseName(other.gameObject.name);

        if (string.IsNullOrEmpty(thisBase) || string.IsNullOrEmpty(otherBase)) return false;

        foreach (var combo in Items.combinations)
        {
            if ((combo.itemA == thisBase && combo.itemB == otherBase) ||
                (combo.itemA == otherBase && combo.itemB == thisBase))
            {
                var prefab = Resources.Load<GameObject>(combo.resultPrefab);
                if (prefab == null)
                {
                    Debug.LogWarning($"Result prefab '{combo.resultPrefab}' not found in Resources for {combo.itemA} + {combo.itemB}");
                    return false;
                }

                var newGO = Instantiate(prefab, targetSlot);
                newGO.name = prefab.name;

                var rect = newGO.GetComponent<RectTransform>();
                if (rect != null)
                {
                    ResetRectTransform(rect);
                }

                Destroy(other.gameObject);
                Destroy(gameObject);
                return true;
            }
        }

        return false;
    }

    private void ResetRectTransform(RectTransform targetRect)
    {
        targetRect.anchoredPosition = Vector2.zero;
        targetRect.localPosition = Vector3.zero;
        targetRect.localRotation = Quaternion.identity;
        targetRect.localScale = Vector3.one;
    }

    private string GetBaseName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName)) return fullName;
        int idx = fullName.IndexOf("_");
        return idx > 0 ? fullName.Substring(0, idx) : fullName;
    }
}
