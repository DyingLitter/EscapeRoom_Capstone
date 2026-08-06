using UnityEngine;
using Unity.UI;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Inventory : MonoBehaviour

{
    public Transform[] InventorySlots;
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

    public void AddItem()
    {
        if (Interacted.selection.tag == "Stick")
        {
            Transform spawnPoint = InventorySlots[0];
            GameObject item = Resources.Load<GameObject>("Items/Stick");
            GameObject itemspawn = Instantiate(item, spawnPoint, false);
        }
    }
}
