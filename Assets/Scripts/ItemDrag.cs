using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [System.Serializable]
    public struct Combination
    {
        public string itemA;          // base name before the '_' (e.g. "Stick (Broken)")
        public string itemB;          // other item (order is ignored)
        public string resultPrefab;   // Resources path to resulting prefab (e.g. "Stick (Fixed)")
    }

    public Image image;
    public Combination[] combinations;

    public static bool mouseButtonReleased;
    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
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
        image.raycastTarget = true;
        mouseButtonReleased = true;
    }

    private string GetBaseName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName)) return fullName;
        int idx = fullName.IndexOf("_");
        return idx > 0 ? fullName.Substring(0, idx) : fullName;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!mouseButtonReleased) return;
        if (collision == null || collision.gameObject == null) return;

        string thisBase = GetBaseName(gameObject.name);
        string otherBase = GetBaseName(collision.gameObject.name);

        if (string.IsNullOrEmpty(thisBase) || string.IsNullOrEmpty(otherBase)) return;

        // Find a matching combination (order-insensitive)
        foreach (var combo in combinations)
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

                Instantiate(prefab, transform.position, Quaternion.identity);
                mouseButtonReleased = false;

                // Destroy both originals
                Destroy(collision.gameObject);
                Destroy(gameObject);
                return;
            }
        }
    }
}
