using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool StickGet;
    [SerializeField] Inventory Inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StickGet = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneReset()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
