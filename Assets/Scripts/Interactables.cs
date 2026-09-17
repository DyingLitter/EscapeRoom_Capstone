using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
public class Interactables : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] private Interact Interacted;

    private BabyLevelManager BKey;
    private Canvas Canvas;
    private GameObject Canvass;
    private Inventory Inventory;
    private GameObject Player;
    private NPC npc;

    public bool Pickable = false; 

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
        CanAni = Canvas.GetComponent<Animator>();
    }

 
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Pickable == true)
        {
            Interacted.selection.GetComponent<Interactables>().Interact();
            Interacted.interactionText.SetActive(false);
            Debug.Log("Item has Itemed");
        }
        else if (Pickable == false)
        {
            Debug.Log("Item Not Found");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Detected");
        if (Pickable == true)
        {
            Interacted.interactionText.SetActive(true);
        }
        else
        {
            Interacted.interactionText.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
     
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickable = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickable = false;
        }
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
            Interacted.interactionText.SetActive(false);
            if(BKey.StickGet == false)
            {
                npc = Interacted.selection.GetComponent<NPC>();
                if (npc != null)
                {
                    npc.InteractedWith(Interacted.selection.gameObject);
                }
            }
        }

        if (Interacted.selection.name == "Door")
        {
            Interacted.interactionText.SetActive(false);
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

   

    private void QuitGame()
    {
        Application.Quit();
    }

    private void SceneTransition()
    {
        SceneManager.LoadScene(SceneName);
    }

   
}
