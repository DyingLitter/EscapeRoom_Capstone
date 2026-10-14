using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class BabyLevelManager : MonoBehaviour
{
    public bool StickGet;
    public bool StickFixed;
    public bool TapeGet;
    [SerializeField] Inventory Inventory;
    public GameObject Tut2;
    public AudioSource audioSource;
    public Image fadeScreen;
    public NPC dialouge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartFadeIn());
        TapeGet = false;
        audioSource = GetComponent<AudioSource>();
        dialouge.StartDialogue();
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
    IEnumerator StartFadeIn()
    {
        yield return new WaitForSeconds(1f);
        fadeScreen.CrossFadeAlpha(0f, 5f, false);
    }



}
