using UnityEngine;

public class UVRevealable : MonoBehaviour
{
    [Header("Cipars")]
    public GameObject digitVisual;

    void Start()
    {
        if (digitVisual != null)
            digitVisual.SetActive(false);
    }

    public void Reveal()
    {
        if (digitVisual != null)
            digitVisual.SetActive(true);
    }

    public void Hide()
    {
        if (digitVisual != null)
            digitVisual.SetActive(false);
    }
}