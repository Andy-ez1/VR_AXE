using UnityEngine;
using TMPro; // Nepieciešams TextMeshPro

public class EscapeTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Te ievelc savu Text(TMP)
    public float timeRemaining = 900f; // 15 minūtes sekundēs
    private bool isRunning = false;

    void Update()
    {
        if (isRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateDisplay();
        }
        else if (timeRemaining <= 0)
        {
            isRunning = false;
            timerText.text = "00:00";
            // Šeit vēlāk pieliksim spēles beigas
        }
    }

    void UpdateDisplay()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        isRunning = true;
    }
}