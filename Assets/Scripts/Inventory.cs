using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Inventory : MonoBehaviour

{
    public Transform[] InventorySlots;
    public Transform[] HotbarSlots;
    private Interactables Interactables;
    private Interact Interacted;
    private GameManager Key;

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

        if (HotbarSlots != null)
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

        if (spawnPoint == null && InventorySlots != null)
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

    //public void AddItemSo(ItemsSO SelectedItem)
    //{
    //    Transform spawnPoint = InventorySlots[0];
    //    GameObject item = Resources.Load<GameObject>(SelectedItem.InventoryPrefab.name); //Need to get GameObject name instead of reference
    //    GameObject itemspawn = Instantiate(item, spawnPoint, false);
    //}
}
