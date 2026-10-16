using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public string SceneName;
    private Scene c_Scene;
    private string CurrentScene;
    
    private void Start()
    {
        
        c_Scene = SceneManager.GetActiveScene();
        CurrentScene = c_Scene.name;
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void Update()
    {
        
    }
}
