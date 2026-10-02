using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Interactables : MonoBehaviour
{
    [SerializeField] private Texture2D clickCursorTexture;
    private Vector2 hotspot = Vector2.zero;

    private BabyLevelManager BM;
    private Canvas Canvas;
    private Inventory Inventory;
    private GameObject Player;
    private NPC npc;

    [SerializeField] private Material highlightMaterial;
    private Material previousMaterial;
    private static Interactables currentlyHighlighted;

    public LayerMask myLayerMask;

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
        BM = FindAnyObjectByType<BabyLevelManager>();
        Inventory = FindAnyObjectByType<Inventory>();
        Player = FindAnyObjectByType<PlayerController>()?.gameObject;
        Canvas = FindAnyObjectByType<Canvas>();
        CanAni = Canvas.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
       
        if (Pickable == false)
        {
            currentlyHighlighted = null;
        }
    }

    private void OnMouseEnter()
    {
        var selectionRenderer = GetComponent<Renderer>();
        if (selectionRenderer == null) return;
      
        if (Pickable)
        {
            Cursor.SetCursor(clickCursorTexture, hotspot, CursorMode.Auto);

            if (currentlyHighlighted != this)
            {
                previousMaterial = selectionRenderer.material;
                selectionRenderer.material = highlightMaterial;
                currentlyHighlighted = this;   
            }

        }
        else if (Pickable == false && tag == "NPC")
        {
            Cursor.SetCursor(clickCursorTexture, hotspot, CursorMode.Auto);
            previousMaterial = selectionRenderer.material;
            selectionRenderer.material = highlightMaterial;
            currentlyHighlighted = this;
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, hotspot, CursorMode.Auto);
        RestoreMaterial();
        currentlyHighlighted = null;
    }

    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, myLayerMask, QueryTriggerInteraction.Ignore))
        {
            // Check the hit object first, then search parents
            Interactables clickedObject = hit.collider.GetComponent<Interactables>();

            if (clickedObject == null)
            {
                clickedObject = hit.collider.GetComponentInParent<Interactables>();
            }

            if (clickedObject != null)
            {
                clickedObject = hit.collider.GetComponentInParent<Interactables>();

                Debug.Log($"Found Interactables on {clickedObject.gameObject.name}");
                Debug.Log($"Pickable: {clickedObject.Pickable}");
                Debug.Log($"ISO: {clickedObject.ISO?.name}");

                bool isNpc = clickedObject.gameObject.CompareTag("NPC");

                if (clickedObject.Pickable == false)
                {
                    
                }

                if (isNpc)
                {
                    if (clickedObject.dialogue != null && clickedObject.ISO.CanBePickedUp == false)
                    {
                        StartCoroutine(DialogueCheck(clickedObject.dialogue, clickedObject.gameObject));
                    }
                }
                
                if (clickedObject.Pickable)
                {
                    clickedObject.Interact();
                    Debug.Log("Interact called!");
                }

            }
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
        if (!other.CompareTag("Player")) return;

        Pickable = false;
       
    }

    private void RestoreMaterial()
    {
        var r = GetComponent<Renderer>();
        if (r != null && previousMaterial != null)
        {
            r.material = previousMaterial;
        }
    }

    public void Interact()
    {
        if (Picked) return;
        
        if (WorldSlot != null)
        {
            
        }
        else if (ISO.CanBePickedUp == true)
        {
            Picked = true;
        }

        var pickup = this;

        onClick?.Invoke();

        if (ISO.CanBePickedUp == false)
        {
            WorldSlot.SetActive(true);
            npc = gameObject.GetComponent<NPC>();
            if (npc != null)
                {
                    StartCoroutine(DialogueCheck(npc, gameObject));
                    return;
                }
        }
        
        

        
        if (pickup != null && pickup.ISO != null && pickup.ISO.CanBePickedUp == true)
        {
            Inventory?.AddItem(pickup.ISO);

            
                npc = gameObject.GetComponent<NPC>();
                if (npc != null)
                {
                    StartCoroutine(DialogueCheck(npc, gameObject));
                    return;
                }
                if (pickup.ISO.ItemName == "Tape" && BM != null)
                {
                    BM.TapeGet = true;
                }
            gameObject.SetActive(false);
            return;

        }

        

    }

    private IEnumerator DialogueCheck(NPC npc, GameObject item)
    {
        if (ISO.CanBePickedUp == false)
        {
            if (WorldSlot == null)
            {

            }
            else if (WorldSlot != null)
            {
                WorldSlot.SetActive(true);
            }

        }
        if (ISO.CanBePickedUp == true)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
        }

        Debug.Log("Play");
        npc.StartDialogue();

        while (npc.isDialogueActive)
        {
            yield return null;
        }

        if (ISO.CanBePickedUp == false)
        {
            
        }
        else if (ISO.CanBePickedUp == true)
        {
            item.SetActive(false);
        }
       
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
