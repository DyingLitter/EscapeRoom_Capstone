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
            var targetItem = FindItemDragFrom(eventData.pointerEnter);
            if (targetItem != null && targetItem != this)
            {
                TryCombineWith(targetItem);
            }
        }
    }

    private ItemDrag FindItemDragFrom(GameObject go)
    {
        if (go == null) return null;
        var current = go.transform;
        while (current != null)
        {
            var itemDrag = current.GetComponent<ItemDrag>();
            if (itemDrag != null) return itemDrag;
            current = current.parent;
        }
        return null;
    }

    private string GetBaseName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName)) return fullName;
        int idx = fullName.IndexOf("_");
        return idx > 0 ? fullName.Substring(0, idx) : fullName;
    }

    private void TryCombineWith(ItemDrag other)
    {
        if (!mouseButtonReleased) return;

        string thisBase = GetBaseName(gameObject.name);
        string otherBase = GetBaseName(other.gameObject.name);

        if (string.IsNullOrEmpty(thisBase) || string.IsNullOrEmpty(otherBase)) return;

        foreach (var combo in Items.combinations)
        {
            if ((combo.itemA == thisBase && combo.itemB == otherBase) ||
                (combo.itemA == otherBase && combo.itemB == thisBase))
            {
                var prefab = Resources.Load<GameObject>(combo.resultPrefab);
                if (prefab == null)
                {
                    Debug.LogWarning($"Result prefab '{combo.resultPrefab}' not found in Resources for combination {combo.itemA} + {combo.itemB}");
                    mouseButtonReleased = false;
                    return;
                }
                // Determine the slot where the resulting item should be placed.
                // Prefer the other's recorded parentAfterDrag (if it was dragged), otherwise use its current parent.
                Transform targetSlot = other.parentAfterDrag != null
                    ? other.parentAfterDrag
                    : (other.transform.parent != null ? other.transform.parent : other.transform);

                // Instantiate as a child of the target slot so it occupies the same slot.
                var newGO = Instantiate(prefab, targetSlot);
                newGO.name = prefab.name; // keep a clean name (optional)

                // Reset transform so it fits the UI slot (RectTransform aware).
                var rect = newGO.GetComponent<RectTransform>();
         
                // Preserve slot ordering
                newGO.transform.SetSiblingIndex(other.transform.GetSiblingIndex());

                mouseButtonReleased = false;

                Destroy(other.gameObject);
                Destroy(gameObject);
                return;
            }
        }
    }
}
