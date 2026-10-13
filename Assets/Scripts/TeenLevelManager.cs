using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeenLevelManager : MonoBehaviour
{
    [SerializeField] Inventory Inventory;
    public GameObject Desk;
    public GameObject BirdCage;
    public GameObject CanvasScreen;
    public GameObject KeyDrawer;
    public bool KeyDrawerUnlocked;
    public Image fadeScreen;

    public Animator Hour;
    public Animator Minutes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeyDrawerUnlocked = false;
        StartCoroutine(StartFadeIn());
        if(Hour != null)
        {
            Hour.SetTrigger("Start");
        }
        if(Minutes != null)
        {
            Minutes.SetTrigger("Start");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UnlockKeyDrawer()
    {
        KeyDrawerUnlocked = true;
    }
    IEnumerator StartFadeIn()
    {
        yield return new WaitForSeconds(1f);
        fadeScreen.CrossFadeAlpha(0f, 5f, false);
    }
}
