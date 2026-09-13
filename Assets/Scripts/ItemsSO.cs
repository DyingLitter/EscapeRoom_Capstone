using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ItemsSO", menuName = "Scriptable Objects/ItemsSO")]
public class ItemsSO : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public GameObject InventoryPrefab; //Inventory Object
    public GameObject ItemPrefab; //In-Game Object
    public bool CanBePickedUp;

    private PlayerController Player;
    private NPC npc;
    private Interact Interacted;
    private BabyLevelManager BKey;
    private Inventory Inventory;
    private Canvas Canvas;
    private DoorController Door;
    private ItemDrag ID;

    [System.Serializable]


    public struct Combination
    {
        public string itemA;         
        public string itemB;         
        public string resultPrefab;   
    }

    public Combination[] combinations;

    public ItemDrag FindItemDragFrom(GameObject gameobject)
    {
        if (gameobject == null) return null;
        var current = gameobject.transform;
        while (current != null)
        {
            var itemDrag = current.GetComponent<ItemDrag>();
            if (itemDrag != null) return itemDrag;
            current = current.parent;
        }
        return null;
    }

    public string GetBaseName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName)) return fullName;
        int idx = fullName.IndexOf("_");
        return idx > 0 ? fullName.Substring(0, idx) : fullName;
    }

    public void TryCombineWith(ItemDrag draggedItem, ItemDrag targetItem)
    {
        if (!ItemDrag.mouseButtonReleased) return;

        string draggedBase = GetBaseName(draggedItem.gameObject.name);
        string targetBase = GetBaseName(targetItem.gameObject.name);

        if (string.IsNullOrEmpty(draggedBase) || string.IsNullOrEmpty(targetBase)) return;

        foreach (var combo in combinations)
        {
            if ((combo.itemA == draggedBase && combo.itemB == targetBase) ||
                (combo.itemA == targetBase && combo.itemB == draggedBase))
            {
                var prefab = Resources.Load<GameObject>(combo.resultPrefab);
                if (prefab == null)
                {
                    Debug.LogWarning($"Result prefab '{combo.resultPrefab}' not found in Resources for combination {combo.itemA} + {combo.itemB}");
                    ItemDrag.mouseButtonReleased = false;
                    return;
                }

                Transform targetSlot = targetItem.parentAfterDrag != null
                    ? targetItem.parentAfterDrag
                    : (targetItem.transform.parent != null ? targetItem.transform.parent : targetItem.transform);

                var newObject = Instantiate(prefab, targetSlot);
                newObject.name = prefab.name;

                var rect = newObject.GetComponent<RectTransform>();
                newObject.transform.SetSiblingIndex(targetItem.transform.GetSiblingIndex());

                ItemDrag.mouseButtonReleased = false;

                Destroy(targetItem.gameObject);
                Destroy(draggedItem.gameObject);
                return;
            }
        }
    }

    public void InteractChecks()
    {
        if (Player == null) Player = FindAnyObjectByType<PlayerController>();
        if (Interacted == null) Interacted = FindAnyObjectByType<Interact>();
        if (BKey == null) BKey = FindAnyObjectByType<BabyLevelManager>();
        if (Inventory == null) Inventory = FindAnyObjectByType<Inventory>();

      

        if (Interacted.selection.name == "NPC")
        {
            npc = Interacted.selection.GetComponent<NPC>();
            if (npc != null)
            {
                npc.InteractedWith(Interacted.selection.gameObject);
            }
        }

      

        return;
    }
}
