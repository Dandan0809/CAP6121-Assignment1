using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class TimerButton : MonoBehaviour
{
    public GameObject lightSaber;
    public Canvas timerCanvas;
    public float timerDuration = 10;
    public TextMeshProUGUI timerText;
    public GameObject droid;
    public Canvas resultCanvas;
    public SaberDeflect saberDeflect;
    public Canvas handCanvas;


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

        if (timerCanvas != null)
            timerCanvas.gameObject.SetActive(false);

        if (droid != null)
            droid.SetActive(false);

        if (resultCanvas != null)
            resultCanvas.gameObject.SetActive(false);

        if (handCanvas != null)
            handCanvas.gameObject.SetActive(true);

    }

    public void OnButtonClick()
    {
        if (lightSaber != null)
            lightSaber.SetActive(true);

        if (timerCanvas != null)
            timerCanvas.gameObject.SetActive(true);

        if (timerText != null)
            timerText.gameObject.SetActive(true);

        if (droid != null)
        {
            droid.SetActive(false);
            droid.SetActive(true);
        }

        if (saberDeflect != null)
        {
            saberDeflect.deflectCount = 0;
            saberDeflect.UpdateDeflectUI();
        }

        if (resultCanvas != null)
            resultCanvas.gameObject.SetActive(false);

        if (handCanvas != null)
            handCanvas.gameObject.SetActive(false);

        timer = timerDuration;
        isTimerRunning = true;
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
        if (timerCanvas != null)
            timerCanvas.gameObject.SetActive(false);

        if (lightSaber != null)
            lightSaber.SetActive(false);

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        if (droid != null)
            droid.SetActive(false);

        if (resultCanvas != null)
            resultCanvas.gameObject.SetActive(true);

        if (handCanvas != null)
            handCanvas.gameObject.SetActive(true);
    }
}
