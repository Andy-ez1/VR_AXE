using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public Image faderImage; 

    void Start()
    {
        StartCoroutine(WakingUp());
    }

    IEnumerator WakingUp()
    {
        // Sākumā pilnīgs melnums 1 sekundi
        SetAlpha(1);
        yield return new WaitForSeconds(1.0f);

        // 1. Lēns mirkšķinājums (atver un aizver)
        SetAlpha(0.5f); yield return new WaitForSeconds(0.4f); // Pusvirus
        SetAlpha(1);    yield return new WaitForSeconds(0.6f); // Atpakaļ ciet

        // 2. Garāks mirkšķinājums
        SetAlpha(0.3f); yield return new WaitForSeconds(0.5f); // Gandrīz redz
        SetAlpha(1);    yield return new WaitForSeconds(0.8f); // Atkal ciet uz mirkli

        // 3. Galējā lēnā atvēršanās (MIEGAINUMS)
        float t = 1f;
        while (t > 0)
        {
            // Šis process tagad aizņems aptuveni 3-4 sekundes
            t -= Time.deltaTime * 0.25f; 
            SetAlpha(t);
            yield return null; 
        }
        
        // Šeit vēlāk pievienosim rindiņu, lai ieslēgtu TV audio
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