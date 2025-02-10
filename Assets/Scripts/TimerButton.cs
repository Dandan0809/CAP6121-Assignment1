using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class TimerButton : MonoBehaviour
{
    public GameObject targetObject; // The GameObject to activate
    public Canvas targetCanvas; // The Canvas to deactivate
    public float timerDuration = 10; // Set timer duration (optional)
    public TextMeshProUGUI timerText; // Assign a TMP text UI element

    private float timer = 0;
    private bool isTimerRunning = false;

    void Start()
    {
        // Ensure the target object is inactive at the start
        if (targetObject != null)
            targetObject.SetActive(false);

        // Initialize Timer Text Display
        if (timerText != null)
            timerText.text = timerDuration.ToString("F2") + "s";
    }

    public void OnButtonClick()
    {
        // Activate the target object
        if (targetObject != null)
            targetObject.SetActive(true);

        // Deactivate the target canvas
        if (targetCanvas != null)
            targetCanvas.gameObject.SetActive(false);

        // Start the timer
        timer = timerDuration;
        isTimerRunning = true;

        Debug.Log("Button clicked: Object Activated, Canvas Deactivated, Timer Started!");
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;

            // Ensure Timer Never Goes Below Zero
            if (timer < 0) timer = 0;

            // Update TextMeshPro UI Timer
            if (timerText != null)
                timerText.text = timer.ToString("F2") + "s"; // Show two decimal places
        }
    }
}
            // Stop Timer When It Reaches
