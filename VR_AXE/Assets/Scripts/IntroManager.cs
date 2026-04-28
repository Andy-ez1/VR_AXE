using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [Header("UI Elementi")]
    public Image faderImage;

    [Header("Kustības Kontrole")]
    public MonoBehaviour moveProvider; 
    public MonoBehaviour teleportProvider;
    public MonoBehaviour deviceSimulator; 

    [Header("TV Iestatījumi")]
    public VideoPlayer tvVideo; 

    [Header("Taimera Iestatījumi")]
    public EscapeTimer gameTimer; // Šeit Inspector logā ievelc savu taimera objektu

    void Start()
    {
        if (moveProvider != null) moveProvider.enabled = false;
        if (teleportProvider != null) teleportProvider.enabled = false;
        if (deviceSimulator != null) deviceSimulator.enabled = false;

        if (tvVideo != null) tvVideo.Stop();

        SetAlpha(1);
        StartCoroutine(WakingUp());
    }

    IEnumerator WakingUp()
    {
        yield return new WaitForSeconds(1.5f);

        SetAlpha(0.5f); yield return new WaitForSeconds(0.4f);
        SetAlpha(1);    yield return new WaitForSeconds(0.7f);
        SetAlpha(0.3f); yield return new WaitForSeconds(0.5f);
        SetAlpha(1);    yield return new WaitForSeconds(0.9f);

        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime * 0.25f;
            SetAlpha(t);
            yield return null;
        }

        if (tvVideo != null) 
        {
            tvVideo.Play();
            Debug.Log("TV Ieslēdzas!");
        }

        if (moveProvider != null) moveProvider.enabled = true;
        if (teleportProvider != null) teleportProvider.enabled = true;
        if (deviceSimulator != null) deviceSimulator.enabled = true;

        if (tvVideo != null)
        {
            yield return new WaitForSeconds((float)tvVideo.length);
            tvVideo.Stop(); 
            Debug.Log("TV Video beidzies, ekrāns paliek melns.");

            // --- ŠEIT PIEVIENOTA TAIMERA PALAIŠANA ---
            if (gameTimer != null)
            {
                gameTimer.StartTimer();
                Debug.Log("Taimeris sācis skaitīt!");
            }
        }
    }

    void SetAlpha(float alpha)
    {
        if (faderImage != null)
        {
            Color c = faderImage.color;
            c.a = alpha;
            faderImage.color = c;
        }
    }
}
