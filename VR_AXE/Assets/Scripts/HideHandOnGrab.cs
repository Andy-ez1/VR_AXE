using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HideHandOnGrab : MonoBehaviour
{
    [Header("Rokas vizuālie objekti")]
    public GameObject leftHandVisual;
    public GameObject rightHandVisual;

    private XRGrabInteractable grabInteractable;
    private GameObject hiddenHand;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        string interactorName = args.interactorObject.transform.name.ToLower();

        if (interactorName.Contains("left") && leftHandVisual != null)
            hiddenHand = leftHandVisual;
        else if (rightHandVisual != null)
            hiddenHand = rightHandVisual;

        if (hiddenHand != null)
            hiddenHand.SetActive(false);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        if (hiddenHand != null)
        {
            hiddenHand.SetActive(true);
            hiddenHand = null;
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}