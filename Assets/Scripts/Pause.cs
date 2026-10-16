using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    private Timer Timer;
    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer = FindAnyObjectByType<Timer>();
        if (Timer == null)
        {
            Debug.LogError("Timer script not found in the scene.");
        }

        player = FindAnyObjectByType<PlayerController>();

        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu != null)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        player.enabled = !player.enabled;
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        if (Timer != null)
        {
            Timer.timerIsRunning = !Timer.timerIsRunning;
        }
        
    }
}
