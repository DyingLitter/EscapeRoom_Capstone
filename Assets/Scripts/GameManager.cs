using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool StickGet;
    public bool StickFixed;
    [SerializeField] Inventory Inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StickGet = false;
        StickFixed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

  
}
