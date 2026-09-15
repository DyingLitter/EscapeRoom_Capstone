using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactables : MonoBehaviour
{
    private Interact Interacted;

    private BabyLevelManager BKey;
    private Canvas Canvas;
    private GameObject Canvass;
    private Inventory Inventory;
    private GameObject Player;
    private NPC npc;
    public ItemsSO ISO;
    [SerializeField] private NPC dialogue;
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject WorldSlot;
    private Animator CanAni;
    public void Start()
    {
        Interacted = FindAnyObjectByType<Interact>();
        BKey = FindAnyObjectByType<BabyLevelManager>();
        Inventory = FindAnyObjectByType<Inventory>();
        Player = FindAnyObjectByType<PlayerController>().gameObject;
        Canvas = FindAnyObjectByType<Canvas>();
        Canvass = FindAnyObjectByType<Canvas>().gameObject;
        CanAni = Canvas.GetComponent<Animator>();
    }
    public void Interact()
    {
        var pickup = Interacted.selection.GetComponent<Interactables>();

        if (WorldSlot == null)
        {
            WorldSlot = null;
        }

        ISO.InteractChecks();
        if (pickup.ISO.name == "Desk")
        {
            CanAni.SetTrigger("OpenInv");
        }

        if (Interacted.selection.name == "Gate")
        {
            WorldSlot.SetActive(true);
           
            if(BKey.StickGet == false)
            {
                npc = Interacted.selection.GetComponent<NPC>();
                if (npc != null)
                {
                    npc.InteractedWith(Interacted.selection.gameObject);
                }
            }
        }
        else if (Interacted.selection == null)
        {
            WorldSlot.SetActive(false);
        }

        if (Interacted.selection.name == "Door")
        {
            WorldSlot.SetActive(true);

            if (BKey.StickFixed == false)
            {
                npc = Interacted.selection.GetComponent<NPC>();
                if (npc != null)
                {
                    npc.InteractedWith(Interacted.selection.gameObject);
                }
            }
        }

        if (ISO.CanBePickedUp == false)
        {
            return;
        }

        if (Interacted == null || Interacted.selection == null) return;

        
        if (pickup != null && pickup.ISO != null && pickup.ISO.CanBePickedUp == true)
        {
            Inventory?.AddItem(pickup.ISO);

            if (pickup.ISO.ItemName == "Stick" && BKey != null)
            {
                BKey.StickGet = true;
            }

            if (pickup.ISO.ItemName == "Tape" && BKey != null)
            {
                BKey.TapeGet = true;
            }


            Interacted.selection.SetActive(false);
            if (Interacted.interactionText != null) Interacted.interactionText.SetActive(false);
            {
                Interacted.selection = null;
            }
 
            return;

        }

        

    }

    public void OpenGate()
    {
        Animator gateAnimator = GetComponent<Animator>();
        BoxCollider gatecol = GetComponent<BoxCollider>();

        BKey.StickGet = false;
        gateAnimator.SetTrigger("Open");
        gatecol.enabled = false;
    }

    public void PlayerHide()
    {
        Player.SetActive(false);
        Canvass.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void SceneTransition()
    {
        SceneManager.LoadScene(SceneName);
    }
 


}
