using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldInteractSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private string requiredItemName = "Stick";
    [SerializeField] private bool consumeItemOnSuccess = true;

    public UnityEvent onCorrectItemPlaced;
    public UnityEvent onWrongItemPlaced;

    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        if (!eventData.pointerDrag.TryGetComponent(out ItemDrag incomingItem))
            return;

        string droppedItemName = GetBaseName(incomingItem.gameObject.name);

        if (!string.IsNullOrEmpty(requiredItemName) && droppedItemName == requiredItemName)
        {
            if (consumeItemOnSuccess)
            {
                incomingItem.parentAfterDrag = null;
                Destroy(incomingItem.gameObject);
                Destroy(gameObject);
            }
            else
            {
                incomingItem.parentAfterDrag = transform;
            }

            onCorrectItemPlaced?.Invoke();
        }
        else
        {
            onWrongItemPlaced?.Invoke();
        }
    }

    private string GetBaseName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName)) return fullName;
        int idx = fullName.IndexOf("_");
        return idx > 0 ? fullName.Substring(0, idx) : fullName;
    }
    
}