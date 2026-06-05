using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using NavKeypad;

[RequireComponent(typeof(XRSimpleInteractable))]
public class SimpleKeypadButton : MonoBehaviour
{
    [Header("Pogas vērtība: 0-9 vai enter")]
    public string value;

    [Header("Keypad atsauce")]
    public Keypad keypad;

    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnPress);
    }

    void OnPress(SelectEnterEventArgs args)
    {
        Debug.Log("Poga: " + value);
        if (keypad != null)
            keypad.AddInput(value);
    }

    void OnDestroy()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnPress);
    }
}