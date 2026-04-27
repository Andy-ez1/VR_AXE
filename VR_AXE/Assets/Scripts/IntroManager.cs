using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    [Header("UI Elementi")]
    public Image faderImage;

    [Header("Kustības Kontrole")]
    // Šeit mēs izmantojam vispārīgu tipu, lai nebūtu kļūdu ar nosaukumiem
    public MonoBehaviour moveProvider; 
    public MonoBehaviour teleportProvider;
    public MonoBehaviour deviceSimulator; // Ja lieto PC simulatoru

    void Start()
    {
        // 1. Izslēdzam visu kustību uzreiz
        if (moveProvider != null) moveProvider.enabled = false;
        if (teleportProvider != null) teleportProvider.enabled = false;
        if (deviceSimulator != null) deviceSimulator.enabled = false;

        SetAlpha(1);
        StartCoroutine(WakingUp());
    }

    IEnumerator WakingUp()
    {
        // Pirmā pauze tumsā
        yield return new WaitForSeconds(1.5f);

        // Mirkšķināšana
        SetAlpha(0.5f); yield return new WaitForSeconds(0.4f);
        SetAlpha(1);    yield return new WaitForSeconds(0.7f);
        SetAlpha(0.3f); yield return new WaitForSeconds(0.5f);
        SetAlpha(1);    yield return new WaitForSeconds(0.9f);

        // Lēnā atvēršanās
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime * 0.25f;
            SetAlpha(t);
            yield return null;
        }

        // 2. Ieslēdzam kustību atpakaļ
        if (moveProvider != null) moveProvider.enabled = true;
        if (teleportProvider != null) teleportProvider.enabled = true;
        if (deviceSimulator != null) deviceSimulator.enabled = true;
        
        Debug.Log("Pamošanās pabeigta! Kustība atļauta.");
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