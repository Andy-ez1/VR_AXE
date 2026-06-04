using UnityEngine;

public class UVLight : MonoBehaviour
{
    [Header("UV Stars")]
    public Transform rayOrigin;
    public float rayDistance = 10f;
    public Light uvBeam;

    private UVRevealable currentDigit; // Cipars ko stars šobrīd trāpa

    void Update()
    {
        if (uvBeam == null || !uvBeam.enabled)
        {
            // Gaisma izslēgta - paslēp pēdējo ciparu
            if (currentDigit != null)
            {
                currentDigit.Hide();
                currentDigit = null;
            }
            return;
        }

        Transform origin = rayOrigin != null ? rayOrigin : transform;
        Ray ray = new Ray(origin.position, origin.forward);
        RaycastHit hit;

        UVRevealable hitDigit = null;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            hitDigit = hit.collider.GetComponent<UVRevealable>();
        }

        // Ja trāpa citu ciparu nekā iepriekš
        if (hitDigit != currentDigit)
        {
            // Paslēp veco
            if (currentDigit != null)
                currentDigit.Hide();

            // Parādi jauno
            if (hitDigit != null)
                hitDigit.Reveal();

            currentDigit = hitDigit;
        }
    }
}