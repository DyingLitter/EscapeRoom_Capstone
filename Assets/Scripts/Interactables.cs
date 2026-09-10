using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class Interactables : MonoBehaviour
{
    private Interact Interacted;

    private BabyLevelManager BKey;
    private GameObject Canvas;
    private Inventory Inventory;
    private GameObject Player;
    public ItemsSO ISO;
    [SerializeField] private string SceneName;
    public void Start()
    {
        Interacted = FindAnyObjectByType<Interact>();
        BKey = FindAnyObjectByType<BabyLevelManager>();
        Inventory = FindAnyObjectByType<Inventory>();
        Player = FindAnyObjectByType<PlayerController>().gameObject;
        Canvas = FindAnyObjectByType<Canvas>().gameObject;
    }
    public void Interact()
    {
        if (Interacted == null || Interacted.selection == null) return;

        ISO.InteractChecks();

        var pickup = Interacted.selection.GetComponent<Interactables>();
        if (pickup != null && pickup.ISO != null && pickup.ISO.name != "Gate" && pickup.ISO.name != "NPC")
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

    public void PlayerHide()
    {
        Player.SetActive(false);
        Canvas.SetActive(false);
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
