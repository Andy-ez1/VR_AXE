using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace NavKeypad
{
    [RequireComponent(typeof(KeypadButton))]
    public class KeypadButtonXR : MonoBehaviour
    {
        private KeypadButton keypadButton;
        private XRSimpleInteractable interactable;

        void Awake()
        {
            keypadButton = GetComponent<KeypadButton>();
            interactable = GetComponent<XRSimpleInteractable>();

            if (interactable != null)
            {
                interactable.selectEntered.AddListener(OnPress);
                interactable.hoverEntered.AddListener(OnHover);
            }
            else
            {
                Debug.LogError("XR Simple Interactable NAV uz pogas: " + gameObject.name);
            }
        }

        void OnHover(HoverEnterEventArgs args)
        {
            Debug.Log("STARS UZ POGAS (hover): " + gameObject.name);
        }

        void OnPress(SelectEnterEventArgs args)
        {
            Debug.Log("POGA NOSPIESTA: " + gameObject.name);
            keypadButton.PressButton();
        }

        void OnDestroy()
        {
            if (interactable != null)
            {
                interactable.selectEntered.RemoveListener(OnPress);
                interactable.hoverEntered.RemoveListener(OnHover);
            }
        }
    }
}