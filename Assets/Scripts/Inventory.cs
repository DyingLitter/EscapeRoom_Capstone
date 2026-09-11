using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Inventory : MonoBehaviour

{
    public Transform[] InventorySlots;
    public Transform[] HotbarSlots;
    private Transform ItemSlot;
    private Interactables Interactables;
    private Interact Interacted;
    private BabyLevelManager BKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Interacted = FindAnyObjectByType<Interact>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(ItemsSO SelectedItem)
    {
        if (SelectedItem == null) return;

        Transform spawnPoint = null;

        if (HotbarSlots != null && HotbarSlots.Length > 0)
        {
            for (int i = 0; i < HotbarSlots.Length; i++)
            {
                var slot = HotbarSlots[i];
                if (slot == null) continue;
                if (slot.childCount == 0)
                {
                    spawnPoint = slot;
                    break;
                }
            }
        }

        if (spawnPoint == null && InventorySlots != null && InventorySlots.Length > 0)
        {
            for (int i = 0; i < InventorySlots.Length; i++)
            {
                var slot = InventorySlots[i];
                if (slot == null) continue;
                if (slot.childCount == 0)
                {
                    spawnPoint = slot;
                    break;
                }
            }
        }

        if (spawnPoint == null) return;
        GameObject itemspawn;

        if (SelectedItem.InventoryPrefab != null)
        {
            itemspawn = Instantiate(SelectedItem.InventoryPrefab, spawnPoint, false);
            itemspawn.name = SelectedItem.ItemName;
        }
        else
        {
            itemspawn = new GameObject(SelectedItem.ItemName);
            itemspawn.transform.SetParent(spawnPoint, false);
        }
    }

    private string GetBaseName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName)) return fullName;
        int idx = fullName.IndexOf("_");
        return idx > 0 ? fullName.Substring(0, idx) : fullName;
    }

    public bool ReplaceItem(string oldBaseName, GameObject newItemPrefab)
    {
        if (string.IsNullOrEmpty(oldBaseName)) return false;

        Transform found = FindItemByBaseNameInSlots(oldBaseName, HotbarSlots)
            ?? FindItemByBaseNameInSlots(oldBaseName, InventorySlots);

        if (found == null) return false;

        Transform parentSlot = found.parent;
        int siblingIndex = found.GetSiblingIndex();

        Destroy(found.gameObject);

        if (newItemPrefab != null)
        {
            GameObject newGO = Instantiate(newItemPrefab, parentSlot, false);
            newGO.name = newItemPrefab.name;
            newGO.transform.SetSiblingIndex(siblingIndex);
        }

        return true;
    }

    private Transform FindItemByBaseNameInSlots(string baseName, Transform[] slots)
    {
        if (slots == null) return null;
        foreach (var slot in slots)
        {
            if (slot == null) continue;
            for (int i = 0; i < slot.childCount; i++)
            {
                var child = slot.GetChild(i);
                if (GetBaseName(child.name) == baseName)
                    return child;
            }
        }
        return null;
    }

    public bool HasItem(string baseName)
    {
        if (string.IsNullOrEmpty(baseName)) return false;
        return FindItemByBaseNameInSlots(baseName, HotbarSlots) != null
         || FindItemByBaseNameInSlots(baseName, InventorySlots) != null;
    }
}