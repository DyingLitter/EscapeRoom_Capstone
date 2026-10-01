using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Interactables : MonoBehaviour
{
    [SerializeField] private Interact Interacted;

    private BabyLevelManager BM;
    private Canvas Canvas;
    private Inventory Inventory;
    private GameObject Player;
    private NPC npc;

    public bool Pickable = false; 

    
    [SerializeField] bool Picked = false;

    public ItemsSO ISO;
    [SerializeField] private NPC dialogue;
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject WorldSlot;
    private Animator CanAni;

    public UnityEvent onClick;
    public void Start()
    {
        Interacted = FindAnyObjectByType<Interact>();
        BM = FindAnyObjectByType<BabyLevelManager>();
        Inventory = FindAnyObjectByType<Inventory>();
        if (Player == null)
        {

        }
        else
        {
            Player = FindAnyObjectByType<PlayerController>().gameObject;
        }
        Canvas = FindAnyObjectByType<Canvas>();
        CanAni = Canvas.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }

        if (Pickable)
        {
            //ShowInteractionText();
        }
    }
    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = ~(1 << LayerMask.NameToLayer("IgnoreRaycast"));

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");
            Debug.Log($"Hit object transform: {hit.transform.name}");

            // Check the hit object first, then search parents
            Interactables clickedObject = hit.collider.GetComponent<Interactables>();

            if (clickedObject == null)
            {
                Debug.Log("Script not on hit object, checking parents...");
                clickedObject = hit.collider.GetComponentInParent<Interactables>();
            }

            if (clickedObject != null)
            {
                Debug.Log($"Found Interactables on {clickedObject.gameObject.name}");
                Debug.Log($"Pickable: {clickedObject.Pickable}");
                Debug.Log($"ISO: {clickedObject.ISO?.name}");

                if (clickedObject.Pickable)
                {
                    Interacted.selection = clickedObject.gameObject;
                    clickedObject.Interact();
                    Debug.Log("Interact called!");
                }
                else
                {
                    Debug.Log("Object not pickable (player not in range)");
                }
            }
            else
            {
                Debug.Log($"No Interactables script found on {hit.collider.gameObject.name} or its parents!");
            }
        }
        else
        {
            Debug.Log("Raycast didn't hit anything");
        }
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
        if (Interacted.selection == null || Picked) return;
        
        if (Interacted.selection.name == "Inventory")
        {
            
        }
        else
        {
            Picked = true;
        }
         

        var pickup = Interacted.selection.GetComponent<Interactables>();
        ISO.InteractChecks();

        onClick?.Invoke();

        if (ISO.CanBePickedUp == false)
        {
            WorldSlot.SetActive(true);
            npc = Interacted.selection.GetComponent<NPC>();
            if (npc != null)
                {
                    StartCoroutine(DialogueCheck(npc, Interacted.selection));
                    return;
                }
        }
        
        if (ISO.CanBePickedUp == false)
        {
            return;
        }

        
        if (pickup != null && pickup.ISO != null && pickup.ISO.CanBePickedUp == true)
        {
            Inventory?.AddItem(pickup.ISO);

            
                npc = Interacted.selection.GetComponent<NPC>();
                if (npc != null)
                {
                    StartCoroutine(DialogueCheck(npc, Interacted.selection));
                    return;
                }
                if (pickup.ISO.ItemName == "Tape" && BM != null)
                {
                    BM.TapeGet = true;
                }
            Interacted.selection.SetActive(false);
            Interacted.selection = null;
            return;

        }

        

    }

    private IEnumerator DialogueCheck(NPC npc, GameObject item)
    {
        item.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Play");
        npc.StartDialogue();

        while (npc.isDialogueActive)
        {
            yield return null;
        }
        if (ISO.CanBePickedUp == false)
        {
            WorldSlot.SetActive(true);
        }
        else if (ISO.CanBePickedUp == true)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
            item.SetActive(false);
        }
            
        Interacted.selection = null;
    }

    public void OpenGate()
    {
        Animator gateAnimator = GetComponent<Animator>();
        BoxCollider gatecol = GetComponent<BoxCollider>();

        BM.StickGet = false;
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
