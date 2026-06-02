using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Flashlight : MonoBehaviour
{
    [Header("Gaisma")]
    public Light flashlightBeam;

    [Header("Haptika")]
    public float hapticAmplitude = 0.5f;
    public float hapticDuration = 0.1f;

    private bool isOn = false;
    private bool isHeld = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
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
        SendHaptics(args.interactorObject);
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

        SendHaptics(args.interactorObject);
    }

    void SendHaptics(IXRInteractor interactor)
    {
        if (interactor is XRBaseInputInteractor controllerInteractor)
        {
            controllerInteractor.SendHapticImpulse(hapticAmplitude, hapticDuration);
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
        grabInteractable.activated.RemoveListener(OnTrigger);
    }
}