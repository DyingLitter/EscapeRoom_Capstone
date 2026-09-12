using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldInteractSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private string requiredItemName = "Stick";

    
    [SerializeField] private GameObject returnItem;

    [SerializeField] private Transform groundSpawnPoint;

   
    [SerializeField] private bool destroySlotAfterUse = false;

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
            incomingItem.parentAfterDrag = null;
            Destroy(incomingItem.gameObject);

            if (returnItem != null)
            {
                Vector3 spawnPos = groundSpawnPoint != null ? groundSpawnPoint.position : transform.position;
                Quaternion spawnRot = groundSpawnPoint != null ? groundSpawnPoint.rotation : Quaternion.identity;

                GameObject spawnedItem = Instantiate(returnItem, spawnPos, spawnRot);
                spawnedItem.name = returnItem.name;
            }

            onCorrectItemPlaced?.Invoke();

            if (destroySlotAfterUse)
            {
                Destroy(gameObject);
            }
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