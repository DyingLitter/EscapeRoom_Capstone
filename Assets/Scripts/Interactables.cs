using UnityEngine;

public class Interactables : MonoBehaviour
{
    private Interact Interacted;
    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      player = FindAnyObjectByType<PlayerController>();
      Interacted = FindAnyObjectByType<Interact>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (Interacted.selectableTags.Contains("Key"))
        {
           player.KeyGet = true;
           Interacted.selection.SetActive(false);
        }

        if (Interacted.selectableTags.Contains("Door"))
        {
            if (player.KeyGet == true)
            {
                Interacted.selection.SetActive(false);
            }
            else
            {
                return;
            }
        }
    }
}
