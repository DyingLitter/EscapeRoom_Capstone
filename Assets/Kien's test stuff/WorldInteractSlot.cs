using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldInteractSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private string requiredItemName = "Stick";

    [Header("Ground Spawn Settings")]
    [Tooltip("The 3D world prefab to spawn on the ground (e.g., Broken Stick GameObject with collider/pickup)")]
    [SerializeField] private GameObject returnItem;

    [Tooltip("Where on the ground the 3D item will appear")]
    [SerializeField] private Transform groundSpawnPoint;

    [Header("Optional Slot Cleanup")]
    [Tooltip("If true, removes this slot once the item has been spawned")]
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
            // 1. Destroy the held UI item from the player
            incomingItem.parentAfterDrag = null;
            Destroy(incomingItem.gameObject);

            // 2. Spawn the 3D physical prefab onto the ground
            if (returnItem != null)
            {
                Vector3 spawnPos = groundSpawnPoint != null ? groundSpawnPoint.position : transform.position;
                Quaternion spawnRot = groundSpawnPoint != null ? groundSpawnPoint.rotation : Quaternion.identity;

                GameObject spawnedItem = Instantiate(returnItem, spawnPos, spawnRot);
                spawnedItem.name = returnItem.name;
            }

            // 3. Trigger events (gate animation, sound, etc.)
            onCorrectItemPlaced?.Invoke();

            // 4. Optionally clean up this world slot
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