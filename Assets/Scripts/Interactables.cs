using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
public class Interactables : MonoBehaviour
{
    [SerializeField] private Interact Interacted;

    private BabyLevelManager BKey;
    private Canvas Canvas;
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

    //private void ShowInteractionText()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

    //    if (Physics.Raycast(ray, out RaycastHit hit, 100f))
    //    {
    //        if (hit.collider.gameObject == gameObject && Pickable)
    //        {
    //            Interacted.interactionText.SetActive(true);
    //        }
    //        else
    //        {
    //            Interacted.interactionText.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        Interacted.interactionText.SetActive(false);
    //    }
    //}

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
        if (Interacted.selection == null) return;

        var pickup = Interacted.selection.GetComponent<Interactables>();
        ISO.InteractChecks();

        if (pickup.ISO.name == "Desk")
        {
            if (CanAni != null)
            {
                CanAni.SetTrigger("OpenInv");
            }
         
        }

        if (Interacted.selection.name == "Gate")
        {
            if(BKey.StickGet == false)
            {
                npc = Interacted.selection.GetComponent<NPC>();
                if (npc != null)
                {
                    npc.InteractedWith(Interacted.selection.gameObject);
                }
            }

            WorldSlot.SetActive(true);
        }

        if (Interacted.selection.name == "Door")
        {
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

        
        if (pickup != null && pickup.ISO != null && pickup.ISO.CanBePickedUp == true)
        {
            Inventory?.AddItem(pickup.ISO);

            if (pickup.ISO.ItemName == "Stick" && BKey != null)
            {
                BKey.StickGet = true;

                npc = Interacted.selection.GetComponent<NPC>();
                if (npc != null)
                {
                    npc.InteractedWith(Interacted.selection.gameObject);

                }

              
            }

            if (pickup.ISO.ItemName == "Tape" && BKey != null)
            {
                BKey.TapeGet = true;
            }
            


            Interacted.selection.SetActive(false);
            Interacted.selection = null;
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
