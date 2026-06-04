using UnityEngine;
using System.Collections;

public class SafeDoor : MonoBehaviour
{
    [Header("Durvis")]
    public Transform doorToOpen;
    public Vector3 openRotation = new Vector3(0, 110, 0);
    public float openSpeed = 2f;

    [Header("Skaņa (neobligāti)")]
    public AudioSource doorAudio;

    private bool isOpen = false;

    // Šo izsauc keypad onAccessGranted
    public void OpenSafe()
    {
        if (isOpen) return;
        isOpen = true;
        StartCoroutine(OpenRoutine());

        if (doorAudio != null)
            doorAudio.Play();
    }

    IEnumerator OpenRoutine()
    {
        Quaternion start = doorToOpen.localRotation;
        Quaternion target = Quaternion.Euler(openRotation);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            doorToOpen.localRotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }
    }
}