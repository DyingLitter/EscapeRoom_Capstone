using UnityEngine;

public class TeenLevelManager : MonoBehaviour
{
    [SerializeField] Inventory Inventory;
    public GameObject Desk;
    public GameObject BirdCage;
    public GameObject CanvasScreen;
    public GameObject KeyDrawer;
    public bool KeyDrawerUnlocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeyDrawerUnlocked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UnlockKeyDrawer()
    {
        KeyDrawerUnlocked = true;
    }
}
