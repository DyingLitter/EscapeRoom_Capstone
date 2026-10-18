using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeenLevelManager : MonoBehaviour
{
    [SerializeField] Inventory Inventory;
    public NPC npc;
    public GameObject Desk;
    public GameObject BirdCage;
    public GameObject CanvasScreen;
    public GameObject KeyDrawer;
    public Camera LivCam;
    public Camera Clock;
    public bool KeyDrawerUnlocked;
    

    public Animator hour;
    public Animator minutes;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeyDrawerUnlocked = false;
        StartCoroutine(TriggerWhenDialogueReady());
    }

    private IEnumerator TriggerWhenDialogueReady()
    {
        yield return null; // wait one frame so other Start/Awake run
        if (npc != null) npc.InteractedWith(gameObject);
    }

    public void UnlockKeyDrawer()
    {
        KeyDrawerUnlocked = true;
    }
    
}
