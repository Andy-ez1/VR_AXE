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
    public Transform tvTransform;
    public Light tvLight;

    [Header("TV Ekrāns")]
    public Renderer tvScreenRenderer;
    public Material tvOffMaterial;
    public Material tvOnMaterial;

    [Header("Taimera Iestatījumi")]
    public EscapeTimer gameTimer;

    [Header("Subtitri")]
    public SubtitleManager subtitleManager;

    void Start()
    {
        SetMovement(false);

        if (tvLight != null)
            tvLight.enabled = false;

        if (tvScreenRenderer != null)
            tvScreenRenderer.material = tvOffMaterial;

        if (tvVideo != null)
            tvVideo.Stop();

        SetAlpha(1);
        //StartCoroutine(WakingUp());
        
        SetMovement(true);
        if (gameTimer != null)
            gameTimer.StartTimer();
    }

    IEnumerator WakingUp()
    {
        yield return new WaitForSeconds(1.5f);
        SetAlpha(0.5f);
        yield return new WaitForSeconds(0.4f);
        SetAlpha(1f);
        yield return new WaitForSeconds(0.7f);
        SetAlpha(0.3f);
        yield return new WaitForSeconds(0.5f);
        SetAlpha(1f);
        yield return new WaitForSeconds(0.9f);

        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime * 0.25f;
            SetAlpha(Mathf.Clamp01(t));
            yield return null;
        }
        SetAlpha(0);

        // Pagriež player pret TV
        if (tvTransform != null)
        {
            Transform playerHead = Camera.main.transform.parent;
            if (playerHead != null)
            {
                Vector3 directionToTV = tvTransform.position - playerHead.position;
                directionToTV.y = 0;
                playerHead.rotation = Quaternion.LookRotation(directionToTV);
            }
            else
            {
                Debug.LogWarning("Player head transform nav atrasts!");
            }
        }
        else
        {
            Debug.LogWarning("TV Transform nav pievienots Inspector logā!");
        }

        if (tvVideo != null)
        {
            tvVideo.Prepare();

            while (!tvVideo.isPrepared)
            {
                yield return null;
            }

            // TV ieslēdzas
            tvVideo.Play();
            if (tvLight != null)
                tvLight.enabled = true;
            if (tvScreenRenderer != null)
                tvScreenRenderer.material = tvOnMaterial;

            if (subtitleManager != null)
                subtitleManager.StartSubtitles();
            else
                Debug.LogWarning("SubtitleManager nav pievienots!");

            float videoLength = (float)tvVideo.length;
            yield return new WaitForSeconds(videoLength);

            // TV izslēdzas
            tvVideo.Stop();
            if (tvLight != null)
                tvLight.enabled = false;
            if (tvScreenRenderer != null)
                tvScreenRenderer.material = tvOffMaterial;
        }
        else
        {
            Debug.LogWarning("TV Video nav pievienots! Gaida 5s.");
            yield return new WaitForSeconds(5f);
        }

        SetMovement(true);

        if (gameTimer != null)
            gameTimer.StartTimer();
        else
            Debug.LogError("GameTimer nav pievienots IntroManager!");
    }

    void SetMovement(bool state)
    {
        if (moveProvider != null) moveProvider.enabled = state;
        if (teleportProvider != null) teleportProvider.enabled = state;
        if (deviceSimulator != null) deviceSimulator.enabled = state;
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