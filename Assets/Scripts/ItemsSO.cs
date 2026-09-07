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
    private NPC npc;
    private Interact Interacted;
    private GameManager Key;
    private Inventory Inventory;

    [System.Serializable]
    public struct Combination
    {
        public string itemA;         
        public string itemB;         
        public string resultPrefab;   
    }

    public Combination[] combinations;

    public void InteractChecks()
    {
      

        if (Interacted == null) Interacted = FindAnyObjectByType<Interact>();
        if (Key == null) Key = FindAnyObjectByType<GameManager>();
        if (Inventory == null) Inventory = FindAnyObjectByType<Inventory>();

        if (Interacted.selection.name == "NPC")
        {
            npc = Interacted.selection.GetComponent<NPC>();
            if (npc != null)
            {
                npc.InteractedWith(Interacted.selection.gameObject);
            }
        }

        if (Interacted.selection.name == "Gate" && Key.StickGet == true)
        {
            GameObject BrokenStick = Resources.Load<GameObject>("Stick (Broken)");
            Animator gateAnimator = Interacted.selection.GetComponent<Animator>();
            BoxCollider gatecol = Interacted.selection.GetComponent<BoxCollider>();

            Key.StickGet = false;
            gateAnimator.SetTrigger("Open");
            if (BrokenStick != null)
            {
                gatecol.enabled = false;
                Inventory?.ReplaceItem("Stick", BrokenStick);
            }
        }

        if (Interacted.selection.name == "Door" && Key.StickFixed == true)
        {
            Animator DoorAnimator = Interacted.selection.GetComponent<Animator>();

            DoorAnimator.SetTrigger("Open");
           
            Key.StickFixed = false;

        }

       
       
        return;
    }
}
