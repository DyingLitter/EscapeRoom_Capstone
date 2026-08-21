using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool StickGet;
    public bool StickFixed;
    public bool TapeGet;
    [SerializeField] Inventory Inventory;
    public GameObject Tut2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StickGet = false;
        TapeGet = false;
        StickFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TapeGet == true)
        {
            Tut2.SetActive(true);
        }
        else
        {
            Tut2.SetActive(false);
        }
    }

  
}
