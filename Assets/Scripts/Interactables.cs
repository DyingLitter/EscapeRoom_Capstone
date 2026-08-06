using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Interactables : MonoBehaviour
{
    private Interact Interacted;
    private PlayerController player;
    private GameManager Key;
    private Inventory Inventory;

    private GameObject DestItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      player = FindAnyObjectByType<PlayerController>();
      Interacted = FindAnyObjectByType<Interact>();
      Key = FindAnyObjectByType<GameManager>();
      Inventory = FindAnyObjectByType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (Interacted.selection.tag == "Stick")
        {
           Key.StickGet = true;
            Inventory.AddItem();
            Interacted.selection.SetActive(false);
           
        }

        if (Interacted.selection.tag == "BabyGate")
        {
            if (Key.StickGet == true)
            {
                Interacted.selection.SetActive(false);
                Key.StickGet = false;
                DestItem = GameObject.Find("Stick(Clone)");
                Destroy(DestItem);
            }
            else if (Key.StickGet == false)
            {
                Debug.Log("Gate Locked");
            }
        }
    }
}
