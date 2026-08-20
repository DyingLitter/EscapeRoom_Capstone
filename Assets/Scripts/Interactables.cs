using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Interactables : MonoBehaviour
{
    private Interact Interacted;

    private GameManager Key;
    private Inventory Inventory;

    public ItemsSO ISO;

    public void Start()
    {
        Interacted = FindAnyObjectByType<Interact>();
        Key = FindAnyObjectByType<GameManager>();
        Inventory = FindAnyObjectByType<Inventory>();
    }
    public void Interact()
    {
        if (Interacted == null || Interacted.selection == null) return;

        var pickup = Interacted.selection.GetComponent<Interactables>();
        if (pickup != null && pickup.ISO != null && pickup.ISO.name != "Gate")
        {
            Inventory?.AddItem(pickup.ISO);

            if (pickup.ISO.ItemName == "Stick" && Key != null)
            {
                Key.StickGet = true;
            }



            Interacted.selection.SetActive(false);
            if (Interacted.interactionText != null) Interacted.interactionText.SetActive(false);
            {
                Interacted.selection = null;
            }
 
            return;

        }

        ISO.InteractChecks();

    }

 


}
