using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI subtitleText;
    public GameObject subtitleBackground; // Panel objekts

    [System.Serializable]
    public class SubtitleEntry
    {
        public float startTime;   // Kad parādās (sekundēs)
        public float endTime;     // Kad pazūd
        [TextArea] public string text; // Teksts
    }

    [Header("Subtitri")]
    public SubtitleEntry[] subtitles;

    void Start()
    {
        HideSubtitle();
    }

    public void StartSubtitles()
    {
        StartCoroutine(PlaySubtitles());
    }

    IEnumerator PlaySubtitles()
    {
        float elapsed = 0f;
        int index = 0;

        while (index < subtitles.Length)
        {
            elapsed += Time.deltaTime;
            SubtitleEntry current = subtitles[index];

            if (elapsed >= current.startTime && elapsed < current.endTime)
            {
                ShowSubtitle(current.text);
            }
            else if (elapsed >= current.endTime)
            {
                HideSubtitle();
                index++;
            }

            yield return null;
        }

        HideSubtitle();
    }

    void ShowSubtitle(string text)
    {
        subtitleText.text = text;
        subtitleText.gameObject.SetActive(true);
        if (subtitleBackground != null) 
            subtitleBackground.SetActive(true);
    }

    void HideSubtitle()
    {
        subtitleText.text = "";
        subtitleText.gameObject.SetActive(false);
        if (subtitleBackground != null) 
            subtitleBackground.SetActive(false);
    }
}