
using UnityEngine;
using UnityEngine.UI;


public class SkipCutscene : MonoBehaviour
{
    public Slider skipBar;
    private bool isDecaying;
    [SerializeField]
    private Scene_Manager sceneManager;

    private float fill = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isDecaying);
        if (Input.GetKey(KeyCode.Space))
        {
            skipBar.value += fill * Time.deltaTime;
        }
        else
        {
            Decay();
        }
        if(skipBar.value >= 1)
        {
            sceneManager.ChangeScene();
        }
    }
    private void Decay()
    {   

        skipBar.value -= fill * Time.deltaTime;
        
    }
}
