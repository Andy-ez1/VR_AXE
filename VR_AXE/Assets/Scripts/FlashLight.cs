using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Flashlight : MonoBehaviour
{
    [Header("Gaisma")]
    public Light flashlightBeam;
    private bool isOn = false;
    private bool isHeld = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Paņemšana un noliekšana
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // Trigger ieslēdz/izslēdz
        grabInteractable.activated.AddListener(OnTrigger);
    }

    void Start()
    {
        if (flashlightBeam != null)
            flashlightBeam.enabled = false;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    void OnTrigger(ActivateEventArgs args)
    {
        if (!isHeld) return;

        isOn = !isOn;

        if (flashlightBeam != null)
            flashlightBeam.enabled = isOn;
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
        grabInteractable.activated.RemoveListener(OnTrigger);
    }
}