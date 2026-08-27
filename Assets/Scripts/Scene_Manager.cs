using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void MoveToGame()
    {
        SceneManager.LoadScene("Baby Level");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
