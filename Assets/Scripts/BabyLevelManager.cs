using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;
public class BabyLevelManager : MonoBehaviour
{
    public bool StickGet;
    public bool StickFixed;
    public bool TapeGet;
    [SerializeField] Inventory Inventory;
    public GameObject Tut2;
    public AudioSource audioSource;
    
    public NPC dialouge;
    [SerializeReference] ItemsSO items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            audioSource.Play();
        }
        else
        {
            Tut2.SetActive(false);
        }


    }



    }
