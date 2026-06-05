using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    [Header("Win UI")]
    public GameObject winScreen;      // Panelis ar tekstu + pogām

    [Header("Audio")]
    public AudioSource winMusic;      // Uzvaras mūzika

    [Header("Taimeris (apstāties)")]
    public EscapeTimer gameTimer;

    private bool triggered = false;

    void Start()
    {
        if (winScreen != null)
            winScreen.SetActive(false); // Sākumā paslēpts
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.GetComponentInParent<Camera>() != null || other.CompareTag("Player"))
        {
            triggered = true;

            // Aptur taimeri
            if (gameTimer != null)
                gameTimer.enabled = false;

            // Mūzika
            if (winMusic != null)
                winMusic.Play();

            // Parāda win UI (teksts + pogas)
            if (winScreen != null)
                winScreen.SetActive(true);
        }
    }
}