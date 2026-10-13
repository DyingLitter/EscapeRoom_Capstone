using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        if (eventData.pointerDrag.TryGetComponent(out ItemDrag incomingItem))
        {
            // Debug the item name to the console
            Debug.Log($"Item placed in world slot: {incomingItem.gameObject.name}");

            // Accept the item into this slot if empty
            if (transform.childCount == 0)
            {
                incomingItem.parentAfterDrag = transform;

                incomingItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                incomingItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
                incomingItem.GetComponent<RectTransform>().localRotation = Quaternion.identity;
                incomingItem.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
