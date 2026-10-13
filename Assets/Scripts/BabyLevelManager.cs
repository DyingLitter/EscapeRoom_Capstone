using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BabyLevelManager : MonoBehaviour
{
    public bool StickGet;
    public bool StickFixed;
    public bool TapeGet;
    [SerializeField] Inventory Inventory;
    public GameObject Tut2;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        TapeGet = false;
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (TapeGet == true)
        {
            Tut2.SetActive(true);
        }
        else
        {
            Tut2.SetActive(false);
        }

        if (Inventory.HasItem("Stick (Fixed)"))
        {
            audioSource.Play();
        }
    }



  
}
