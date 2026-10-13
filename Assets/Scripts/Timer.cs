using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float timeRemaining = 300;
    public bool timerIsRunning = false;
    [SerializeField] private TMP_Text timer;

    public UnityEvent OnTimerEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else if (timeRemaining <=0)
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerEnd?.Invoke();
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1f;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
