using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Raycast raycast;
    private NPC npc;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        raycast = GetComponentInChildren<Raycast>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            npc = raycast.selection.GetComponent<NPC>();
            if (npc != null)
            {
                npc.InteractedWith(gameObject);
          
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (npc != null && npc.isDialogueActive)
        {
            raycast.selection = null; // Clear the selection after interaction
            raycast.interactionText.SetActive(false); // Hide the interaction text

        }
    }

    private void OnTriggerEnter(Collider other) //Dialogue that plays when you enter a trigger
    {
        if (other.CompareTag("Cutscene"))
        {
            npc = other.GetComponent<NPC>();
            if (npc != null)
            {
                npc.InteractedWith(gameObject);
            }

        }

        if (other.CompareTag("Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.CompareTag("End"))
        {
            Application.Quit();
        }

    }

    private void Interact()
    {
        string selectedTag = raycast.selection.tag;

        if (selectedTag == "NPC")
        {
            npc = raycast.selection.GetComponent<NPC>();
            if (npc != null)
            {
                npc.InteractedWith(gameObject);
            }
        }
        else if (selectedTag == null)
        {            
            return;
        }
    }
}
