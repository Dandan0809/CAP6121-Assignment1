using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerButton : MonoBehaviour
{
    public GameObject lightSaber; 
    public Canvas targetCanvas;  
    public float timerDuration = 10;  
    public TextMeshProUGUI timerText;  

    private float timer = 0;
    private bool isTimerRunning = false;

    void Start()
    {
        if (lightSaber != null)
            lightSaber.SetActive(false);

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
            timerText.text = timerDuration.ToString("F2") + "s";
        }

        if (targetCanvas != null)
            targetCanvas.gameObject.SetActive(true);
    }

    public void OnButtonClick()
    {
        if (lightSaber != null)
            lightSaber.SetActive(true);

        if (targetCanvas != null)
            targetCanvas.gameObject.SetActive(false);

        if (timerText != null)
            timerText.gameObject.SetActive(true);

        timer = timerDuration;
        isTimerRunning = true;

        //Debug.Log("Button clicked: Object Activated, Canvas Deactivated, Timer Started!");
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
                timer = 0;

            if (timerText != null)
                timerText.text = timer.ToString("F2") + "s";

            if (timer <= 0)
            {
                isTimerRunning = false;
                TimerEnded();
            }
        }
    }

    void TimerEnded()
    {
        //Debug.Log("Timer finished!");

        if (targetCanvas != null)
            targetCanvas.gameObject.SetActive(true);

        if (lightSaber != null)
            lightSaber.SetActive(false);

        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }
}
