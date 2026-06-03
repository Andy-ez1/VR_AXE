using UnityEngine;

public class UVRevealable : MonoBehaviour
{
    [Header("Cipars")]
    public GameObject digitVisual;   // Pats cipars (TMP vai mesh)
    public bool stayRevealed = false; // Vai paliek redzams pēc atklāšanas

    private bool revealed = false;

    void Start()
    {
        if (digitVisual != null)
            digitVisual.SetActive(false); // Sākumā neredzams
    }

    public void Reveal()
    {
        if (digitVisual != null)
            digitVisual.SetActive(true);

        if (stayRevealed)
            revealed = true;
    }

    public void Hide()
    {
        if (revealed) return; // Ja jāpaliek redzamam, nepaslēpj
        if (digitVisual != null)
            digitVisual.SetActive(false);
    }
}