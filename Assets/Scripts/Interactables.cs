using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Interactables : MonoBehaviour
{
    private Interact Interacted;

    private GameManager Key;
    private Inventory Inventory;
    
    public void Start()
    {
        Interacted = FindAnyObjectByType<Interact>();
        Key = FindAnyObjectByType<GameManager>();
        Inventory = FindAnyObjectByType<Inventory>();
    }
    public void Interact()
    {
        if (Interacted == null || Interacted.selection == null) return;

        var pickup = Interacted.selection.GetComponent<ItemPickup>();
        if (pickup != null && pickup.ISO != null)
        {
            Inventory?.AddItem(pickup.ISO);

            if (pickup.ISO.ItemName == "Stick")
            {
                if (Key != null) Key.StickGet = true;
            }

            Interacted.selection.SetActive(false);
            if (Interacted.interactionText != null) Interacted.interactionText.SetActive(false);
            Interacted.selection = null;
            return;

        }

    }

 


}
