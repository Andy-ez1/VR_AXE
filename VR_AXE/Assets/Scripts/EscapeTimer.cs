using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class EscapeTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 900f; // 15 minūtes
    public float lightSpeedMultiplier = 1.5f; // Cik ātrāk iet laiks ar gaismu

    private bool isRunning = false;
    private bool lightsOn = false;

    public UnityEvent onTimerEnd; // Izsaucas kad laiks beidzas (nāve)

    void Update()
    {
        if (!isRunning || timeRemaining <= 0) return;

        // Ja gaisma ieslēgta - laiks iet ātrāk
        float multiplier = lightsOn ? lightSpeedMultiplier : 1f;
        timeRemaining -= Time.deltaTime * multiplier;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;
            timerText.text = "00:00";
            onTimerEnd?.Invoke(); // Nāve
        }
        else
        {
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void SetLightsOn(bool state)
    {
        lightsOn = state;
    }
}